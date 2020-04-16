using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blumb: MonoBehaviour {

    private Vehicle vehicle;
    private BlumbType type;

    [Range(1.5f, 4f)]
    [SerializeField] float lenght = 2.8f;
    public float Lenght {
        get { return lenght; }
        set { lenght = value; }
    }


    public void Set(Vehicle vehicle, BlumbType type) {
        this.vehicle = vehicle;
        this.type = type;
        Setup();
    }


    private void Clear() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    private void Setup() {
        Clear();
        if (type == BlumbType.None) return; 
        HingeJoint joint = GetComponent<HingeJoint>();
        Transform attachPoint = vehicle.transform.Find("BlumbAttachPoint").transform;
        joint.connectedBody = vehicle.GetComponent<Rigidbody>();
        joint.connectedAnchor = attachPoint.localPosition;
        joint.axis = attachPoint.transform.right;
        joint.anchor = -lenght * attachPoint.transform.up.normalized;
        GameObject blumb = GOManager.Create(type.Path(), transform);
        blumb.transform.localRotation = vehicle.transform.localRotation;
    }


    private void Awake() {
        Setup();
    }
}


public enum BlumbType {
    None,
    Skull,
    Luci,
    Creeper
}


public static class BlumbTypeExtention {
    public static string Path(this BlumbType type) {
        switch (type) {
            case BlumbType.Skull: { return "Blumbs/Skull"; }
            case BlumbType.Luci: { return "Blumbs/Luci"; }
            case BlumbType.Creeper: { return "Blumbs/Creeper"; }
            default: return "";
        }
    }
}
