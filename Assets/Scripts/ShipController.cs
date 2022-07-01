using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ParticleData
{
    public List<ParticleSystem> particles;
    public AnimationCurve particleCount;
    public AnimationCurve particleLifetimeMin;
    public AnimationCurve particleLifetimeMax;
}

[Serializable]
public class ShipController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] InputController inputController;
    [SerializeField] List<ParticleData> fireParticles;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawn;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileSpawn;


    [Space(20), Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float lerpSpeed = 0.1f;
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float tiltSpeed = 10f;
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private AnimationCurve speedMultiplierCurve;
    [SerializeField] private float laserCooldown = 0.5f;
    [SerializeField] private float missileCooldown = 1f;


    [Space(20), Header("Debug")]
    [SerializeField] private Vector3 movement = Vector3.zero;
    [SerializeField] private Vector3 rotation = Vector3.zero;
    [SerializeField] private float roll = 0f;
    [SerializeField] private float tilt = 0f;
    [SerializeField] private float pan = 0f;
    [SerializeField] private float forward = 0f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float actualVelocity = 0f;
    [SerializeField] private float actualLaserCooldown = 0f;
    [SerializeField] private float actualMissileCooldown = 0f;

    private void Update()
    {
        UpdateCooldowns();
        UpdateFireParticles();
    }

    private void FixedUpdate()
    {
        Fire();
        Move();
    }

    private void Fire()
    {
        if (inputController.InputData.laser && actualLaserCooldown <= 0f && laserPrefab)
        {
            actualLaserCooldown = laserCooldown;
            Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation);
        }
        if (inputController.InputData.missile && actualMissileCooldown <= 0f && missilePrefab)
        {
            actualMissileCooldown = missileCooldown;
            Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        }
    }

    private void Move()
    {
        CalculateMovement();
        CalculateRotation();
    }

    private void CalculateMovement()
    {
        forward = inputController.InputData.forward;
        speedMultiplier = speedMultiplierCurve.Evaluate(rb.velocity.magnitude);
        movement = transform.forward * forward * speed * speedMultiplier;
        movement = movement.magnitude < minSpeed ? transform.forward * minSpeed : movement;
        rb.velocity = Vector3.Lerp(rb.velocity, movement, lerpSpeed * Time.deltaTime);
        actualVelocity = rb.velocity.magnitude;
    }

    private void CalculateRotation()
    {
        pan = inputController.InputData.pan;
        tilt = inputController.InputData.tilt;
        roll = inputController.InputData.roll;

        rotation = new Vector3(tilt * tiltSpeed, pan * panSpeed, -roll * rollSpeed);

        transform.Rotate(rotation);
    }

    private void UpdateFireParticles()
    {
        foreach (ParticleData particleData in fireParticles)
        {
            foreach (ParticleSystem particleSystem in particleData.particles)
            {
                ParticleSystem.EmissionModule emission = particleSystem.emission;
                emission.rateOverTime = particleData.particleCount.Evaluate(inputController.InputData.forward);
                ParticleSystem.MainModule main = particleSystem.main;
                ParticleSystem.MinMaxCurve lifetime = main.startLifetime;
                lifetime.curveMin = particleData.particleLifetimeMin;
                lifetime.curveMax = particleData.particleLifetimeMax;
            }
        }
    }

    private void UpdateCooldowns()
    {
        actualLaserCooldown = Mathf.Max(0, actualLaserCooldown - Time.deltaTime);
        actualMissileCooldown = Mathf.Max(0, actualMissileCooldown - Time.deltaTime);
    }
}
