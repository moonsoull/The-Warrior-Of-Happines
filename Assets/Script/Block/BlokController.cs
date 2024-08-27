using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokController : MonoBehaviour
{
    public Transform altPoint;

    Animator anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orjinalPos;
    Vector3 animPos;

    public LayerMask playerLayer;

    bool animasyonbaslasinMi;
    bool hareketEtsinmi = true;


    public GameObject coinPrefab;
    Vector3 coinPos;




    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

        orjinalPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;

        coinPos = transform.position;
        coinPos.y += 1f;

    }

    private void Update()
    {
        carpismayiKontorlEtFNC();
        animasyonuBaslat();
    }

    void carpismayiKontorlEtFNC()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, .1f, playerLayer);

            // Debug.DrawRay(altPoint.position, Vector2.down, Color.green);

            if (hit && hit.collider.gameObject.tag == "Player")
            {


                anim.Play("mat");
                animasyonbaslasinMi = true;
                hareketEtsinmi = false;

                Instantiate(coinPrefab,coinPos, Quaternion.identity);

            }
        }


    }

    void animasyonuBaslat()
    {

        if (animasyonbaslasinMi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);

            if (transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down;
            }
            else if (transform.position.y <= orjinalPos.y)
            {

                animasyonbaslasinMi = false;
            }


        }

    }
}
