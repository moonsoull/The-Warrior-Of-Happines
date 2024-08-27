using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletHealthController : MonoBehaviour
{
    public int maxSaglik;

    int gecerliSaglik;

  public  bool iskeletOldumu;

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        iskeletOldumu = false;

    }

    public void caniAzalatFNC()
    {
        gecerliSaglik-- ;
        sesManager.instance.sesEfektCikar(1);

        if (gecerliSaglik<=0) 
        {
            iskeletOldumu=true;
            GetComponent<Animator>().SetTrigger("caniniVerdi");
            sesManager.instance.sesEfektCikar(2);
            GetComponent<BoxCollider2D>().enabled=false;
            Destroy(gameObject,3f);
        }
    }
}
