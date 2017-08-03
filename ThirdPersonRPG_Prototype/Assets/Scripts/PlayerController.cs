﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [SerializeField]
    private float walkSpeed = 2;
    [SerializeField]
    private float runSpeed = 5;
    [SerializeField]
    private float jumpSpeed = 8;

    public Transform rotationPivot;

    [SerializeField]
    private CameraDynamicOrbit cameraPivot;

    [SerializeField]
    private Camera playerCamera;

    private float gravity = 20;
    private float rotationSpeed = 15;
    private float aimRotationSpeed = 40;
    private float currentSpeed = 0;

    private CharacterController _characterCtr;
    
    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    private void Awake() {
        instance = this;
        _characterCtr = this.GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
    }

    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update () {

        //If in Game menu panel is on, player cannot be controlled;
        if (UIManager.isMenuPanelOn) {
            return;
        }
        MoveCharacter();
    }

    private void MoveCharacter() {
        currentSpeed = walkSpeed;

        if (_characterCtr.isGrounded) {
            moveDirection = ControllerManager.instacne.OnMove();
            moveDirection = playerCamera.transform.TransformDirection(moveDirection);
            moveDirection.Normalize();
            moveDirection *= currentSpeed;
            if (ControllerManager.instacne.OnJump() == true) {
                moveDirection.y = jumpSpeed;
            } else {
                moveDirection.y = 0;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        _characterCtr.Move(moveDirection * Time.deltaTime);

        RotateCharacter(moveDirection);
    }

    private void RotateCharacter(Vector3 _direction) {

        //No Aiming, Player will facing to it's movement direction;
        if (Mathf.Abs(_direction.x) > 0.1f || Mathf.Abs(_direction.z) > 0.1f) {
            rotationDirection = playerCamera.transform.TransformDirection(new Vector3(_direction.x, 0, _direction.z));
            rotationDirection.y = 0;
            rotationDirection.Normalize();
            rotationDirection *= Time.deltaTime;

            rotationDirection *= (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.z)) ? Mathf.Abs(_direction.x) : Mathf.Abs(_direction.z);

            if(ControllerManager.instacne.OnAim() == false && ControllerManager.instacne.OnFire() == false) {
                rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, (Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg), 0)), rotationSpeed * Time.deltaTime);
            }
        }

        //On Aiming, Player rotation follow along with camera direction;
        if (ControllerManager.instacne.OnAim() == true || ControllerManager.instacne.OnFire() == true) {
            rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, cameraPivot.transform.localEulerAngles.y + this.transform.eulerAngles.y, 0)), aimRotationSpeed * Time.deltaTime);
        }
    }
}
