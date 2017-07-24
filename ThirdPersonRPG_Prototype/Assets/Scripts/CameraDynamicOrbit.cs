using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamicOrbit : MonoBehaviour {
    public static Transform followingTarget;
    public float cameraMinDistance = 0.5f;
    public float cameraMaxDistance = 3;
    public float cameraRotationSpeed = 120;
    public float camDampTime = 0.15f;
    public float verticalMinAngle = -20;
    public float verticalMaxAngle = 80;

    [SerializeField]
    private bool DampCamera = false;

    private float _cameraDistance = 3;
    private float _cameraVerticalOffset = 1.65f;
    private Transform _camTrans;
    private Vector3 _veloctity = Vector3.zero;

    private RaycastHit _hit;

    private void Awake() {
        _camTrans = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        this.transform.position = new Vector3(0, _cameraVerticalOffset, 0);
    }
	
	// Update is called once per frame
	void Update () {
        CameraRotate();
        CameraFollow();
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
        this.transform.Rotate(Input.GetAxis("CamVertical") * cameraRotationSpeed * Time.deltaTime, Input.GetAxis("CamHorizontal") * cameraRotationSpeed * Time.deltaTime, 0);
        this.transform.localEulerAngles = new Vector3(AngleClamp(this.transform.localEulerAngles.x, verticalMinAngle, verticalMaxAngle), this.transform.localEulerAngles.y, 0);
    }

    private void CameraFollow() {
        if (followingTarget != null) {
            Vector3 targetPostion = new Vector3(followingTarget.position.x, followingTarget.position.y + _cameraVerticalOffset, followingTarget.position.z);
            if (DampCamera == true) {
                this.transform.parent = null;
                this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPostion, ref _veloctity, camDampTime);
            } else {
                this.transform.parent = followingTarget;
            }
            
        }
    }

    private void DynamicCameraDistance() {
        _cameraDistance = Mathf.Clamp(_cameraDistance, cameraMinDistance, cameraMaxDistance);
        _camTrans.localPosition = Vector3.Lerp(_camTrans.localPosition, new Vector3(0, 0, -_cameraDistance), 30 * Time.deltaTime);
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
