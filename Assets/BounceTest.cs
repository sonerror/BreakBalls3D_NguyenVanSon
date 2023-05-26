using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTest : MonoBehaviour
{

   // public ColorData colorToChange;
    private void OnCollisionEnter(Collision collision)
    {

    }

    public Color GetRandomColor()
    {
        return new Color(
            r: UnityEngine.Random.Range(0f, 1f),
            g: UnityEngine.Random.Range(0f, 1f),
            b: UnityEngine.Random.Range(0f, 1f));
    }
}
