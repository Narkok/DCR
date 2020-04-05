using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blumb : MonoBehaviour
{

    public WheelVehicle vehicle;
    public BlumbType type;

    [Range(1.5f, 4f)]
    [SerializeField] float lenght = 2.8f;
    public float Lenght {
        get { return lenght; }
        set { lenght = value; }
    }


    public void Set(WheelVehicle vehicle, BlumbType type) {
        this.vehicle = vehicle;
        this.type = type;
    }


    private void Awake() {
        if (type == BlumbType.None) return; 
        HingeJoint joint = GetComponent<HingeJoint>();
        Transform attachPoint = vehicle.transform.Find("BlumbAttachPoint").transform;
        joint.connectedBody = vehicle.GetComponent<Rigidbody>();
        joint.connectedAnchor = attachPoint.localPosition;
        joint.axis = attachPoint.transform.right;
        joint.anchor = new Vector3(1, -lenght, 0);
        GameObject blumb = GOManager.Create(type.Path(), transform);
        blumb.transform.localRotation = vehicle.transform.localRotation;
    }
}


public enum BlumbType {
    None,
    Skull
}


public static class BlumbTypeExtention {

    public static string Path(this BlumbType type) {
        switch (type) {
            case BlumbType.Skull: { return "Blumbs/Skull"; }
            default: return "";
        }
    }
}
