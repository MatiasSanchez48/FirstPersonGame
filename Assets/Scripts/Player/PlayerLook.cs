using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera Cam;
    private float xRotation = 0f;

    public float xSensivity = 30f;
    public float ySensivity = 30f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculaye camera rotation fort looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80f);
        //apply this to our camera tansform.
        Cam.transform.localRotation = Quaternion.Euler(xRotation ,0,0);
        //rotate player to look and right.
        transform.Rotate(Vector3.up *(mouseX *  Time.deltaTime) * xSensivity);

    }
}
