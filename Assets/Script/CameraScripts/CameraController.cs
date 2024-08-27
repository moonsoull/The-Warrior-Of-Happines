using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    PlayerHareketController player;
    [SerializeField]
    Collider2D boundsBox;

    float halfYukseklik, halfGenislik;

    Vector2 sonPosion;

    [SerializeField]
    Transform background;

    private void Awake()
    {
        
     player=Object.FindObjectOfType<PlayerHareketController>(); 

    }
  
    private void Start()
    {
        halfYukseklik = Camera.main.orthographicSize;

        halfGenislik = halfYukseklik * Camera.main.aspect;

        sonPosion = transform.position;
         
    }


    private void Update()
    {
        if (player != null)
        {

            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfGenislik, boundsBox.bounds.max.x - halfGenislik),
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfYukseklik, boundsBox.bounds.max.y - halfYukseklik), 
                transform.position.z);
        }
      

        if (background != null)
        {
            BackgrodunHareket();
        }
        
    }

    void BackgrodunHareket()
    {
        Vector2 aradakifark = new Vector2(transform.position.x-sonPosion.x, transform.position.y-sonPosion.y);

        background.position += new Vector3(aradakifark.x,aradakifark.y,0f);

        sonPosion= transform.position;
        
    }

}
