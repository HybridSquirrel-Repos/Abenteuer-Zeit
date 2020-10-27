using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    [SerializeField] private bool isSwitched;
    private bool _corotineAllowed;
    private PlatformEffector2D _effector2D;
    private void Awake()
    {
        _effector2D = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            isSwitched = true;
        }
        else
        {
            isSwitched = false;
        }
        
        if (isSwitched)
        {
            _effector2D.rotationalOffset = 180;
            
        }
        else
        {
            _effector2D.rotationalOffset = 0;
            
        }
    }
}
