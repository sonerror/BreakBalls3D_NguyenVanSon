using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStatic : BallController
{

    public override void OnCollisionEnter(Collision collisionInfo)
    {
        base.OnCollisionEnter(collisionInfo);
        Debug.Log(isBallMoving + "?hit");
        if (collisionInfo.gameObject.CompareTag(Constant.TAG_BALL))
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            isBallMoving = true;
            Debug.Log(isBallMoving + "hit?");
            BallStaticManager.Ins.RemainingBalls.Remove(this);
        }
        if(collisionInfo.gameObject.CompareTag(Constant.TAG_WALL))
        {
            Invoke(nameof(OnDespawn), destroyDelay);
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
