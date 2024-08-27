using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatController : MonoBehaviour
{

    [SerializeField]
    float takipMesafesi=8f;

    [SerializeField]
    float batHiz;

    [SerializeField]
    Transform hedefPlayer;

    Animator anim;

    Rigidbody2D rigidbody2;

    BoxCollider2D batCollider;

    public float atakYapmaSuresi;
    float atakYapmaSayac;

    float mesafe;

    Vector2 hareketYonu;

    public int maxSaglik;
    int gecerliSaglik;

    [SerializeField]
    GameObject iksirPrefab;


    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;

        anim = GetComponent<Animator>();

        rigidbody2 = GetComponent<Rigidbody2D>();

        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;

    }

    private void Update()
    {
        if (atakYapmaSayac<=0)
        {
            if (hedefPlayer && gecerliSaglik > 0 && !PlayerHareketController.instance.playerCanVerdiMi)
            {


                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);
                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("ucusaGitti");

                    hareketYonu = hedefPlayer.position - transform.position;


                    if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    rigidbody2.velocity = hareketYonu * batHiz;
                }
            }   


        }else
        {

            atakYapmaSayac -= Time.deltaTime;


        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                rigidbody2.velocity = Vector2.zero;
                atakYapmaSayac = atakYapmaSuresi;
                anim.SetTrigger("atakYapti");

                other.GetComponent<PlayerHareketController>().geriTepkiFonk();

                other.GetComponent<PlayerHealthController>().canAzalma();
            }
        }
    }

    public void caniAzalatFNC()
    {

        gecerliSaglik--;
        sesManager.instance.sesEfektCikar(1);
        atakYapmaSayac = atakYapmaSuresi;
        rigidbody2.velocity=Vector2.zero;

        if(gecerliSaglik<=0)
        {
            gecerliSaglik=0;
            batCollider.enabled = false;
            if (PlayerHealthController.Instance.gecerliSaglik<5)
            {

                Instantiate(iksirPrefab, transform.position, Quaternion.identity);

            }

            anim.SetTrigger("canVerdi");
            sesManager.instance.sesEfektCikar(0);
            Destroy(gameObject,3f);
            
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,takipMesafesi);
    }



}
