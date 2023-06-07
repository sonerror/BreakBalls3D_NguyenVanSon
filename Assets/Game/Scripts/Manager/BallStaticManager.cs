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
    private float initialBallSpawnPositionY = 50f;
    private float shiftAmount = 1f;

    public BallStatic ballPrefab;

    public List<BallStatic> RemainingBalls { get; set; }

    public List<int[,]> LevelsData { get; set; }

    public int InitialBallStaticCount { get; set; }

    public int currentLevel;

    private void Start()
    {
        Debug.Log(currentLevel);
        levelDatas = new List<int[,]>();
        LevelsData = new List<int[,]>();
        levelDatas = LoadLevelsData();
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBalls();
    }
    private void Update()
    {
        this.InitialBallStaticCount = this.RemainingBalls.Count;
    }
    public void LoadNextLevel()
    {
        this.currentLevel++;
        if (this.currentLevel >= this.LevelsData.Count)
        {
            GameManager.Ins.UIVictory();
        }
        this.LoadLevel(this.currentLevel);
    }
    public void LoadLevel(int level)
    {
        this.currentLevel = level;
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

        int[,] currentLevelData = this.LevelsData[this.currentLevel];

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
                    BallStatic newBall = Instantiate(ballPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as BallStatic;
                   
                    newBall.Init(bricksContainer.transform);

                    SetBallColor(newBall, BallType);

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