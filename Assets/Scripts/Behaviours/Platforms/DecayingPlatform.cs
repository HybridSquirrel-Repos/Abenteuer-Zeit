
using UnityEngine;



public class DecayingPlatform : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _meshRenderer.enabled = false;
            _boxCollider2D.enabled = false;
        }
    }

    /*void Wait()
    {
        
    }*/
}
