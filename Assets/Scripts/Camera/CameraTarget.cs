using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraTarget: MonoBehaviour {
    [SerializeField] public Transform Target = null;
    [SerializeField] public float SmoothTime = 0;
}
