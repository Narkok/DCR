using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arena: MonoBehaviour {

    [SerializeField] private GravityProcessor gravityProcessor;
    public GravityProcessor GravityProcessor { get { return gravityProcessor; } }

}
