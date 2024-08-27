using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public int toplananCoinAdet;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        toplananCoinAdet = 0;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.pausePanelAcKapa(); 
        }
    }

    public void oyunCikisEkraniAc()
    {
        UIManager.Instance.bitisPaneliAc();
    }

}
