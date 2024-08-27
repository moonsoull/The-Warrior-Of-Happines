using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KirilanManager : MonoBehaviour
{
    [SerializeField]
    bool sandikMi, korkulukMu;

    Animator anim;

    int kacinciVurus;

    [SerializeField]
    GameObject parlamaEfekti;

    [SerializeField]
    GameObject coinPrefab;

    Vector2 patlamaMiktari = new Vector2(4, 4);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        kacinciVurus = 0;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("kilicVurusBox"))
        {
            if (sandikMi)
            {
                if (kacinciVurus == 0)
                {
                    anim.SetTrigger("sallanma");

                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else if (kacinciVurus == 1)
                {

                    anim.SetTrigger("sallanma");

                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else
                {
                    GetComponent<BoxCollider2D>().enabled = false;

                    sesManager.instance.sesEfektCikar(9);

                    anim.SetTrigger("parcalanma");


                    for (int i = 0; i < Random.Range(1, 4); i++)
                    {

                        Vector3 rastgeleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);
                        GameObject coin = Instantiate(coinPrefab, rastgeleVector, transform.rotation);
                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));

                    }
                }

                kacinciVurus++;
            }


            if (korkulukMu)
            {
                if (kacinciVurus == 0)
                {

                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                    sesManager.instance.sesEfektCikar(3);
                }
                else if (kacinciVurus == 1)
                {

                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                    sesManager.instance.sesEfektCikar(3);
                }
                else
                {
                    GetComponent<BoxCollider2D>().enabled = false;

                    for (int i = 0; i < Random.Range(1, 4); i++)
                    {

                        Vector3 rastgeleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);
                        GameObject coin = Instantiate(coinPrefab, rastgeleVector, transform.rotation);
                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));

                    }
                    sesManager.instance.sesEfektCikar(9);
                    //  Destroy(gameObject);
                }

                kacinciVurus++;
            }
        }

    }
}
