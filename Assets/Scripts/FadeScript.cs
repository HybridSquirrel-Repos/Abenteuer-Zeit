using System.Collections;
using UnityEngine;


public class FadeScript : MonoBehaviour
{
    public Material material;


    void Start () {
        
        material = GetComponent<Renderer> ().material;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(nameof(FadeOut));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(nameof(FadeIn));
    }


    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f) 
        {
                SetAlpha(ft);
                yield return null;
        }
    }
    
    IEnumerator FadeIn()
    {
        for (float ft = 0f; ft <= 1; ft += 0.1f) 
        {
            SetAlpha(ft);
            yield return null;
        }
    }
    
    
    
    void SetAlpha(float alpha)
    {

        Color color = material.color;
        color.a = Mathf.Clamp( alpha, 0, 1 );
        material.color = color;
    }
}
