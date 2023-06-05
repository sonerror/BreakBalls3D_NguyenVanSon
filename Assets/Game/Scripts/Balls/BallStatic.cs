using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStatic : BallController
{
    public void Start()
    {
        ChangeColor(MaterialType.red);
    }
    public override void OnCollisionEnter(Collision collisionInfo)
    {
        base.OnCollisionEnter(collisionInfo);

        if (collisionInfo.gameObject.CompareTag(Constant.TAG_BALL))
        {
            Debug.Log("Hit");
            rb.isKinematic = false;
            rb.useGravity = true;

        }
        if(collisionInfo.gameObject.CompareTag(Constant.TAG_WALL))
        {
            Invoke(nameof(OnDespawn), destroyDelay);
        }
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

}
