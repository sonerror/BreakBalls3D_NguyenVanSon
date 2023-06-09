using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class BallController : GameUnit
{
    [SerializeField]
    private Vector3 initialVelocity;
    private Vector3 direction;
    private float speed;

    [SerializeField]
    private float minVelocity;

    private Vector3 lastFrameVelocity;
    [SerializeField] public Rigidbody rb;

    public float destroyDelay;

    public bool hasCollided;

    public MaterialType materialType;
    public int Hitpoints = 1;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected MeshRenderer rendererBall;
    [SerializeField] protected SkinnedMeshRenderer skinnedRenderer;

    public bool hasStarted = false;

    private void Start()
    {
        OnInit();
        hasStarted = false;
    }
    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }
    void OnInit()
    {
        rb.velocity = initialVelocity;
    }

 /*   public virtual void OnCollisionEnter(Collision collision)
    {
        if (!hasStarted)
        {
            hasStarted = true;
        }
        else
        {
            speed += 30f;
            Bounce(collision.contacts[0].normal);
        }
    }
    public void Bounce(Vector3 collisionNormal)
    {
        speed = lastFrameVelocity.magnitude;

        direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed, minVelocity);

    }*/
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constant.TAG_ZONE_SPAWN))
        {
            Vector3 moveDirection = rb.velocity.normalized;

            Ray ray = new Ray(other.ClosestPoint(transform.position), moveDirection);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 collisionNormal = -hit.normal;

                Vector3 reflectedDirection = Vector3.Reflect(moveDirection, collisionNormal);

                rb.velocity = reflectedDirection.normalized * rb.velocity.magnitude;
            }
        }
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
    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

}
