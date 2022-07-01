using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody rb;

    [Space(20), Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 10f;

    [Space(20), Header("Debug")]
    [SerializeField] private float lifetimeLeft = 0f;

    private void Awake()
    {
        lifetimeLeft = lifetime;
    }

    private void Update()
    {
        lifetimeLeft -= Time.deltaTime;
        if (lifetimeLeft <= 0f)
            Die();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Die();
    }
}
