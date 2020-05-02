using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WheelController : MonoBehaviour {
        
    [Header("Inputs")]
    // If isPlayer is false inputs are ignored
    [SerializeField] ControlType controlType = ControlType.AI;
    public ControlType ControlType {
        get { return controlType; }
        set { controlType = value; }
    } 
        
    /* 
        *  Turn input curve: x real input, y value used
        *  My advice (-1, -1) tangent x, (0, 0) tangent 0 and (1, 1) tangent x
        */
    [SerializeField] AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

    [Header("Wheels")]
    [SerializeField] WheelCollider[] driveWheel;
    public WheelCollider[] DriveWheel { get { return driveWheel; } }
    [SerializeField] WheelCollider[] turnWheel;
    public WheelCollider[] TurnWheel { get { return turnWheel; } }

    // This code checks if the car is grounded only when needed and the data is old enough
    bool isGrounded = false;
    int lastGroundCheck = 0;
    public bool IsGrounded { get {
        if (lastGroundCheck == Time.frameCount)
            return isGrounded;

        lastGroundCheck = Time.frameCount;
        isGrounded = true;
        foreach (WheelCollider wheel in wheels)
        {
            if (!wheel.gameObject.activeSelf || !wheel.isGrounded)
                isGrounded = false;
        }
        return isGrounded;
    }}

    [Header("Behaviour")]
    /*
        *  Motor torque represent the torque sent to the wheels by the motor with x: speed in km/h and y: torque
        *  The curve should start at x=0 and y>0 and should end with x>topspeed and y<0
        *  The higher the torque the faster it accelerate
        *  the longer the curve the faster it gets
        */
    public float maxSpeed = 100;
    private float boostSpeedIncrease = 30;

    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

    // Differential gearing ratio
    [Range(2, 16)]
    [SerializeField] float diffGearing = 4.0f;
    public float DiffGearing { get { return diffGearing; } set { diffGearing = value; } }

    // Basicaly how hard it brakes
    [SerializeField] float brakeForce = 1500.0f;
    public float BrakeForce { get { return brakeForce; } set { brakeForce = value; } }

    // Max steering hangle, usualy higher for drift car
    [Range(0f, 50.0f)]
    [SerializeField] float steerAngle = 30.0f;
    public float SteerAngle { get { return steerAngle; } set { steerAngle = Mathf.Clamp(value, 0.0f, 50.0f); } }

    // The value used in the steering Lerp, 1 is instant (Strong power steering), and 0 is not turning at all
    [Range(0.001f, 1.0f)]
    [SerializeField] float steerSpeed = 0.2f;
    public float SteerSpeed { get { return steerSpeed; } set { steerSpeed = Mathf.Clamp(value, 0.001f, 1.0f); } }

    // How hight do you want to jump?
    [Range(1f, 5f)]
    [SerializeField] float jumpVel = 1.3f;
    public float JumpVel { get { return jumpVel; } set { jumpVel = Mathf.Clamp(value, 1.0f, 5f); } }

    // How hard do you want to drift?
    [Range(0.0f, 2f)]
    [SerializeField] float driftIntensity = 1f;
    public float DriftIntensity { get { return driftIntensity; } set { driftIntensity = Mathf.Clamp(value, 0.0f, 2.0f); }}

    /*
        *  The center of mass is set at the start and changes the car behavior A LOT
        *  I recomment having it between the center of the wheels and the bottom of the car's body
        *  Move it a bit to the from or bottom according to where the engine is
        */
    [SerializeField] Transform centerOfMass;

    // Force aplied downwards on the car, proportional to the car speed
    [Range(0.5f, 10f)]
    [SerializeField] float downforce = 1.0f;
    public float Downforce { get{ return downforce; } set{ downforce = Mathf.Clamp(value, 0, 5); } }     

    // When IsPlayer is false you can use this to control the steering
    float steering;
    public float Steering { get{ return steering; } set{ steering = Mathf.Clamp(value, -1f, 1f); } } 

    // When IsPlayer is false you can use this to control the throttle
    float throttle;
    public float Throttle { get{ return throttle; } set{ throttle = Mathf.Clamp(value, -1f, 1f); } } 

    // Like your own car handbrake, if it's true the car will not move
    [SerializeField] bool handbrake;
    public bool Handbrake { get{ return handbrake; } set{ handbrake = value; } } 
        
    // Use this to disable drifting
    [HideInInspector] public bool allowDrift = true;
    bool drift;
    public bool Drift { get{ return drift; } set{ drift = value; } }         

    // Use this to read the current car speed (you'll need this to make a speedometer)
    [SerializeField] float speed = 0.0f;
    public float Speed { get{ return speed; } }

    [Header("Particles")]
    // Exhaust fumes
    [SerializeField] ParticleSystem[] gasParticles;

    [Header("Boost")]
    // Disable boost
    [HideInInspector] public bool allowBoost = true;

    // Current boost available
    [SerializeField] float boost = 0;
    public float Boost { get { return boost; } set { boost = Mathf.Clamp(value, 0f, VehicleExtention.MaxBoost); } }

    // Regen boostRegen per second until it's back to maxBoost
    [Range(0f, 1f)]
    [SerializeField] float boostRegen = 0.2f;
    public float BoostRegen { get { return boostRegen; } set { boostRegen = Mathf.Clamp01(value); } }

    /*
        *  The force applied to the car when boosting
        *  NOTE: the boost does not care if the car is grounded or not
        */
    [SerializeField] float boostForce = 5000;
    public float BoostForce { get { return boostForce; } set { boostForce = value; } }

    // Use this to boost when IsPlayer is set to false
    public bool boosting = false;
    // Use this to jump when IsPlayer is set to false
    public bool jumping = false;

    // Boost particles and sound
    [SerializeField] ParticleSystem[] boostParticles;
    [SerializeField] AudioClip boostClip;
    [SerializeField] AudioSource boostSource;
        
    // Private variables set at the start
    Rigidbody _rb;
    WheelCollider[] wheels;

    private BackLights backLights;

    // Init rigidbody, center of mass, wheels and more
    void Start() {
        if (boostClip != null) 
            boostSource.clip = boostClip;

		boost = VehicleExtention.MaxBoost;

        _rb = GetComponent<Rigidbody>();

        if (_rb != null && centerOfMass != null)
            _rb.centerOfMass = centerOfMass.localPosition;

        wheels = GetComponentsInChildren<WheelCollider>();

        // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
        foreach (WheelCollider wheel in wheels) {
            wheel.motorTorque = 0.0001f;
            wheel.ConfigureVehicleSubsteps(5, 12, 16);
        }

        backLights = GetComponentInChildren<BackLights>();
    }


    void Update() {
        foreach (ParticleSystem gasParticle in gasParticles) {
            gasParticle.Play();
            ParticleSystem.EmissionModule em = gasParticle.emission;
            em.rateOverTime = handbrake ? 0 : Mathf.Lerp(em.rateOverTime.constant, Mathf.Clamp(150.0f * throttle, 30.0f, 100.0f), 0.1f);
        }
    }
        
        
    void FixedUpdate () {
        // Mesure current speed
        speed = transform.InverseTransformDirection(_rb.velocity).z * 3.6f;

        // Get all the inputs!
        if (!controlType.isAI()) {
            handbrake = InputManager.isActive(InputManager.Break);
            // Accelerate & brake
            throttle = InputManager.Input(InputManager.Throttle) - InputManager.Input(InputManager.Break);
            // Boost
            boosting = InputManager.isActive(InputManager.Boost);
            // Turn
            steering = turnInputCurve.Evaluate(InputManager.Input(InputManager.Turn)) * steerAngle;
            // Dirft
            drift = InputManager.isActive(InputManager.Drift) && (_rb.velocity.sqrMagnitude > 100) || handbrake;
            // Jump
            jumping = InputManager.isActive(InputManager.Jump);

            backLights.isEnabled = handbrake || (throttle < 0);
        }

        /// Поворот
        foreach (WheelCollider wheel in turnWheel) {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, steerSpeed);
        }

        foreach (WheelCollider wheel in wheels) {
            wheel.brakeTorque = 0;
        }

        /// Тормоз
        if (handbrake) {
            foreach (WheelCollider wheel in wheels) {
                // Don't zero out this value or the wheel completly lock up
                wheel.motorTorque = 0.0001f;
                wheel.brakeTorque = brakeForce;
            }
        }
        else if (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle)) {
            foreach (WheelCollider wheel in driveWheel) 
                wheel.motorTorque =  (Mathf.Abs(speed) < maxSpeed ? throttle : 0) * motorTorque.Evaluate(speed) * diffGearing / driveWheel.Length;
        }
        else {
            foreach (WheelCollider wheel in wheels) {
                wheel.brakeTorque = Mathf.Abs(throttle) * brakeForce;
            }
        }

        /// Прыжок
        if (jumping && !controlType.isAI()) {
            if (!IsGrounded) return;
            _rb.velocity += transform.up * jumpVel;
        }

        /// Нитро
        if (boosting && allowBoost && boost > 0) {
            
            if (Mathf.Abs(speed) < maxSpeed + boostSpeedIncrease)
                _rb.AddForce(transform.forward * boostForce);

            boost = Mathf.Max(0, boost - Time.fixedDeltaTime);
            
            if (controlType.isPlayer()) 
                GameCanvasManager.Shared.SetNitro(boost / VehicleExtention.MaxBoost);

            if (boostParticles.Length > 0 && !boostParticles[0].isPlaying)
                foreach (ParticleSystem boostParticle in boostParticles)
                    boostParticle.Play();

            if (boostSource != null && !boostSource.isPlaying) 
                boostSource.Play();

        } else {
            if (boostParticles.Length > 0 && boostParticles[0].isPlaying)
                foreach (ParticleSystem boostParticle in boostParticles)
                    boostParticle.Stop();

            if (boostSource != null && boostSource.isPlaying)
                boostSource.Stop();
        }

        /// Дрифт
        if (drift && allowDrift) {
            Vector3 driftForce = -transform.right;
            driftForce.y = 0.0f;
            driftForce.Normalize();

            if (steering != 0) driftForce *= _rb.mass * speed/7f * throttle * steering/steerAngle;
            Vector3 driftTorque = transform.up * 0.1f * steering/steerAngle;

            _rb.AddForce(driftForce * driftIntensity, ForceMode.Force);
            _rb.AddTorque(driftTorque * driftIntensity, ForceMode.VelocityChange);             
        }
    }

    public void deactivateHandbreak() {
        handbrake = false;
    }

    public void activateHandbrake() {
        handbrake = true;
    }
}
