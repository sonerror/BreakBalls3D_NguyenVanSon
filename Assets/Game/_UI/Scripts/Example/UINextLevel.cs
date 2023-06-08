using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINextLevel : UICanvas
{
    public void BtnNextLevel()
    {
        CloseDirectly();
        LevelManager.Ins.LoadNextLevel();
    }
}
