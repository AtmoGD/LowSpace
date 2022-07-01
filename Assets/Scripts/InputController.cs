using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputController : MonoBehaviour
{
    [SerializeField] private InputData inputData = new InputData();
    public InputData InputData { get { return inputData; } }

    public void OnFirstMovement(InputAction.CallbackContext context)
    {
        Vector3 movement = context.ReadValue<Vector2>();

        inputData.roll = movement.x;
        inputData.forward = Mathf.Clamp01(movement.y); 
    }

    public void OnSecondMovement(InputAction.CallbackContext context)
    {
        Vector3 movement = context.ReadValue<Vector2>();

        inputData.pan = movement.x;
        inputData.tilt = movement.y;
    }

    public void OnLaser(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                inputData.laser = true;
                break;
            case InputActionPhase.Canceled:
                inputData.laser = false;
                break;
        }
    }

    public void OnMissile(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                inputData.missile = true;
                break;
            case InputActionPhase.Canceled:
                inputData.missile = false;
                break;
        }
    }
}
