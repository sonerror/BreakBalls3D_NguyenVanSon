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

    void OnInit()
    {
        SpawnBalls(totalBalls);
    }
    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {
        
    }
    public void Update()
    {
        if(PaddleController.Ins.isInsBall)
        {
            StartCoroutine(SpawnObjects());
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
    private IEnumerator SpawnObjects()
    {
        foreach(Ball ball in balls)
        {
            ball.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
