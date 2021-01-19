using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    public bool buttonPressed;
    public List<PlatformEffector2D> _effector2D = new List<PlatformEffector2D>();

    void Update()
    {
        if (buttonPressed)
        {
            for (int i = 0; i < _effector2D.Count; i++)
            {
                _effector2D[i].rotationalOffset = 180;
            }
            
            StartCoroutine(Delay());
        }
        else
        {
            for (int i = 0; i < _effector2D.Count; i++)
            {
                _effector2D[i].rotationalOffset = 0;
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.5f);
            buttonPressed = false;
        }

       
    }
}
