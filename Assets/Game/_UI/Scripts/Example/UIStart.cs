using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : UICanvas
{
    public void Update()
    {
        if(PaddleController.Ins.isMoving == true)
        {
            UIManager.Ins.OpenUI<UIGamePlay>();
            CloseDirectly();
        }
    }
}
