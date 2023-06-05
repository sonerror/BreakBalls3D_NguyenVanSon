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
    private int maxRows = 50;
    private int maxCols = 60;
    public Transform bricksContainer;
    private float initialBallSpawnPositionX = 0f;
    private float initialBallSpawnPositionY = 45f;
    private float shiftAmount = 1f;
    public MaterialType materialType; 

    public BallStatic brickPrefab;

    public List<BallStatic> RemainingBalls { get; set; }

    public List<int[,]> LevelsData { get; set; }

    public int InitialBricksCount { get; set; }

    public int CurrentLevel;

    private void Start()
    {
        levelDatas = new List<int[,]>();
        LevelsData = new List<int[,]>();    
        levelDatas = LoadLevelsData();
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBalls();
    }

    public void LoadNextLevel()
    {
        this.CurrentLevel++;

        if (this.CurrentLevel >= this.LevelsData.Count)
        {
            GameManager.Ins.ShowVictoryScreen();
        }
        else
        {
            this.LoadLevel(this.CurrentLevel);
        }
    }

    public void LoadLevel(int level)
    {
        this.CurrentLevel = level;
        this.ClearRemainingBalls();
        this.GenerateBalls();
    }

    private void ClearRemainingBalls()
    {
        foreach (BallStatic ballStatic in this.RemainingBalls.ToList())
        {
            Destroy(ballStatic.gameObject);
        }
    }

    private void GenerateBalls()
    {
        this.RemainingBalls = new List<BallStatic>();

        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];

        float currentSpawnX = initialBallSpawnPositionX;

        float currentSpawnY = initialBallSpawnPositionY;

        float zShift = 0;
        

        for (int row = 0; row < this.maxRows; row++)
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                int BallType = currentLevelData[row, col];

                if (BallType > 0)
                {
                    BallStatic newBall = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as BallStatic;

                    newBall.Init(bricksContainer.transform);

                    this.RemainingBalls.Add(newBall);

                    zShift += 0.0001f;
                }

                currentSpawnX += shiftAmount;

                if (col + 1 == this.maxCols)
                {
                    currentSpawnX = initialBallSpawnPositionX;
                }
            }
            currentSpawnY -= shiftAmount;
        }

        this.InitialBricksCount = this.RemainingBalls.Count;

        OnLevelLoaded?.Invoke();
    }

    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;

        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();

        int[,] currentLevel = new int[maxRows, maxCols];

        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] balls = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < balls.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(balls[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;

                levelsData.Add(currentLevel);

                currentLevel = new int[maxRows, maxCols];
            }
        }   
        return levelsData;
    }
}