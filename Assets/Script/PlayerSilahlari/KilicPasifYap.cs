using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilicPasifYap : MonoBehaviour
{
    public GameObject kilicVurusBox;

    public void KilicKapat() 
    {
        kilicVurusBox.SetActive(false); 
    }
}
