using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] InputController inputController;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float lerpSpeed = 0.1f;
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float tiltSpeed = 10f;
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private AnimationCurve speedMultiplierCurve;

    [SerializeField] private Vector3 movement = Vector3.zero;
    [SerializeField] private Vector3 rotation = Vector3.zero;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        CalculateMovement();
        CalculateRotation();
    }

    private void CalculateMovement()
    {
        float forward = inputController.InputData.forward;
        float speedMultiplier = speedMultiplierCurve.Evaluate(rb.velocity.magnitude);
        movement = transform.forward * forward * speed * speedMultiplier;
        movement = movement.magnitude < minSpeed ? transform.forward * minSpeed : movement;
        rb.velocity = Vector3.Lerp(rb.velocity, movement, lerpSpeed * Time.deltaTime);
        // rb.velocity = movement;
        // rb.AddForce(movement);
    }

    private void CalculateRotation()
    {
        float pan = inputController.InputData.pan;
        float tilt = inputController.InputData.tilt;
        float roll = inputController.InputData.roll;

        rotation = new Vector3(tilt * tiltSpeed, pan * panSpeed, -roll * rollSpeed);
        // rb.angularVelocity = rotation;

        // rotation = new Vector3(0, 0, roll * rollSpeed);
        // rb.AddTorque(rotation);
        // rb.AddRelativeTorque(rotation);

        // float tilt = inputController.InputData.tilt;
        // float pan = inputController.InputData.pan;

        // rotation = new Vector3(tilt * tiltSpeed,0 , 0);

        transform.Rotate(rotation);

    }

    // private void Fo 

    // private void Pan() {
    //     float horizontal = inputController.InputData.horizontal;
    //     float vertical = inputController.InputData.vertical;
    //     float roll = inputController.InputData.roll;

    //     Vector3 pan = new Vector3(horizontal, 0, vertical);
    //     Vector3 tilt = new Vector3(0, 0, roll);

    //     rb.AddForce(pan * panSpeed);
    //     rb.AddTorque(tilt * tiltSpeed);
    // }

    // private void Tilt() {
    //     float vertical = inputController.InputData.vertical * tiltSpeed * Time.deltaTime;
    //     movement = new Vector3(0f, 0f, vertical);
    // }

    // private void Roll() {
    //     float roll = inputController.InputData.roll * rollSpeed * Time.deltaTime;
    //     movement = new Vector3(0f, 0f, 0f);
    // }

    // private void Move() {
    //     rb.MovePosition(rb.position + movement);
    // }

}
