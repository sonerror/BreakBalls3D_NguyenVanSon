using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class BallStaticManager : Singleton<BallStaticManager>
{
    public static event Action OnLevelLoaded;
    [SerializeField] private List<int[,]> levelDatas;
    public Transform bricksContainer;
    private float initialBallSpawnPositionX = 0f;
    private float initialBallSpawnPositionY = 50f;
    private float shiftAmount = 1f;

    public BallStatic ballPrefab;

    public List<BallStatic> RemainingBalls { get; set; }

    public List<int[,]> LevelsData { get; set; }

    public int InitialBallStaticCount { get; set; }


    private void Start()
    {
        levelDatas = new List<int[,]>();
        LevelsData = new List<int[,]>();
        levelDatas = LevelManager.Ins.LoadLevelsData();
        this.LevelsData = LevelManager.Ins.LoadLevelsData();
        this.GenerateBalls();
    }
    private void Update()
    {
        this.InitialBallStaticCount = this.RemainingBalls.Count;
    }

    public void ClearRemainingBalls()
    {
        foreach (BallStatic ballStatic in this.RemainingBalls.ToList())
        {
            Destroy(ballStatic.gameObject);
        }
    }
    public void GenerateBalls()
    {
        this.RemainingBalls = new List<BallStatic>();

        int[,] currentLevelData = this.LevelsData[LevelManager.Ins.currentLevel];

        float currentSpawnX = initialBallSpawnPositionX;

        float currentSpawnY = initialBallSpawnPositionY;

        float zShift = 0;

        for (int row = 0; row < LevelManager.Ins.maxRows; row++)
        {
            for (int col = 0; col < LevelManager.Ins.maxCols; col++)
            {
                int BallType = currentLevelData[row, col];

                if (BallType > 0)
                {
                    BallStatic newBall = Instantiate(ballPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as BallStatic;
                   
                    newBall.Init(bricksContainer.transform);

                    SetBallColor(newBall, BallType);

                    this.RemainingBalls.Add(newBall);

                    zShift += 0.0001f;
                }

                currentSpawnX += shiftAmount;

                if (col + 1 == LevelManager.Ins.maxCols)
                {
                    currentSpawnX = initialBallSpawnPositionX;
                }
            }
            currentSpawnY -= shiftAmount;
        }

        this.InitialBallStaticCount = this.RemainingBalls.Count;

        OnLevelLoaded?.Invoke();

    }
    private void SetBallColor(BallStatic ball, int ballType)
    {
        Dictionary<int, MaterialType> ballTypeToMaterialType = new Dictionary<int, MaterialType>()
            {
                { 1, MaterialType.blue },
                { 2, MaterialType.red },
                { 3, MaterialType.yellow },
                { 4, MaterialType.white },
                { 5, MaterialType.Black }
            };

        if (ballTypeToMaterialType.TryGetValue(ballType, out MaterialType materialType))
        {
            ball.ChangeColor(materialType);
        }
    }
}