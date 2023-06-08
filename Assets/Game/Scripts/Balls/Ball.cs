using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallController
{
    private int numberBallSpawn = 2;

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        GameObject collidedObject = collision.gameObject;

        if (collision.gameObject.CompareTag(Constant.TAG_BALL_IMG))
        {

            MeshRenderer objectBRenderer = collidedObject.GetComponent<MeshRenderer>();

            Material objectBColor = objectBRenderer.material;

            MeshRenderer objectARenderer = rendererBall.GetComponent<MeshRenderer>();

            objectARenderer.material = objectBColor;

            Invoke(nameof(OnDespawn), destroyDelay);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_ZONE))
        {
            Vector3 spawnPosition = gameObject.transform.position - new Vector3(0f, 2f, 0f);

            BallManager.Ins.ZoneSpawnBalls(numberBallSpawn, spawnPosition);

            OnDespawn();

        }
    }
    public override void OnDespawn()
    {
        base.OnDespawn();

        BallManager.Ins.balls.Remove(this);

    }
}
