using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {

            PlayerHealthController.Instance.canAzalma();
            PlayerHareketController.instance.geriTepkiFonk();

        }
    }
}
