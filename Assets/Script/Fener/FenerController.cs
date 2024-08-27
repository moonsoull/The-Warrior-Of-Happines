using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenerController : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer fenerSprRenderer;

    [SerializeField]
    Sprite fenerOnSprite, fenerOffSprite;

    private void Awake()
    {
        fenerSprRenderer.sprite = fenerOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        
            fenerSprRenderer.sprite = fenerOnSprite;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
            Invoke("feneriKapat", .5f );
           

    }

    void feneriKapat()
    {
        fenerSprRenderer.sprite = fenerOffSprite;
    }
}
