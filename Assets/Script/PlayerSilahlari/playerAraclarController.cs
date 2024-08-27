using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAraclarController : MonoBehaviour

{
    [SerializeField]
    bool kilicMi, mizrakMi,okMu;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (other != null && kilicMi)
            {
                other.GetComponent<PlayerHareketController>().normaldenKilicaFonk();

                

            }
            if(other != null && mizrakMi)
            {
                other.GetComponent<PlayerHareketController>().HerSeyiKapatMizrakAcFNC();

                

            }

            if (other != null && okMu)
            {
                other.GetComponent<PlayerHareketController>().herseyiKapatokAc();



            }
            Destroy(gameObject);

        }
    }

}
