using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> _levels = new List<Level>();

    public void AddLevel()
    {
        _levels.Add(new Level());
    }

    public void RemoveLevel()
    {
        _levels.RemoveAt(_levels.Count - 1);
    }

    public void CheckLevelProgression()
    {
        int taskCompleted = 0;
        bool cancel = false;
        foreach (var level in _levels)
        {
            if (level.levelCompleted == false)
            {
                foreach (var objective in level.objectives)
                {
                    if (objective.taskComplete == false)
                    {
                        break;
                    }
                    else
                    {
                        taskCompleted += 1;
                        if (taskCompleted == level.objectives.Count)
                        {
                            level.levelCompleted = true;
                        }
                    }
                }
            }
            
        }
    }

    public void NextLevel()
    {
        int levelCompete = 0;

   
        foreach (var level in _levels)
        {
            Debug.Log(levelCompete);
            if (level.levelCompleted)
            {
                levelCompete += 1;
            }
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.transform.position = _levels[levelCompete - 1].nextLevel.transform.position;
        }
       
    }

    private void Update()
    {
        CheckLevelProgression();
        
    }
}