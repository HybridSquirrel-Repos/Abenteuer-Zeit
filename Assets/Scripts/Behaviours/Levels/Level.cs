using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
  
    public bool levelCompleted;
    public GameObject nextLevel;
    public List<TaskComplete> objectives = new List<TaskComplete>();

}
