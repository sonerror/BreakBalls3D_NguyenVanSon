using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallManager : Singleton<BallManager>
{
    public Ball ballPf;
    public Transform ballTF;
    public List<Ball> balls = new List<Ball>();
    public int totalBalls;
    public float spawnInterval;
    [SerializeField] private SpawnZone[] zones;
    public Transform zoneTF;

    void OnInit()
    {
        SpawnBalls(totalBalls);
        ZoneSpawnBalls();
    }

    private void Start()
    {
        OnInit();
    }
    public void Update()
    {
        //TEST
        if (PaddleController.Ins.isInsBall)
        {
           SpawnObjects();
        }
    }

    private void ZoneSpawnBalls()
    {
        for (int i = 0; i < zones.Length; i++)
        {
            Debug.Log("SpawnZone " + i);
            SpawnZone zone = SimplePool.Spawn<SpawnZone>(zones[i]);
            zone.gameObject.transform.SetParent(zoneTF);
            zone.gameObject.SetActive(true);
        }
    }
    public void SpawnBalls(int totalBalls)
    {
        for (int i = 0; i < totalBalls; i++)
        {
            Ball ball = SimplePool.Spawn<Ball>(ballPf, ballTF.transform.position, Quaternion.identity);
            ball.transform.SetParent(ballTF);
            ball.gameObject.SetActive(false);
            balls.Add(ball);
        }
    }

    private void SpawnObjects()
    {
        foreach(Ball ball in balls)
        {
            ball.gameObject.SetActive(true);
        }
    }
    public void ZoneSpawnBalls(int totalBalls, Vector3 spawnPosition)
    {
        for (int i = 0; i < totalBalls; i++)
        {
            Ball ball = SimplePool.Spawn<Ball>(ballPf);
            ball.gameObject.transform.position = spawnPosition;
            ball.transform.SetParent(ballTF);
            ball.gameObject.SetActive(true);
           
            balls.Add(ball);
        }
    }
}
