using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Get pos in world
    private List<Vector3> positions = new List<Vector3>();

    private Vector3 _currentPos;
    private Vector3 _moveTo;
    private float speed = 1;
    
    //Get children to move to
    
    //Move platform to destination and change destination

    private void Awake()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            positions.Add(child.position);
        }
    }

    private void Update()
    {
        _currentPos = transform.position;
        
        if (_currentPos == positions[0])
        {
            _moveTo = positions[1];
        }
        else if (_currentPos == positions[1])
        {
            _moveTo = positions[0];
        }
        
        transform.position = Vector3.MoveTowards(_currentPos, _moveTo, speed * Time.deltaTime);
    }
}


