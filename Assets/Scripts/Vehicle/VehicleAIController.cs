using System.Collections;
using System.Collections.Generic;
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
        
        if (SceneManager.Shared.Equipment.Count == 0) {
            currentPurpose = Purpose.Inaction;
            return;
        }

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
        switch (currentPurpose) {
            case Purpose.Inaction:
                inaction();
                break;

            case Purpose.Wehicle:
                if (targetVehicle == null) currentPurpose = Purpose.Inaction;
                reachPoint(targetVehicle.transform.position);
                break;

            case Purpose.ReachPoint:
                reachPoint(targetPosition);
                _weaponController.ShootFromSelectedWeapon();
                break;
        }
    }


    /// Достижение позиции
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
        float normalizedAngle = Vector3.Angle(forward, projectDirection) / 180;
        float dir = Vector3.Angle(right, projectDirection) < 90 ? 1 : -1;
        float result = normalizedAngle * dir;
        debugValue = result;

        _wheelController.Throttle = 0.7f;
        _wheelController.Steering = result * 8;
        _wheelController.boosting = Mathf.Abs(normalizedAngle) < 0.05f; 
    }


    /// Бездействие
    private void inaction() {
        _wheelController.Steering = 0;
        _wheelController.boosting = false;
        _wheelController.Throttle = 0;
    }
}
