using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController Instance;

    public int maxSaglik, gecerliSaglik;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;

        if(UIManager.Instance != null)
        {
            UIManager.Instance.slideriGuncelle(gecerliSaglik, maxSaglik);
        }

        
    }

    public void canAzalma()
    {
        gecerliSaglik--;
        sesManager.instance.sesEfektCikar(3);

        UIManager.Instance.slideriGuncelle(gecerliSaglik, maxSaglik);
        

        if (gecerliSaglik<=0)
        {
            gecerliSaglik = 0;
          // gameObject.SetActive(false);

          PlayerHareketController.instance.playerCanVerdiFonk();

        }
    }
    public void caniArtirFNC()
    {
        gecerliSaglik += Random.Range(1,3);

        if (gecerliSaglik >= maxSaglik)
        {
            gecerliSaglik = maxSaglik;
            UIManager.Instance.slideriGuncelle(gecerliSaglik, maxSaglik);
        }
    }

}
