using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaldiracx : MonoBehaviour
{
    [SerializeField]
    GameObject acilacakEngel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ok"))
        {

            GetComponent<Animator>().SetTrigger("kaldiracAcilsin");
            GetComponent<BoxCollider2D>().enabled = false;
            acilacakEngel.transform.DOLocalMoveY(acilacakEngel.transform.localPosition.y +2.5f, 1f);
        }
    }
}