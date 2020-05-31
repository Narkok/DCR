using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VehicleAIController: MonoBehaviour {

    enum Purpose {
        ReachPoint,
        Inaction
    }

    private WheelController _wheelController;
    private WeaponController _weaponController;

    [SerializeField] private Purpose currentPurpose = Purpose.Inaction;

    private Thread calculatorThread;
    private bool isDisabled;


    private void Awake() {
        _wheelController = GetComponent<WheelController>();
        _weaponController = GetComponent<WeaponController>();
        
    }


    private void Start() {
        startCalculating();
    }


    private void startCalculating() {
        if (calculatorThread != null) calculatorThread.Abort();
        calculatorThread = new Thread(calculator);
        calculatorThread.Start();
    }


    private void calculator() {
        while(true) {
            calculatePurpose();
            Thread.Sleep(2000);
        }
    }


    /// Тут вычисляются цели, типа, до какой точки доехать, какую машику преследовать, обработка состояния и расчет нужд
    private void calculatePurpose() {
    }


    /// Тут контролируется процесс выполнения вычисленной цели
    private void Update() {
        _wheelController.Throttle = 1;
    }


    /// На выходе убиваем поток калькулятора
    private void OnDisable() {
        if (calculatorThread != null) calculatorThread.Abort();
    }
}
