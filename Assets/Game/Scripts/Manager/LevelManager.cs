using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int totalBalls;
    public int maxRows = 50;
    public int maxCols = 60;

    public int currentLevel;


    public void LoadNextLevel()
    {
        this.currentLevel++;
        if (this.currentLevel >= BallStaticManager.Ins.LevelsData.Count)
        {
            
            GameManager.Ins.UIVictory();
        }
        else
        {
            this.LoadLevel(this.currentLevel);
        }
    }
    public void LoadLevel(int level)
    {
        ResetLevel();
        this.currentLevel = level;
        BallStaticManager.Ins.GenerateBalls();
        BallManager.Ins.countBall = 40;
    }
    public void ResetLevel()
    {
        BallManager.Ins.ClearBalls();
        BallStaticManager.Ins.ClearRemainingBalls();
    }
    public List<int[,]> LoadLevelsData()
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
