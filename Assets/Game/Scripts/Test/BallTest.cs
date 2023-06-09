using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public Vector3 LastFrameVelocity;
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
            ContactPoint[] contacts = new ContactPoint[1];
            // Calculate the reflection direction
            Vector3 incomingDirection = other.transform.position - transform.position;
            Debug.Log(incomingDirection.normalized);
            Vector3 normal = other.ClosestPointOnBounds(transform.position);
            Debug.Log(normal);
            Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);

            // Apply the reflection to the entered object
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = reflectedDirection.normalized * Speed;
            }
        }
        
    }
}
