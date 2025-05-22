using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerCameraFirst;
    [SerializeField] Transform playerCameraThird;
    [SerializeField] Camera playerCamera;
    int sensitivity = 10;

    float xRotation;
    float yRotation;

    float mouseX;
    float mouseY;

    bool isFirstPerson;

    void Start()
    {
        isFirstPerson = true;
        SwitchToFirstPerson();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnLook(InputValue input)
    {
        Vector2 lookInput = input.Get<Vector2>();
        mouseX = lookInput.x;
        mouseY = lookInput.y;
    }

    void ApplyRotation()
    {
        float scaledMouseX = mouseX * sensitivity * Time.deltaTime;
        float scaledMouseY = mouseY * sensitivity * Time.deltaTime;

        
        xRotation -= scaledMouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 40f); 

        yRotation += scaledMouseX;

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        if(isFirstPerson)
        {
            playerCameraFirst.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void SwitchToFirstPerson() //tryck v för att byta
    {
        playerCamera.transform.SetParent(playerCameraFirst);
        playerCamera.transform.localPosition = Vector3.zero;
        playerCamera.transform.localRotation = Quaternion.identity;
    }

    void SwitchToThirdPerson() //tryck v för att byta
    {
        playerCamera.transform.SetParent(playerCameraThird);
        playerCamera.transform.localPosition = Vector3.zero;
        playerCamera.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        ApplyRotation();
        if(Keyboard.current.vKey.wasReleasedThisFrame)
        {
            isFirstPerson = !isFirstPerson;

            if(isFirstPerson)
            {
                SwitchToFirstPerson();
            }
            else
            {
                SwitchToThirdPerson();
            }
        }
    }
}
