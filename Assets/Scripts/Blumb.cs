using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blumb: MonoBehaviour {
    
    private bool _isEnabled = false;
    private Vehicle _vehicle;
    private BlumbType _type;

    private LineRenderer _lineRenderer;
    private Transform _attachPoint;
    private const int _lineSegmentsCount = 6;
    private Vector3[] _linePositions = new Vector3[_lineSegmentsCount];
    private HingeJoint _joint;
    private Transform _blumb;

    [Range(1.5f, 4f)]
    [SerializeField] float lenght = 2.8f;
    public float Lenght {
        get { return lenght; }
        set { lenght = value; }
    }


    public void Set(Vehicle vehicle, BlumbType type) {
        _vehicle = vehicle;
        _type = type;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _lineSegmentsCount;
        _attachPoint = _vehicle.GetComponentInChildren<BlumbAttachPoint>().transform;
        _joint = GetComponent<HingeJoint>();
        Setup();
    }


    private void Clear() {
        _isEnabled = false;
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }


    private void Setup() {
        Clear();
        if (_type == BlumbType.None) return;
        _joint.connectedBody = _vehicle.GetComponent<Rigidbody>();
        _joint.connectedAnchor = _attachPoint.localPosition;
        _joint.axis = _attachPoint.right;
        _joint.anchor = -lenght * _attachPoint.up;
        _blumb = GOManager.Create(_type.Path(), transform).transform;
        _blumb.localRotation = _vehicle.transform.localRotation;
        _isEnabled = true;
    }


    private void Update() {
        UpdateLine();
    }


    private void UpdateLine() {
        if (!_isEnabled) return;
        for (int i = 0; i < _lineSegmentsCount; i++) {
            float t = i / (float)(_lineSegmentsCount - 1);
            Vector3 middlePoint = _attachPoint.position + _attachPoint.up * 0.3f;
            _linePositions[i] = BezierPoint(t, _attachPoint.position, middlePoint, _blumb.position);
        }
        _lineRenderer.SetPositions(_linePositions);
    }


    private Vector3 BezierPoint(float t, Vector3 start, Vector3 middle, Vector3 end) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return uu * start + 2 * u * t * middle + tt * end;
    }


    private void Awake() {
        Setup();
    }
}


public enum BlumbType {
    None,
    Skull,
    Luci,
    Creeper,
    Cupcake
}


public static class BlumbTypeExtention {
    public static string Path(this BlumbType type) {
        switch (type) {
            case BlumbType.Skull: { return "Blumbs/Skull"; }
            case BlumbType.Luci: { return "Blumbs/Luci"; }
            case BlumbType.Creeper: { return "Blumbs/Creeper"; }
            case BlumbType.Cupcake: { return "Blumbs/Cupcake"; }
            default: return "";
        }
    }
}
