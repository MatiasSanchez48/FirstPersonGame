using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crounchTimer = 3f;
    public bool lerpCrounch= false;
    public bool sprinting= false;
    public bool crounching= false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrounch)
        {
            crounchTimer += Time.deltaTime;
            float p = crounchTimer / 1;
            p *= p;
            if (crounching) controller.height = Mathf.Lerp(controller.height, 1, p);
            else controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrounch = false;
                crounchTimer = 0f;
            }
        }
    }
    /// <summary>
    /// Receive the inputs for out inputManager.cs and apply them to our character controller.
    /// </summary>
    /// <param name="input"></param>
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Crounch()
    {
        crounching = !crounching;
        crounchTimer = 0;
        lerpCrounch = true;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting) 
            speed = 8; 
        else speed = 5; 

    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
