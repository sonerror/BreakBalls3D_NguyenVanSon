using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : GameUnit
{
    private int totalBalls = 2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_BALL))
        {
            Vector3 spawnPosition = other.transform.position - new Vector3(0f, 2f, 0f);
            Destroy(other.gameObject);
            BallManager.Ins.ZoneSpawnBalls(totalBalls, spawnPosition);
            
        }
    }
}
