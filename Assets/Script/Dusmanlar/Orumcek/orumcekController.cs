using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class orumcekController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    [SerializeField]
    Slider orumcekSlider;

    public int maxSaglik;
    int gecerliSaglik;


    public float orumcekHýzý;

    public float beklemeSuresi;

    float beklemeSayaci;

    Animator animator;

    int kacinciPoz;

    public float takipMesafesi;

    Transform hedefPlayer;

    BoxCollider2D orumcekCollider;

    bool atakYapabilirmi;

    Rigidbody2D rigidbody2;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        orumcekCollider = GetComponent<BoxCollider2D>();

        rigidbody2 = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        orumcekSlider.maxValue= maxSaglik;
        SliderGüncelleFonk();

        atakYapabilirmi = true;

        hedefPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in pozisyonlar) 
        {
            pos.parent = null;
        }
    }


    private void Update()
    {

        if(!atakYapabilirmi)
        {

            return;
        }
        if (beklemeSayaci>0)
        {
            beklemeSayaci -= Time.deltaTime;
            animator.SetBool("hareketEtsinMi", false);

        }else
        {
            if (hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                Vector3 yeniPoz = hedefPlayer.position;
                yeniPoz.y=transform.position.y;

                

                    transform.position = Vector3.MoveTowards(transform.position,yeniPoz, orumcekHýzý * Time.deltaTime);

                    animator.SetBool("hareketEtsinMi", true);

                    if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(1,1,1);
                    }
                    else if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1,1, 1);
                    }


                }
                else
                {

                    animator.SetBool("hareketEtsinMi", true);


                    if (transform.position.x < pozisyonlar[kacinciPoz].position.x)
                    {
                        transform.localScale = new Vector3(1,1,1);
                    }
                    else if (transform.position.x > pozisyonlar[kacinciPoz].position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPoz].position, orumcekHýzý * Time.deltaTime);

                    if (Vector3.Distance(transform.position, pozisyonlar[kacinciPoz].position) < 0.1f)
                    {

                        beklemeSayaci = beklemeSuresi;
                        pozisyonuDegisFonk();
                    }
                }
            
        }

    }

    void pozisyonuDegisFonk()
    {

        kacinciPoz++;
        if(kacinciPoz >= pozisyonlar.Length)
        
            kacinciPoz= 0;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,takipMesafesi);

       
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirmi)
        {
            atakYapabilirmi= false;
            animator.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketController>().geriTepkiFonk();
            other.GetComponent<PlayerHealthController>().canAzalma();
            StartCoroutine(YenidenAtakYapsin());
        }
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);

        if(gecerliSaglik>0)
        atakYapabilirmi = true;
    }


    public IEnumerator GeriTepkiFNC()
    {
        atakYapabilirmi = false;
       
        rigidbody2.velocity = Vector2.zero;

       // yield return new WaitForSeconds(.1f);

        gecerliSaglik--;
        sesManager.instance.sesEfektCikar(1);

        SliderGüncelleFonk();

        if (gecerliSaglik<=0)
        {
            atakYapabilirmi = false;
            gecerliSaglik=0;
            animator.SetTrigger("canVerdi");

            sesManager.instance.sesEfektCikar(0);

            orumcekCollider.enabled = false;
            orumcekSlider.gameObject.SetActive(false); 
            Destroy(gameObject, 2f);

        }
        else
        {
            for(int i = 0; i<5; i++)
            {
                rigidbody2.velocity =new Vector2(-transform.localScale.x+i,rigidbody2.velocity.y);

                yield return new WaitForSeconds(0.05f);
            }


            animator.SetBool("hareketEtsinMi", false);
            yield return new WaitForSeconds(0.25f);

            rigidbody2.velocity= Vector2.zero;
            atakYapabilirmi = true;
        }

    }

    void SliderGüncelleFonk()
    {
        orumcekSlider.value = gecerliSaglik;
    }
}
