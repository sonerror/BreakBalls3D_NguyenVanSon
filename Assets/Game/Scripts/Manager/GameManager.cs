using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected void Awake()
    {
        Input.multiTouchEnabled = false;

        Application.targetFrameRate = 60;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;

        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;

        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }
    private void Start()
    {
        UIStart();

    }
    public void Update()
    {
        UINextLevel();
        Debug.Log(BallStaticManager.Ins.InitialBallStaticCount);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UINextLevel()
    {
        if(BallStaticManager.Ins.InitialBallStaticCount <= 0)
        {
            UIManager.Ins.OpenUI<UINextLevel>();
        }
    }
    public void UIVictory()
    {
        UIManager.Ins.OpenUI<UIVictory>();
    }
    void UIStart()
    {
        UIManager.Ins.OpenUI<UIStart>();
    }
}
