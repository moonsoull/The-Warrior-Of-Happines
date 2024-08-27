using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    [SerializeField] 
    BoxCollider2D kýlýcVurusBox;

    [SerializeField]
    GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kýlýcVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
  
                StartCoroutine(other.GetComponent<orumcekController>().GeriTepkiFNC());

            }
        }

        if (kýlýcVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Bat"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                
                other.GetComponent<BatController>().caniAzalatFNC();

            }
        }

        if (kýlýcVurusBox.IsTouchingLayers(LayerMask.GetMask("iskeletLayer")))
        {
            if (other.CompareTag("iskelet"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }

                other.GetComponent<iskeletHealthController>().caniAzalatFNC();

            }
        }


    }

}
