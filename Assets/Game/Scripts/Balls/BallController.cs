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


 /*   public Vector3 LastFrameVelocity;
    public Vector3 ContactPoint;
    public Vector3 ReflectedDirection;
    public Vector3 Normal;
    public RaycastHit RaycastHit;
    public Ray Ray;
    public LayerMask LayerMaskBall;
    public float Speed;*/
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

    /*    public virtual void OnTriggerEnter(Collider other)
        {
           if (other.CompareTag(Constant.TAG_BALL))
            {
                Speed = LastFrameVelocity.magnitude;
                Vector3 incomingDirection = other.transform.position - transform.position;
                Debug.DrawRay(transform.position, incomingDirection, Color.red);
                Ray = new Ray(transform.position, incomingDirection);
                if (Physics.Raycast(Ray, out RaycastHit, 0.5f, LayerMaskBall))
                {
                    Vector3 Normal = RaycastHit.normal;
                    ReflectedDirection = Vector3.Reflect(incomingDirection.normalized, Normal);
                    if (rb != null)
                    {
                        Debug.Log(1);
                        rb.velocity = ReflectedDirection.normalized * Speed;
                    }
                }
            }
        }*/
    public virtual void OnCollisionEnter(Collision collision)
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
