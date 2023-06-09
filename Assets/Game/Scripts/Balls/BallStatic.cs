using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStatic : BallController
{
    /*   public override void OnCollisionEnter(Collision collisionInfo)
       {
           base.OnCollisionEnter(collisionInfo);

           if (collisionInfo.gameObject.CompareTag(Constant.TAG_BALL))
           {
               rb.isKinematic = false;

               rb.useGravity = true;
           }
           if (collisionInfo.gameObject.CompareTag(Constant.TAG_WALL))
           {
               Invoke(nameof(OnDespawn), destroyDelay);
               BallStaticManager.Ins.RemainingBalls.Remove(this);
           }
       }*/
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Constant.TAG_BALL))
        {
            rb.isKinematic = false;

            rb.useGravity = true;
        }
        if (other.CompareTag(Constant.TAG_WALL))
        {
            Invoke(nameof(OnDespawn), destroyDelay);
            BallStaticManager.Ins.RemainingBalls.Remove(this);
        }
    }


    public void Init(Transform containerTransform)
    {
        this.transform.SetParent(containerTransform);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
}
