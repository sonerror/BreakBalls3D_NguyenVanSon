using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVictory : UICanvas
{
    private void Start()
    {
        CloseDirectly();
    }
    public void ResetGame()
    {
        GameManager.Ins.RestartGame();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
