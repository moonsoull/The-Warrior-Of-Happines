using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sesManager : MonoBehaviour
{

    public static sesManager instance;

    [SerializeField]
    AudioSource[] sesEfektleri;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

        }else if (this != instance)
        {
            Destroy(gameObject);
        }
    }

    public void sesEfektCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].Play();

    }

    public void karisikSesEfekt(int hangiSes)
    {

        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].pitch= Random.Range(0.8f, 1.3f);
        sesEfektleri[hangiSes].Play();


    }


}
