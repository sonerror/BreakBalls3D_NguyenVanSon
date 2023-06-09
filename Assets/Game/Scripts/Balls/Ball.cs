using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallController
{
    private int numberBallSpawn = 2;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Constant.TAG_BALL_IMG))
        {
            Invoke(nameof(OnDespawn), destroyDelay);
        }
        if (other.CompareTag(Constant.TAG_ZONE_SPAWN))
        {
            Vector3 spawnPosition = gameObject.transform.position - new Vector3(0f, 2f, 0f);

            BallManager.Ins.ZoneSpawnBalls(numberBallSpawn, spawnPosition);

            OnDespawn();

        }
        if (other.CompareTag(Constant.TAG_ZONE_1))
        {
            OnDespawn();
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ZONE_SPAWN))
        {
            Vector3 spawnPosition = gameObject.transform.position - new Vector3(0f, 2f, 0f);

            BallManager.Ins.ZoneSpawnBalls(numberBallSpawn, spawnPosition);

            OnDespawn();
        }
        if (other.CompareTag(Constant.TAG_ZONE_1))
        {
            OnDespawn();
        }
    }*/
  /*  public override void OnCollisionEnter(Collision collision)
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
    }*/
    public override void OnDespawn()
    {
        base.OnDespawn();

        BallManager.Ins.balls.Remove(this);

    }

}
