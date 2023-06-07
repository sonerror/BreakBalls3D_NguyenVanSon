using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class BallManager : Singleton<BallManager>
{
    public Ball ballPf;
    public Ball ballZonePf;
    public Transform ballTF;
    public List<Ball> balls = new List<Ball>();
    public int totalBalls;
    public float spawnInterval;
    [SerializeField] private SpawnZone[] zones;
    public Transform zoneTF;
    private Ball initialBall;

    void OnInit()
    {
        SpawnTotalBalls(totalBalls);
        ZoneSpawn();
    }
    private void Update()
    {
        if(PaddleController.Ins.isInsBall == true)
        {
            SpawnBalls();
        }
    }
    private void Start()
    {
        OnInit();
    }
    private void ZoneSpawn()
    {
        for (int i = 0; i < zones.Length; i++)
        {
            SpawnZone zone = SimplePool.Spawn<SpawnZone>(zones[i]);
            zone.gameObject.transform.SetParent(zoneTF);
            zone.gameObject.SetActive(true);
        }
    }
    public void SpawnTotalBalls(int totalBalls)
    {
        for (int i = 0; i < totalBalls; i++)
        {
            Ball ball = SimplePool.Spawn<Ball>(ballPf);
            ball.gameObject.SetActive(false);
            balls.Add(ball);
        }
    }

    private void SpawnBalls()
    {
        Vector3 paddlePosition = PaddleController.Ins.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y, 0);
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.position = startingPosition;
            balls[i].gameObject.SetActive(true);
        }
    }
    public void ZoneSpawnBalls(int totalBalls, Vector3 spawnPosition)
    {
        for (int i = 0; i < totalBalls; i++)
        {
            Ball ball = Instantiate(ballZonePf, spawnPosition,Quaternion.identity);
        }
    }

}
