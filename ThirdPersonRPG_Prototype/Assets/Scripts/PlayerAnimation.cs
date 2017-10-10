using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField] private Animator playerAnimator;

    private CharacterController characterCtr;
    private PlayerController playerController;

    private Vector2 idleBlend = Vector2.zero;
    private Vector2 currentIdleBlend = Vector2.zero;
    private Vector2 horizontalSpeed = Vector2.zero;

    private int jumpingState = Animator.StringToHash("Base Layer.Jumping");
    private int runningJumpingState = Animator.StringToHash("Base Layer.RunningJump");

    private AnimatorStateInfo currentAnimatorState;

    [SerializeField]private float idleChangeTimer = 3;
    private float currentIdleChangeTimer;
    private float currentLocomotionSpeed = 0;
    private float characterCtrHeight = 0;
    private float playerGravity = 0;
    private float currentCharacterControllerCenterOffsetY;
    private float currentCharacterControllerStepOffset;

    private void Awake() {
        characterCtr = this.GetComponent<CharacterController>();
        playerController = this.GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start () {
		if(playerAnimator == null) {
            Debug.LogError("No player animator reference!");
            return;
        }

        currentIdleChangeTimer = idleChangeTimer;
        characterCtrHeight = characterCtr.height;
        playerGravity = playerController.gravity;
        currentCharacterControllerCenterOffsetY = characterCtr.center.y;
        currentCharacterControllerStepOffset = characterCtr.stepOffset;
    }
	
	// Update is called once per frame
	void Update () {
        ChangeIdleBlend();
        ChangeMotion();
        currentAnimatorState = playerAnimator.GetCurrentAnimatorStateInfo(0);
    }

    private void ChangeIdleBlend() {
        currentIdleChangeTimer -= Time.deltaTime;
        if(currentIdleChangeTimer <= 0) {
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
        playerAnimator.SetBool("IsGround", characterCtr.isGrounded);

        if (MobileInputManager.instance.isGamepadConnected == false) {

        } else {
            playerAnimator.SetBool("Jump", ControllerManager.instance.OnJump());
        }
    }
}
