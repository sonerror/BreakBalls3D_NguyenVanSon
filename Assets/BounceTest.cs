using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTest : MonoBehaviour
{

    [SerializeField]
    private Vector3 initialVelocity;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    [SerializeField] public Rigidbody rb;

    public float destroyDelay;

    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }
    void OnInit()
    {
        rb.velocity = initialVelocity;
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }
    public void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}
