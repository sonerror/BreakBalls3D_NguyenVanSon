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
        numberLevel.text = BallStaticManager.Ins.currentLevel.ToString();
        numberBall.text = BallManager.Ins.totalBalls.ToString();
    }

}
