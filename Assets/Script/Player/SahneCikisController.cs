using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneCikisController : MonoBehaviour
{
    public string digerSahne;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerHareketController>().playerHareketKes();

            other.GetComponent<PlayerHareketController>().enabled = false;

            FadeController.instance.seffafdanMata();

            StartCoroutine(digerSahneyeGec());
        }

    } 

    IEnumerator digerSahneyeGec()
    {

     yield return new WaitForSeconds(1f);

     SceneManager.LoadScene(digerSahne);

    }

}
