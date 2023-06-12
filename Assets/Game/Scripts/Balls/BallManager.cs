using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallManager : Singleton<BallManager>
{
    public Ball ballPf;
    public Transform ballTF;
    public List<Ball> balls = new List<Ball>();
    public float spawnInterval;
    [SerializeField] private SpawnZone[] zones;
    public Transform zoneTF;
    public int countBall;
    public float spawnDelay = 1f;

    private float spawnBallTimer = 0f;
    private float spawnBallTime = 0.5f;

    public void OnInit()
    {
        SpawnTotalBalls(LevelManager.Ins.totalBalls);
    }
    private void Start()
    {
        ZoneSpawn();
        countBall = 40;
    }
    private void Update()
    {
        if (PaddleController.Ins.isInsBall)
        {
            spawnBallTimer += Time.deltaTime;
            if (spawnBallTimer >= spawnBallTime)
            {
               if(countBall > 0)
                {
                    spawnBallTimer = 0f;
                    SpawnBall();
                }
            }
        }
        else
        {
            spawnBallTimer = spawnBallTime;
        }
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
    public void ZoneSpawnBalls(int totalBalls, Vector3 spawnPosition)
    {
        for (int i = 0; i < totalBalls; i++)
        {
            Ball ball = Instantiate(ballPf, spawnPosition, Quaternion.identity);
            ball.transform.SetParent(ballTF);
            balls.Add(ball);
        }
    }
    public void SpawnBall()
    {
        Vector3 paddlePosition = PaddleController.Ins.gameObject.transform.position;

        Vector3 startingPosition = new Vector3(paddlePosition.x + 1.6f, paddlePosition.y, 0);

        Ball ball = Instantiate(ballPf);

        ball.gameObject.SetActive(true);

        ball.transform.position = startingPosition;

        ball.transform.SetParent(ballTF);

        balls.Add(ball);

        countBall -= 1;
    }
    public void ClearBalls()
    {

        for(int i = 0; i < balls.Count;i++)
        {
            Destroy(balls[i].gameObject);
        }
        balls.Clear();
    }
}
