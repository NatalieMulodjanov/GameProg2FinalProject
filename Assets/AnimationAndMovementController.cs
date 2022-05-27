using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInputAnimation playerInput;
    CharacterController characterController;
    Animator animator; 

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 1.0f;

    void Awake(){
        playerInput = new PlayerInputAnimation();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
    }

    void handleRotation() {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }

    void OnMovementInput(InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void handleAnimation() {
        bool isRunning = animator.GetBool("isRunning");

        if(isMovementPressed && !isRunning) {
            animator.SetBool("isRunning", true);
        }
        if(!isMovementPressed && isRunning) {
            animator.SetBool("isRunning", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //handleRotation();
        handleAnimation();
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}
