using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAIController: MonoBehaviour {

    private WheelController _wheelController;
    

    void Awake() {
        _wheelController = GetComponent<WheelController>();
        StartCoroutine(Turn());
    }


    private float steering = 1;


    void Update() {
        _wheelController.Throttle = 1;
        _wheelController.Steering = steering;
    }


    private IEnumerator Turn() {
        yield return new WaitForSeconds(Random.Range(1, 4));
        steering = Random.Range(-1f, 1f);
        StartCoroutine(Turn());
    }
}
