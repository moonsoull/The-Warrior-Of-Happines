using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletHareketController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    int kacinciPozisyon;

    [SerializeField]
    float iskeletHizi = 4f;

    [SerializeField]
    float beklemeSuresi = 1f;
    float beklemeSayaci;

    Transform playerHedef;

    Rigidbody2D rb;

    Animator anim;

    bool sinirIcindemi;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sinirIcindemi = false;

    }

    private void Start()
    {
        beklemeSayaci = beklemeSuresi;
        playerHedef = GameObject.Find("Player").transform;

        foreach (Transform poz in pozisyonlar)
        {

            poz.parent = null;

        }




    }

    private void Update()
    {

        if (playerHedef.GetComponent<PlayerHareketController>().playerCanVerdiMi || GetComponent<iskeletHealthController>().iskeletOldumu)
        {
            rb.velocity = new  Vector2(0,rb.velocity.y);
            anim.SetBool("atakYapti",false);
            return;

        }  
        
        float mesafe = Vector2.Distance(playerHedef.position, transform.position);

        if (mesafe > 4)
        {
            sinirIcindemi = false;
        }

        else
        {
            sinirIcindemi = true;
        }

        if (!sinirIcindemi)
        {
            if (Mathf.Abs(transform.position.x - pozisyonlar[kacinciPozisyon].position.x) > 0.2)
            {
                if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-iskeletHizi, -rb.velocity.y);
                }


                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                beklemeSayaci -= Time.deltaTime;

                if (beklemeSayaci <= 0)
                {
                    beklemeSayaci = beklemeSuresi;
                    kacinciPozisyon++;

                    if (kacinciPozisyon >= pozisyonlar.Length)
                    {
                        kacinciPozisyon = 0;
                    }
                }
            }
        }
        else
        {
            Vector2 yonVectoru = transform.position - playerHedef.position;
            if (yonVectoru.magnitude > 1.5f && playerHedef != null)
            {
                if (yonVectoru.x > 0)
                {
                    rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y);
                }

                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("atakYapti", true);
                
            }
        }

        anim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
    }
}
