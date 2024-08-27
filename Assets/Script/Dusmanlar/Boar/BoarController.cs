using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : MonoBehaviour
{
    [SerializeField]
    float boarYurumehizi,boarKosmahizi;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float gorusMesafesi = 8f;

    [SerializeField]
    BoxCollider2D boarCollider;

    public bool olduMu;

    public LayerMask playerLayer;

    [SerializeField]
    GameObject kanamaEfekti;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        olduMu = false;
    }

    private void Update()
    {
        if(olduMu)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), gorusMesafesi,playerLayer);

        

        transform.localScale=new Vector3(-1,1,1);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boarKosmahizi, rb.velocity.y);
                anim.SetBool("kossunmu", true);
            }

        }
        else
        {
            rb.velocity = new Vector2(-boarYurumehizi, rb.velocity.y);
            anim.SetBool("kossunmu", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boarCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");
                other.GetComponent<PlayerHareketController>().geriTepkiFonk();
                other.GetComponent<PlayerHealthController>().canAzalma();
            }
        }
    }
    public void boarOldu()
    {
        olduMu = true;
        anim.SetTrigger("canVerdi");
        sesManager.instance.sesEfektCikar(0);

        rb.velocity = Vector2.zero;
        rb.isKinematic= true;

        Instantiate(kanamaEfekti,transform.position,transform.rotation);


        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>()) 
        {
            box.enabled = false;  


        }

        Destroy(gameObject, 3f);

    }
}
