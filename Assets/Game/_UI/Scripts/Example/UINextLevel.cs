using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINextLevel : UICanvas
{
    private void Start()
    {
        Pref.Score += 100;
    }
    public void BtnNextLevel()
    {
        CloseDirectly();
        Debug.Log("next");
        LevelManager.Ins.LoadNextLevel();
    }
}
