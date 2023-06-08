using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Text numberLevel;
    public Text numberBall;
    public void Update()
    {
        numberLevel.text = (LevelManager.Ins.currentLevel + 1).ToString();
        numberBall.text = LevelManager.Ins.totalBalls.ToString();
    }

}
