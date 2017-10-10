using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 6;
    [SerializeField] private float dashSpeed = 15;
    [SerializeField] private float glideSpeed = 4;
    [SerializeField] private float jumpSpeed = 8;
    [SerializeField] private float sprintTime = 1.65f;

    public Transform rotationPivot;
    public CameraDynamicOrbit cameraPivot;

    [HideInInspector] public float popSpeed;
    [HideInInspector] public float currentGlidingGraivity;
    [HideInInspector] public float gravity = 20;

    [HideInInspector] public bool isPopped = false;
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool toggleJump = false;

    [SerializeField] private Camera playerCamera;

    private float glidingGraivty = 2;
    private float rotationSpeed = 15;
    private float aimRotationSpeed = 40;
    private float currentSpeed = 0;
    private float currentVerticalSpeed = 0;

    private const float MINIMUM_SPEED_TO_GLIDE = -6.5f;

    protected CharacterController characterCtr;
    
    private Vector3 moveDirection;
    private Vector3 rotationDirection;
    private float sprintTimeLimit;

    private bool isReadyGlide = false;
    private bool isGlide = false;

    Vector3 currentDirection = Vector3.zero;

    private void Awake() {
        characterCtr = this.GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        sprintTimeLimit = sprintTime;
        currentGlidingGraivity = glidingGraivty;
    }

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        //If in Game menu panel is on, player cannot be controlled;
        if (MultiplayerUIManager.isMenuPanelOn) {
            return;
        }
        DetectOnGround();
        MoveCharacter();
        SprintLevel();
    }

    private void MoveCharacter() {

        currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, 0.2f);
        OnSprint();

        isReadyGlide = false;
        isGlide = false;

        if (MobileInputManager.instance.isGamepadConnected == false) {
            moveDirection = MobileInputManager.instance.OnJoystickMove();
        } else {
            moveDirection = ControllerManager.instance.OnMove();
        }
        moveDirection = playerCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();

        moveDirection *= currentSpeed;

        if (characterCtr.isGrounded) {
            currentVerticalSpeed = 0;
            if (toggleJump) {
                currentVerticalSpeed = jumpSpeed;
            }
        }

        currentVerticalSpeed -= gravity * Time.deltaTime;
        moveDirection.y = currentVerticalSpeed;
        characterCtr.Move(moveDirection * Time.deltaTime);
        RotateCharacter(moveDirection);

        Debug.Log("Player is on the ground: " + characterCtr.isGrounded + " and Vertical Speed is " + moveDirection.y);

        /*if (characterCtr.isGrounded) {
            isReadyGlide = false;
            isGlide = false;

            if (MobileInputManager.instance.isGamepadConnected == false) {
                moveDirection = MobileInputManager.instance.OnJoystickMove();
            } else {
                moveDirection = ControllerManager.instance.OnMove();
            }

            moveDirection = playerCamera.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;
            moveDirection.Normalize();

            currentDirection = moveDirection;

            moveDirection *= currentSpeed;

            if (isPopped) {
                moveDirection.y = popSpeed;
                isPopped = false;
            }
        } /*else {
            currentDirection.Normalize();
            moveDirection = new Vector3(currentDirection.x * currentSpeed, moveDirection.y, currentDirection.z * currentSpeed);

            //In the air, the player has to release jump button once.
            if (ControllerManager.instance.OnReadyGlide() == true) {
                isReadyGlide = true;
            }

            //When the player reaches the minimum vertical speed to glide, press jump button again to glide;
            //While gliding, player can press jump button again to toggle between gliding and falling;
            if (isReadyGlide == true && ControllerManager.instance.OnGlide() == true && isGlide == false && moveDirection.y < MINIMUM_SPEED_TO_GLIDE) {
                isGlide = true;
            } else if (isGlide == true && ControllerManager.instance.OnGlide() == true) {
                isGlide = false;
            }
        }*/

        /*if (isGlide) {
            OnGlide();
        } else {
            OnDash();
            moveDirection.y -= gravity * Time.deltaTime;

            Debug.Log(gravity);
        }*/
    }

    private void DetectOnGround() {
        Vector3 _rayPosition = this.transform.position + characterCtr.center;
        if (Physics.Raycast(_rayPosition, Vector3.down, 1f)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        Debug.DrawRay(_rayPosition, Vector3.down * 1f, Color.cyan);
    }

    private void RotateCharacter(Vector3 _direction) {

        //No Aiming, Player will facing to it's movement direction;
        if (Mathf.Abs(_direction.x) > 0.1f || Mathf.Abs(_direction.z) > 0.1f) {
            rotationDirection = playerCamera.transform.TransformDirection(new Vector3(_direction.x, 0, _direction.z));
            rotationDirection.y = 0;
            rotationDirection.Normalize();
            rotationDirection *= Time.deltaTime;

            rotationDirection *= (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.z)) ? Mathf.Abs(_direction.x) : Mathf.Abs(_direction.z);

            

            if (MobileInputManager.instance.isGamepadConnected == false) {
                if (MobileInputManager.instance.isAim == false && MobileInputManager.instance.OnFire() == false) {
                    rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, (Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg), 0)), rotationSpeed * Time.deltaTime);
                }
            } else {
                if (ControllerManager.instance.OnAim() == false && ControllerManager.instance.OnFire() == false) {
                    rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, (Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg), 0)), rotationSpeed * Time.deltaTime);
                }
            }
        }

        //On Aiming, Player rotation follows along with camera direction;
        if (MobileInputManager.instance.isGamepadConnected == false) {
            if (MobileInputManager.instance.isAim == true || MobileInputManager.instance.OnFire() == true) {
                rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, cameraPivot.transform.localEulerAngles.y + this.transform.eulerAngles.y, 0)), aimRotationSpeed * Time.deltaTime);
            }
        } else {
            if (ControllerManager.instance.OnAim() == true || MobileInputManager.instance.OnFire() == true) {
                rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.Euler(new Vector3(0, cameraPivot.transform.localEulerAngles.y + this.transform.eulerAngles.y, 0)), aimRotationSpeed * Time.deltaTime);
            }
        }
        
    }

    private void OnSprint() {
        if (MobileInputManager.instance.isGamepadConnected == false) {
            SprintStamina(MobileInputManager.instance.OnSprint(), MobileInputManager.instance.OnJoystickMove(), characterCtr.isGrounded);
        } else {
            SprintStamina(ControllerManager.instance.OnSprint(), ControllerManager.instance.OnMove(), characterCtr.isGrounded);
        }
    }

    private void SprintStamina(bool _isOnSprint, Vector3 _moveDirection, bool _isOnGround) {
        if (_isOnSprint == true && _moveDirection.magnitude >= 0.8f && _isOnGround == true) {
            if (sprintTime >= 0) {
                sprintTime -= Time.deltaTime;
                currentSpeed = runSpeed;
            }
        } else {
            if (sprintTime < sprintTimeLimit) {
                sprintTime += Time.deltaTime;
            } else if (sprintTime >= sprintTimeLimit) {
                sprintTime = sprintTimeLimit;
            }
        }
    }

    private void OnDash() {
        if (ControllerManager.instance.OnDash() && sprintTime / sprintTimeLimit >= 1 && moveDirection.magnitude >= 0.8f) {
            sprintTime = 0;
            currentSpeed = dashSpeed;
        }
    }

    private void OnGlide () {
        if (currentGlidingGraivity < glidingGraivty) {
            currentGlidingGraivity += 0.2f;
        } else if(currentGlidingGraivity >= glidingGraivty) {
            currentGlidingGraivity = glidingGraivty;
        }

        currentSpeed = glideSpeed;
        if (MobileInputManager.instance.isGamepadConnected == false) {
            moveDirection = MobileInputManager.instance.OnJoystickMove();
        } else {
            moveDirection = ControllerManager.instance.OnMove();
        }
        moveDirection = playerCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();
        currentDirection = moveDirection;
        moveDirection *= currentSpeed;
        moveDirection.y = -currentGlidingGraivity;
    }

    private void SprintLevel() {
        MultiplayerGameManager.instance.staminaLevel = sprintTime / sprintTimeLimit;
    }
}
