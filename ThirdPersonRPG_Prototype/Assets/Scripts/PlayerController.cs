using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    public float walkSpeed = 2;
    //public float runSpeed = 5;

    private float _gravity = 20;
    private float _rotationSpeed = 10;
    private float _currentSpeed = 0;

    private CharacterController _characterCtr;
    private Vector3 _moveDirection;
    private Vector3 _rotationDirection;
    private PlayerNetwork _playerNetwork;

    private void Awake() {
        _characterCtr = this.GetComponent<CharacterController>();
        _moveDirection = Vector3.zero;
    }

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        _playerNetwork = this.GetComponent<PlayerNetwork>();
    }

    // Update is called once per frame
    void Update () {
        if(_playerNetwork.isLocalInstance == true) {
            MoveCharacter();
        } else {
            return;
        }
        
    }

    private void MoveCharacter() {
        _currentSpeed = walkSpeed;

        if (_characterCtr.isGrounded) {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = Camera.main.transform.TransformDirection(_moveDirection);
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            _moveDirection *= _currentSpeed;

        }

        RotateCharacter(_moveDirection);

        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterCtr.Move(_moveDirection * Time.deltaTime);
    }

    private void RotateCharacter(Vector3 _moveDirection) {
        if (Mathf.Abs(_moveDirection.x) > 0.1f || Mathf.Abs(_moveDirection.z) > 0.1f) {
            _rotationDirection = Camera.main.transform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.z));
            _rotationDirection.y = 0;
            _rotationDirection.Normalize();
            _rotationDirection *= Time.deltaTime;

            _rotationDirection *= (Mathf.Abs(_moveDirection.x) > Mathf.Abs(_moveDirection.z)) ? Mathf.Abs(_moveDirection.x) : Mathf.Abs(_moveDirection.z);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, (Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg), 0)), _rotationSpeed * Time.deltaTime);
        }
    }
}
