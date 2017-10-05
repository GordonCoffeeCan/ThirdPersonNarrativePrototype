using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPlayerAnimation : MonoBehaviour {

    [SerializeField]private Animator playerAnimator;

    private CharacterController charCtr;

    private Vector2 idleBlend = Vector2.zero;
    private Vector2 currentIdleBlend = Vector2.zero;
    private Vector2 horizontalSpeed = Vector2.zero;

    [SerializeField]private float idleChangeTimer = 3;
    private float currentIdleChangeTimer;
    private float currentSpeed = 0;

    private void Awake() {
        charCtr = this.GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start () {
		if(playerAnimator == null) {
            Debug.LogError("No player animator reference!");
            return;
        }

        currentIdleChangeTimer = idleChangeTimer;

    }
	
	// Update is called once per frame
	void Update () {
        ChangeIdleBlend();
        ChangeMotion();
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
        horizontalSpeed = new Vector2(charCtr.velocity.x, charCtr.velocity.z);
        currentSpeed = Mathf.Lerp(currentSpeed, horizontalSpeed.magnitude, 0.5f);

        if (currentSpeed < 0.1f) {
            currentSpeed = 0;
        }

        playerAnimator.SetFloat("Speed", currentSpeed);
        playerAnimator.SetBool("IsGround", charCtr.isGrounded);

        if (MobileInputManager.instance.isGamepadConnected == false) {
            
        } else {
            playerAnimator.SetBool("Jump", ControllerManager.instance.OnJump());
        }

        //Debug.Log(charCtr.velocity.y);
    }
}
