using System.Collections;
using UnityEngine;

public class PlayerHareketController : MonoBehaviour
{

    public static PlayerHareketController instance;
    
    Rigidbody2D rb;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer, mizrakPlayer,okPlayer;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    Animator animator , kilicAnimator,mizrakAnimator,okAnimator;

    [SerializeField]
    SpriteRenderer sr, kilicSprite,mizrakSprite,okSprite;

    [SerializeField]
    GameObject kilicVurusBoxObj;
   

    public LayerMask zeminMaske;


    public float hareketHizi;
    public float ziplamaGucu;

    bool zemindemi;
    bool ciftziplamami;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;

    float geriTepkiSayaci;

    bool yonSagdaMi;

    public bool playerCanVerdiMi;

    bool atakYaptiMi;

    [SerializeField]
    GameObject atilacakMizrak;

    [SerializeField]
    Transform mizrakCikisNoktasi;

    [SerializeField]
    GameObject atilacakOk;

    [SerializeField]
    Transform okCikisNoktasi;

    bool okAtabilirmi;

    [SerializeField]
    float tirmanisHizi = 3f;

    [SerializeField]
    GameObject normalKamera, kilicKamera, okKamera, mizrakKamera;


    private void Awake()
    {
        instance = this;
        atakYaptiMi= false;

        rb = GetComponent<Rigidbody2D>();

        playerCanVerdiMi = false;

        kilicVurusBoxObj.SetActive(false);

        okAtabilirmi = true;

        

    }

    private void Update()
    {

        if (playerCanVerdiMi)
        {
            return;
        }


        if (geriTepkiSayaci <= 0)
        {

            HareketEt();
            Zipla();
            yonDegistir();


            if (normalPlayer.activeSelf)
            {

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
            if (kilicPlayer.activeSelf)
            {

                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }

            if (mizrakPlayer.activeSelf)
            {

                mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, 1f);
            }

            if (okPlayer.activeSelf)
            {

                okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, 1f);
            }

            if (UnityEngine.Input.GetMouseButtonDown(0) && kilicPlayer.activeSelf)
            {
                atakYaptiMi = true;
                kilicVurusBoxObj.SetActive(true);
                sesManager.instance.sesEfektCikar(4);

            }
            else
            {
                atakYaptiMi= false;
               
            }

            if (UnityEngine.Input.GetMouseButtonDown(1) && mizrakPlayer.activeSelf)
            {
                mizrakAnimator.SetTrigger("mizrakAtti");
                Invoke("mizragiFirlat",.5f);
                sesManager.instance.sesEfektCikar(5);

            }

            if (UnityEngine.Input.GetMouseButtonDown(1) && okPlayer.activeSelf && okAtabilirmi)
            {

                okAnimator.SetTrigger("okAtti");
              StartCoroutine(okuAzsonraAtRoutine());
                sesManager.instance.sesEfektCikar(7);


            }

            if (okPlayer.activeSelf)
            {

                if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("TirmanmaLayer")))
                {

                    float h = UnityEngine.Input.GetAxis("Vertical");

                    Vector2 tirmanisVector = new Vector2(rb.velocity.x, h * tirmanisHizi);
                    rb.velocity = tirmanisVector;
                    rb.gravityScale = 0f;
                    okAnimator.SetBool("tirmansinMi", true);
                    okAnimator.SetFloat("yukariHareketHizi", Mathf.Abs(rb.velocity.y));


                }

                else
                {
                    okAnimator.SetBool("tirmansinMi", false);
                    rb.gravityScale=1f;


                }


            }



            }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;

            if (yonSagdaMi)
            {
                rb.velocity = new Vector2(-geriTepkiGucu,rb.velocity.y);
            }else
            {
                rb.velocity = new Vector2(+geriTepkiGucu, rb.velocity.y);
            }
        }
       

        if (normalPlayer.activeSelf)
        {
            animator.SetBool("zemindemi", zemindemi);

            animator.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        }

        if (kilicPlayer.activeSelf)
        {
            kilicAnimator.SetBool("zemindemi", zemindemi);

            kilicAnimator.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));

        }
        if (mizrakPlayer.activeSelf)
        {
            mizrakAnimator.SetBool("zemindeMi", zemindemi);

            mizrakAnimator.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));

        }

        if (okPlayer.activeSelf)
        {
            okAnimator.SetBool("zemindemi", zemindemi);

            okAnimator.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));

        }

        if (atakYaptiMi && kilicPlayer.activeSelf)
        {
            kilicAnimator.SetTrigger("atakYapti");
        }
    }


    void mizragiFirlat()
    {
        
        GameObject mizrak = Instantiate(atilacakMizrak,mizrakCikisNoktasi.position,mizrakCikisNoktasi.rotation);
        mizrak.transform.localScale = transform.localScale;
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right*transform.localScale.x * 7f;
        Invoke("herseyiKapatnormaliAc", .1f);
    }

    IEnumerator okuAzsonraAtRoutine()
    {
        okAtabilirmi = false;
        yield return new WaitForSeconds(.9f);
        OkPoolManager.instance.OkuFirlatFNC(okCikisNoktasi, this.transform);
        okAtabilirmi=true;
    }
   

    void HareketEt()
    {
        float h = UnityEngine.Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (h*hareketHizi,rb.velocity.y);

        
    }

    void yonDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            yonSagdaMi = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            yonSagdaMi = true;
        }


    }


    void Zipla()
    {
        zemindemi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske);

        if (UnityEngine.Input.GetButtonDown("Jump") && ( zemindemi || ciftziplamami))
        {

            if (zemindemi)
            {
                ciftziplamami = true;
            }
            else
            {
               ciftziplamami = false; 
            }

            rb.velocity = new Vector2(rb.velocity.x,ziplamaGucu);


        }
    }

    public void geriTepkiFonk()
    {

        geriTepkiSayaci = geriTepkiSuresi;


        if (normalPlayer.activeSelf)
        {

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
        }
        if (kilicPlayer.activeSelf)
        {

            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }
        if (mizrakPlayer.activeSelf)
        {

            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, .5f);
        }

        if (okPlayer.activeSelf)
        {

            okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, .5f);
        }



        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    public void playerCanVerdiFonk()
    {
        rb.velocity = Vector2.zero;
        playerCanVerdiMi = true;


        if (normalPlayer.activeSelf)
        {
            animator.SetTrigger("canverdi");
          
        }

        if (kilicPlayer.activeSelf)
        {
            kilicAnimator.SetTrigger("canverdi");
            Destroy(kilicVurusBoxObj);
            
        }
        if (mizrakPlayer.activeSelf)
        {
           mizrakAnimator.SetTrigger("canverdi");

        }

        if (okPlayer.activeSelf)
        {
            okAnimator.SetTrigger("canverdi");

        }

        StartCoroutine(oyundanCikis());
        //StartCoroutine(PlayerYokEtSahneYenile());

    }

    IEnumerator oyundanCikis()
    {
        yield return new WaitForSeconds(2f);

        GameManager.Instance.oyunCikisEkraniAc();
    }


    /*   IEnumerator PlayerYokEtSahneYenile()
       {
           yield return new WaitForSeconds (2f);

           GetComponentInChildren<SpriteRenderer>().enabled = false;

           yield return new WaitForSeconds(1f);

           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }*/


    public void normaldenKilicaFonk()
    {
        tumKameralariKapat();
        kilicKamera.SetActive(true);

        normalPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
            okPlayer.SetActive(false);

    }

    public void HerSeyiKapatMizrakAcFNC() {

        tumKameralariKapat();
        mizrakKamera.SetActive(true);

        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true) ;
        okPlayer.SetActive(false);
    }

    public void herseyiKapatnormaliAc()
    {

        tumKameralariKapat();
        normalKamera.SetActive(true);

        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(false);
    }

    public void herseyiKapatokAc()

    {
        tumKameralariKapat();
        okKamera.SetActive(true);

        okPlayer.SetActive(true);
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
    }

    void tumKameralariKapat()
    {
        normalKamera.SetActive(false);
        kilicKamera.SetActive(false);
        mizrakKamera.SetActive(false);
        okKamera.SetActive(false);
    }
    public void playerHareketKes() { 
        if(normalPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("hareketHizi",0f);
        }
        if (kilicPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            kilicAnimator.SetFloat("hareketHizi", 0f);
        }
        if (mizrakPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            mizrakAnimator.SetFloat("hareketHizi", 0f);
        }

        if (okPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            okAnimator.SetFloat("hareketHizi", 0f);
        }

    }


}
