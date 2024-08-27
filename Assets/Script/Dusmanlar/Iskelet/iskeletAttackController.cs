using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletAttackController : MonoBehaviour
{

    [SerializeField]
    Transform attackPoz;

    [SerializeField]
    float attackYaricap;

    [SerializeField]
    LayerMask PlayerLayer;

    public void attackYapFNC()
    {
        Collider2D playerCollider=Physics2D.OverlapCircle(attackPoz.position, attackYaricap,PlayerLayer);


        if(playerCollider != null && !playerCollider.GetComponent<PlayerHareketController>().playerCanVerdiMi)
        {

            playerCollider.GetComponent<PlayerHareketController>().geriTepkiFonk();
            playerCollider.GetComponent<PlayerHealthController>().canAzalma();

        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoz.position, attackYaricap);

        
    }
}
