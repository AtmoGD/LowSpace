using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputData inputData = new InputData();
    public InputData InputData { get { return inputData; } }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        // inputData.horizontal = context.ReadValue<float>();
    }

    public void OnFirstMovement(InputAction.CallbackContext context)
    {
        Vector3 movement = context.ReadValue<Vector2>();

        // inputData.tilt = movement.y;
        inputData.roll = movement.x;
        // inputData.tilt = movement.y;

        inputData.forward = movement.y; 
        // inputData.vertical = movement.y;
    }

    public void OnSecondMovement(InputAction.CallbackContext context)
    {
        Vector3 movement = context.ReadValue<Vector2>();
        // inputData.forward = movement.y;
        inputData.pan = movement.x;
        // inputData.horizontal = movement.x;

        // inputData.pan = movement.x;
        inputData.tilt = movement.y;

        
        // inputData.roll = movement.x;
        // inputData.horizontal = movement.y;
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        // inputData.vertical = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        // inputData.roll = context.ReadValue<float>();
    }

    public void OnLaser(InputAction.CallbackContext context)
    {
        inputData.laser = context.ReadValue<bool>();
    }

    public void OnMissile(InputAction.CallbackContext context)
    {
        inputData.missile = context.ReadValue<bool>();
    }
}
