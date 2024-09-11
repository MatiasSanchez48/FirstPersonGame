using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    public bool IsInteractTriggered() => onFoot.Interact.triggered;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.OpenMainMenu.performed += ctx => motor.OpenMainMenu();

        onFoot.Crounch.performed += ctx => motor.Crounch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void FixedUpdate()
    {
        //tell the player motor to move using the value from our movement action.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable(); 
    }
}
