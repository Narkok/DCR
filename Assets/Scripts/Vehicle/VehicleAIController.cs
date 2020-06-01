using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VehicleAIController: MonoBehaviour {

    enum Purpose {
        ReachPoint,
        Wehicle,
        Inaction
    }

    private WheelController _wheelController;
    private WeaponController _weaponController;
    private Transform _transform;

    [SerializeField] private Purpose currentPurpose = Purpose.Inaction;

    [Range(0.5f, 4)]
    [SerializeField] private float reactionTime = 1;

    /// Всякие локальные данные для вичислений
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vehicle targetVehicle;

    /// Дебажный вывод в инспектор
    [SerializeField] private float debugValue;


    private void Awake() {
        _wheelController = GetComponent<WheelController>();
        _weaponController = GetComponent<WeaponController>();
        _transform = transform;
    }


    private void Start() {
        StartCoroutine(startCalculating());
    }


    /// Вичесление целей по времени реакции
    private IEnumerator startCalculating() {
        calculatePurpose();
        yield return new WaitForSeconds(reactionTime);
        StartCoroutine(startCalculating());
    }


    /// Тут вычисляются цели, типа, до какой точки доехать, какую машику преследовать, обработка состояния и расчет нужд
    private void calculatePurpose() {
        
        Vector3 currentPos = _transform.position;
        
        Equipment closestEquipment = SceneManager.Shared.Equipment[0];
        float minDistance = Vector3.Distance(currentPos, closestEquipment.transform.position);

        foreach (Equipment equipment in SceneManager.Shared.Equipment)
        {
            float distance = Vector3.Distance(currentPos, equipment.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closestEquipment = equipment;
            }
        }
    
        currentPurpose = Purpose.ReachPoint;
        targetPosition = closestEquipment.transform.position;
    }


    /// Тут контролируется процесс выполнения вычисленной цели
    private void Update() {
        _wheelController.Throttle = 0.7f;
        switch (currentPurpose) {
            case Purpose.Inaction:
                break;

            case Purpose.Wehicle:
                if (targetVehicle == null) currentPurpose = Purpose.Inaction;
                reachPoint(targetVehicle.transform.position);
                break;

            case Purpose.ReachPoint:
                reachPoint(targetPosition);
                break;
        }
    }


    private void reachPoint(Vector3 targetPosition) {
        /// Вытащил, чтобы меньше обращений было
        Vector3 up = _transform.up;
        Vector3 right = _transform.right;
        Vector3 forward = _transform.forward;
        Vector3 position = _transform.position;

        /// Целевле направление
        Vector3 targetDirection = (targetPosition - position).normalized;

        /// Проекция целевого направления на плоскость машины 
        float height = Mathf.Cos(Vector3.Angle(up, targetDirection) * Mathf.PI / 180);
        Vector3 projectDirection = (targetDirection - up * height).normalized;

        /// Угол поворота
        float angle = Vector3.Angle(forward, projectDirection);
        float dir = Vector3.Angle(right, projectDirection) < 90 ? 1 : -1;
        float result = angle * dir / 180;
        debugValue = result;

        _wheelController.Steering = result * 80;
    }


    /// Попытки в мультипоточность

    // private Thread calculatorThread;
    // private bool isDisabled;

    // private void startCalculating() {
    //     if (calculatorThread != null) calculatorThread.Abort();
    //     calculatorThread = new Thread(calculator);
    //     calculatorThread.Start();
    // }

    // private void calculator() {
    //     while(true) {
    //         calculatePurpose();
    //         Thread.Sleep(2000);
    //     }
    // }

    // private void OnDisable() {
    //     if (calculatorThread != null) calculatorThread.Abort();
    // }
}
