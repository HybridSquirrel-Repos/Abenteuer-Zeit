using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class DecayingPlatform : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            meshRenderer.enabled = false;
            boxCollider2D.enabled = false;
        }
    }

    void Wait()
    {
        WaitForSeconds 
    }
}
