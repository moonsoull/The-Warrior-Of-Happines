using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField]
    bool coinmi, iksirmi;

    [SerializeField]
    GameObject nesneEfect;

    bool toplandimi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !toplandimi)
        {
            if (coinmi)
            {
                toplandimi = true;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.toplananCoinAdet++;
                    UIManager.Instance.coinAdetGuncelle();
                    sesManager.instance.karisikSesEfekt(6);

                }

                Destroy(gameObject);
                Instantiate(nesneEfect, transform.position, Quaternion.identity);
            }
            if (iksirmi)
            {

                toplandimi = true;

                PlayerHealthController.Instance.caniArtirFNC();
                sesManager.instance.sesEfektCikar(8);

                Destroy(gameObject);
                Instantiate(nesneEfect, transform.position, Quaternion.identity);

            }

        }
    }
}
