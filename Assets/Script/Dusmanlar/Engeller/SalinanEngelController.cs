using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalinanEngelController : MonoBehaviour
{
    [SerializeField]
    float donmeHizi = 100f;

    float zAngle;

    [SerializeField]
    float minzAngle = -75f;

    [SerializeField]
    float maxzAngle = 75f;

    private void Start()
    {
       // if (Random.Range(0,2) > 0)
        //{
            donmeHizi *= -1;
        //}
    }

    private void Update()
    {
        zAngle += Time.deltaTime*donmeHizi;
         
        transform.rotation=Quaternion.AngleAxis(zAngle,Vector3.forward);

        if(zAngle < minzAngle)
        {
            donmeHizi=Mathf.Abs(donmeHizi);
        }

        if(zAngle > maxzAngle)
        {
            donmeHizi=-Mathf.Abs(donmeHizi);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                
                other.GetComponent<PlayerHareketController>().geriTepkiFonk();
                other.GetComponent<PlayerHealthController>().canAzalma();
            }
        }
    }

}
