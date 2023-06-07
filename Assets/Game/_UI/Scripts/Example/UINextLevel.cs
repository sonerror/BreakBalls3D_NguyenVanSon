using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINextLevel : UICanvas
{
    public void BtnNextLevel()
    {
        BallStaticManager.Ins.LoadNextLevel();
    }
}
