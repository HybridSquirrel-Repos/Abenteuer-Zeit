using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTask : MonoBehaviour
{
    private TaskComplete _taskComplete;

    private void Awake()
    {
        _taskComplete = GetComponent<TaskComplete>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _taskComplete.taskComplete = true;
        }
    }
}
