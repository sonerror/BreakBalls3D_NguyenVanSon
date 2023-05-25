using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : GameUnit
{
    [SerializeField]
    private Vector3 initialVelocity;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    [SerializeField] public Rigidbody rb;

    private void OnEnable()
    {
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);//collision.contacts[0].normal Vector pháp tuyến của lần va chạm đầu tiên
    }

    public virtual void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);// Reflect tính toán vector phản xạ của một vector đầu vào dựa trên một vector pháp tuyến cho trước.
        //Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

}
