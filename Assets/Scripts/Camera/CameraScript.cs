using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        
        gameObject.transform.position = new Vector3(player.transform.position.x, 0 , -1);
    }
}
