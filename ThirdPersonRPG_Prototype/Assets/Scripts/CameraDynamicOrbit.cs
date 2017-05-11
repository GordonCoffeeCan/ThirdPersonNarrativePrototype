using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamicOrbit : MonoBehaviour {
    public Transform followingTarget;
    public float cameraDistance = 3;
    public float cameraMinDistance = 0.5f;
    public float cameraMaxDistance = 3;
    public float cameraRotationSpeed = 120;
    public float camDampSpeed = 0.25f;
    public float verticalMinAngle = -20;
    public float verticalMaxAngle = 80;

    private float _cameraOffset = 1;
    private Transform _camTrans;
    private Vector3 _veloctity = Vector3.zero;

    private RaycastHit _hit;

    private void Awake() {
        _camTrans = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        CameraRotate();
        CameraFollow();
        DynamicCameraDistance();
    }

    private void FixedUpdate() {
        cameraDistance = cameraMaxDistance;

        if (Physics.Raycast(this.transform.position, -this.transform.forward, out _hit)) {
            if(_hit.collider.gameObject.layer == 8) {
                if (_hit.distance < cameraMaxDistance) {
                    cameraDistance = _hit.distance - 0.01f;
                }

                Debug.Log(_hit.distance);
            }
        }
    }

    private void CameraRotate() {
        this.transform.Rotate(Input.GetAxis("Mouse Y") * cameraRotationSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime, 0);
        this.transform.localEulerAngles = new Vector3(AngleClamp(this.transform.localEulerAngles.x, verticalMinAngle, verticalMaxAngle), this.transform.localEulerAngles.y, 0);
    }

    private void CameraFollow() {
        if (followingTarget != null) {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(followingTarget.position.x, followingTarget.position.y + Mathf.Clamp(_cameraOffset / cameraDistance, 1.5f, 1.7f), followingTarget.position.z), ref _veloctity, camDampSpeed);
        }
    }

    private void DynamicCameraDistance() {
        cameraDistance = Mathf.Clamp(cameraDistance, cameraMinDistance, cameraMaxDistance);
        _camTrans.localPosition = Vector3.Lerp(_camTrans.localPosition, new Vector3(0, 0, -cameraDistance), 30 * Time.deltaTime);
    }

    private float AngleClamp(float _angle, float _min, float _max) {
        if (_angle >= 0 && _angle <= 90) {

            _angle = Mathf.Clamp(_angle, 0, _max);
        } else if (_angle >= 270 && _angle < 360) {
            _angle = Mathf.Clamp(_angle, 360 + _min, 360);
        }
        return _angle;
    }
}
