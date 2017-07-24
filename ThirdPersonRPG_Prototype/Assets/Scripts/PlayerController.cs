using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    public float walkSpeed = 2;
    public float runSpeed = 5;

    [SerializeField]
    private Transform rotationPivot;

    private float gravity = 20;
    private float rotationSpeed = 10;
    private float currentSpeed = 0;

    private CharacterController _characterCtr;
    
    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    private void Awake() {
        _characterCtr = this.GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
    }

    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        CameraDynamicOrbit.followingTarget = this.transform;
    }

    // Update is called once per frame
    void Update () {
        MoveCharacter();
    }

    private void MoveCharacter() {
        currentSpeed = walkSpeed;

        if (_characterCtr.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;
            moveDirection.Normalize();
            moveDirection *= currentSpeed;

        }

        RotateCharacter(moveDirection);

        moveDirection.y -= gravity * Time.deltaTime;
        _characterCtr.Move(moveDirection * Time.deltaTime);
    }

    private void RotateCharacter(Vector3 _moveDirection) {
        if (Mathf.Abs(_moveDirection.x) > 0.1f || Mathf.Abs(_moveDirection.z) > 0.1f) {
            rotationDirection = Camera.main.transform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.z));
            rotationDirection.y = 0;
            rotationDirection.Normalize();
            rotationDirection *= Time.deltaTime;

            rotationDirection *= (Mathf.Abs(_moveDirection.x) > Mathf.Abs(_moveDirection.z)) ? Mathf.Abs(_moveDirection.x) : Mathf.Abs(_moveDirection.z);

            rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, (Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg), 0)), rotationSpeed * Time.deltaTime);
        }
    }
}
