using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public Vector3 LastFrameVelocity;
    public Vector3 ContactPoint;
    public Vector3 ReflectedDirection;
    public Vector3 Normal;
    public RaycastHit RaycastHit;
    public Ray Ray;
    public LayerMask LayerMaskBall;
    public float Speed;


    private void Update()
    {
        LastFrameVelocity = GetComponent<Rigidbody>().velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball");
            Speed = LastFrameVelocity.magnitude;
            Vector3 incomingDirection = other.transform.position - transform.position;
            Debug.DrawRay(transform.position, incomingDirection, Color.red);
            Ray = new Ray(transform.position, incomingDirection);
            if (Physics.Raycast(Ray, out RaycastHit, 0.5f, LayerMaskBall))
            {
                Vector3 Normal = RaycastHit.normal;
                ReflectedDirection = Vector3.Reflect(incomingDirection.normalized, Normal);
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Debug.Log(1);
                    rb.velocity = ReflectedDirection.normalized * Speed;
                }
            }
        }
    }
}
