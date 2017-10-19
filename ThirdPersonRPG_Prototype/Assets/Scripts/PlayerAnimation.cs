using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Transform leftLocator;
    [SerializeField]
    private Transform rightLocator;

    private CharacterController characterCtr;
    private PlayerController playerController;

    private Vector2 idleBlend = Vector2.zero;
    private Vector2 currentIdleBlend = Vector2.zero;
    private Vector2 horizontalSpeed = Vector2.zero;

    private int idleState = Animator.StringToHash("Base Layer.Human_Idle");
    private int joggingState = Animator.StringToHash("Base Layer.Jogging");
    private int runToStopState = Animator.StringToHash("Base Layer.RunToStop");
    private int hardLandingState = Animator.StringToHash("Base Layer.HardLanding");
    private int dashingState = Animator.StringToHash("Base Layer.Dashing");
    private int fallingToRunningState = Animator.StringToHash("Base Layer.FallingToRunning");

    private AnimatorStateInfo currentAnimatorState;

    [SerializeField]
    private float idleChangeTimer = 3;
    private float currentIdleChangeTimer;
    private float currentLocomotionSpeed = 0;

    [HideInInspector]
    public bool isHardLanding = false;

    private void Awake() {
        characterCtr = this.GetComponent<CharacterController>();
        playerController = this.GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start() {
        if (playerAnimator == null) {
            Debug.LogError("No player animator reference!");
            return;
        }

        currentIdleChangeTimer = idleChangeTimer;
    }

    // Update is called once per frame
    void Update() {
        ChangeIdleBlend();
        ChangeMotion();
        currentAnimatorState = playerAnimator.GetCurrentAnimatorStateInfo(0);
    }

    private void ChangeIdleBlend() {
        currentIdleChangeTimer -= Time.deltaTime;
        if (currentIdleChangeTimer <= 0) {
            idleBlend = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            currentIdleChangeTimer = idleChangeTimer;
        }
        currentIdleBlend = Vector2.Lerp(currentIdleBlend, idleBlend, 0.1f);
        playerAnimator.SetFloat("IdleBlendX", currentIdleBlend.x);
        playerAnimator.SetFloat("IdleBlendY", currentIdleBlend.y);
    }

    private void ChangeMotion() {
        horizontalSpeed = new Vector2(characterCtr.velocity.x, characterCtr.velocity.z);
        currentLocomotionSpeed = Mathf.Lerp(currentLocomotionSpeed, horizontalSpeed.magnitude, 0.5f);

        if (currentLocomotionSpeed < 0.1f) {
            currentLocomotionSpeed = 0;
        }

        playerAnimator.SetFloat("Speed", currentLocomotionSpeed);
        playerAnimator.SetBool("IsGrounded", characterCtr.isGrounded);
        playerAnimator.SetBool("InAir", playerController.isInMiddleAir);
        playerAnimator.SetBool("Gliding", playerController.isGlide);
        playerAnimator.SetBool("HardLanding", isHardLanding);

        if (currentAnimatorState.fullPathHash == runToStopState || currentAnimatorState.fullPathHash == hardLandingState) {
            playerController.isAbleToMove = false;
        }
        else {
            playerController.isAbleToMove = true;
        }

        if (currentAnimatorState.fullPathHash != dashingState && !playerAnimator.IsInTransition(0)) {
            playerController.isAbleToDash = true;
        }
        else {
            playerController.isAbleToDash = false;
        }

        //Only idle state will trigger jump action when press jump button;
        if ((currentAnimatorState.fullPathHash == idleState || currentAnimatorState.fullPathHash == joggingState)) {

        }
        //Only idle state will trigger jump action when press jump button;

        if (!playerAnimator.IsInTransition(0) && currentAnimatorState.fullPathHash != fallingToRunningState) {
            if (MobileInputManager.instance.isGamepadConnected == false) {

            }
            else {
                playerAnimator.SetBool("Jump", ControllerManager.instance.OnJump());
                playerController.toggleJump = ControllerManager.instance.OnJump();
            }
        }


        //Play Dash animation only player animator is in Jogging state and triggered dashing in PlayerController;
        if (currentAnimatorState.fullPathHash == joggingState && !playerAnimator.IsInTransition(0)) {
            if (playerController.isDashing) {
                playerAnimator.SetTrigger("Dashing");
            }
        }
        //Play Dash animation only player animator is in Jogging state and triggered dashing in PlayerController;
    }

   /*private float LeftToeAndRightToeOffset() {
        float _idle2Left = 0;

        if (leftLocator != null && rightLocator != null) {

            _idle2Left = Vector3.Angle((rightLocator.position - leftLocator.position), this.transform.forward);

        }
        else {
            Debug.LogError("No left or right locator reference!");
        }
        return _idle2Left;
    }*/
}
