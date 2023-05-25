using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCrtImg : BallController
{
    public void Awake()
    {
        Debug.Log("nononononono");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BALL))
        {
            /*rb.isKinematic = false;
            rb.useGravity = true;*/

            Debug.Log(other.gameObject);

        }
    }
}
