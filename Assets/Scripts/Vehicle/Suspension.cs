using UnityEngine;

namespace VehicleBehaviour {
    [RequireComponent(typeof(WheelCollider))]

    public class Suspension : MonoBehaviour {
        public GameObject wheelModel;
        private WheelCollider _wheelCollider;
        public Vector3 localRotOffset;


        void Awake() {
            _wheelCollider = GetComponent<WheelCollider>();
        }
        

        void LateUpdate() {
            if (wheelModel && _wheelCollider) {
                Vector3 pos = new Vector3(0, 0, 0);
                Quaternion quat = new Quaternion();
                _wheelCollider.GetWorldPose(out pos, out quat);
                wheelModel.transform.rotation = quat;
                wheelModel.transform.localRotation *= Quaternion.Euler(localRotOffset);
                wheelModel.transform.position = pos;
            }
        }
    }
}
