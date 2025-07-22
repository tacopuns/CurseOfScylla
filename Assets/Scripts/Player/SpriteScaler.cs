using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;
    public float minScale = 0.6f;  // Minimum scale factor
    public float maxScale = 1.0f;  // Maximum scale factor
    public float scaleThreshold = 5.0f;  // Where should sprite be at max scale i made this up btw


    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AdjustPerspective();

    }
    
    private void AdjustPerspective()
    {
        float currentYPosition = transform.position.y;
        float t = Mathf.InverseLerp(scaleThreshold, -10, currentYPosition); 
        float targetScale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = new Vector3(targetScale, targetScale, 1);
    }
}
