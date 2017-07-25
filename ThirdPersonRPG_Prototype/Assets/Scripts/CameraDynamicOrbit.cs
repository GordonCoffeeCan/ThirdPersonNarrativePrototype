using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamicOrbit : MonoBehaviour {
    public static CameraDynamicOrbit instance;
    public static Transform followingTarget;
    public float cameraMinDistance = 0.5f;
    public float cameraMaxDistance = 3;
    public float cameraAimMaxDistance = 1;
    public float cameraRotationSpeed = 120;
    public float camDampTime = 0.15f;
    public float verticalMinAngle = -20;
    public float verticalMaxAngle = 80;
    public bool isAiming = false;

    [SerializeField]
    private bool DampCamera = false;

    private float _cameraDistance;
    private float _cameraHorizontalOffset;
    private float _cameraVerticalOffset = 1.65f;
    private Transform _camTrans;
    private Vector3 _veloctity = Vector3.zero;

    private float cameraOriginalMaxDistance;

    private RaycastHit _hit;

    private ControllerAxis controllerAxis = new ControllerAxis();

    private void Awake() {
        instance = this;
        _camTrans = this.transform.Find("PlayerCamera");
    }

    // Use this for initialization
    void Start () {
        this.transform.position = new Vector3(0, _cameraVerticalOffset, 0);
        cameraOriginalMaxDistance = cameraMaxDistance;
    }
	
	// Update is called once per frame
	void Update () {
        CameraRotate();
        CameraFollow();
        CameraAiming();
        DynamicCameraDistance();
    }

    private void FixedUpdate() {
        _cameraDistance = cameraMaxDistance;

        if (Physics.Raycast(this.transform.position, -this.transform.forward, out _hit)) {
            if(_hit.collider.gameObject.layer == 8) {
                if (_hit.distance < cameraMaxDistance) {
                    _cameraDistance = _hit.distance - 0.5f;
                }
            }
        }
    }

    private void CameraRotate() {
        this.transform.Rotate(Input.GetAxis(controllerAxis.cameraVerticalAxis) * cameraRotationSpeed * Time.deltaTime, Input.GetAxis(controllerAxis.cameraHorizontalAxis) * cameraRotationSpeed * Time.deltaTime, 0);
        this.transform.localEulerAngles = new Vector3(AngleClamp(this.transform.localEulerAngles.x, verticalMinAngle, verticalMaxAngle), this.transform.localEulerAngles.y, 0);
    }

    private void CameraFollow() {
        if (followingTarget != null) {
            Vector3 targetPostion = new Vector3(followingTarget.position.x, followingTarget.position.y + _cameraVerticalOffset, followingTarget.position.z);
            if (DampCamera == true) {
                this.transform.parent = null;
                this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPostion, ref _veloctity, camDampTime);
            } else {
                this.transform.position = targetPostion;
                this.transform.parent = followingTarget;
            }
        } else {
            this.transform.localPosition = new Vector3(0, _cameraVerticalOffset, 0);
        }
    }

    private void CameraAiming() {
        if (Input.GetButton(controllerAxis.aimButton) || Input.GetAxis(controllerAxis.aimTrigger) > 0.2f) {
            isAiming = true;
            _cameraHorizontalOffset = 0.3f;
            cameraMaxDistance = cameraAimMaxDistance;
        } else {
            isAiming = false;
            _cameraHorizontalOffset = 0;
            cameraMaxDistance = cameraOriginalMaxDistance;
        }
    }

    private void DynamicCameraDistance() {
        _cameraDistance = Mathf.Clamp(_cameraDistance, cameraMinDistance, cameraMaxDistance);
        _camTrans.localPosition = Vector3.Lerp(_camTrans.localPosition, new Vector3(_cameraHorizontalOffset, 0, -_cameraDistance), 30 * Time.deltaTime);
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
