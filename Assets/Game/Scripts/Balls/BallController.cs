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

    public float destroyDelay;

    public MaterialType materialType;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected MeshRenderer rendererBall;
    [SerializeField] protected SkinnedMeshRenderer skinnedRenderer;
    private bool isBounce = false;

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

    public void ChangeColor(MaterialType type)
    {
        materialType = type;

        if (rendererBall != null)
        {
            rendererBall.material = colorData.GetMat(type);
        }

        if (skinnedRenderer != null)
        {
            skinnedRenderer.material = colorData.GetMat(type);
        }
        
    }

}
