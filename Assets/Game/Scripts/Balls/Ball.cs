using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallController
{

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        GameObject collidedObject = collision.gameObject;  //UNDONE: đoạn này chưa xong
        if (collision.gameObject.CompareTag(Constant.TAG_BALL_IMG) )
        {
            //UNDONE: đoạn này chưa xong
            MeshRenderer objectBRenderer = collidedObject.GetComponent<MeshRenderer>();

            Material objectBColor = objectBRenderer.material;

            MeshRenderer objectARenderer = GetComponent<MeshRenderer>();

            objectARenderer.material = objectBColor;
            //
            Invoke(nameof(OnDespawn), destroyDelay);
        }
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
