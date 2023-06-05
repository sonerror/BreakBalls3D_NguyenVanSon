using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallController
{

    public void Start()
    {
        ChangeColor(MaterialType.white);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        GameObject collidedObject = collision.gameObject;
        if (collision.gameObject.CompareTag(Constant.TAG_BALL_IMG) )
        {
            MeshRenderer objectBRenderer = collidedObject.GetComponent<MeshRenderer>();

            Material objectBColor = objectBRenderer.material;

            MeshRenderer objectARenderer = rendererBall.GetComponent<MeshRenderer>();

            objectARenderer.material = objectBColor;

            Invoke(nameof(OnDespawn), destroyDelay);
        }
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
