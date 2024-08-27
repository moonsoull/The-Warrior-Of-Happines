using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField]
    Slider PlayerSlider;

    [SerializeField]
    TMP_Text CoinText;

    [SerializeField]
    Transform ButonlarPanel;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject bitisPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        bitisPanel.SetActive(false);
        tumBtnAlphasiniDus();
        ButonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.herseyiKapatnormaliAc();
        

    }

    public void slideriGuncelle(int gecerliDeger, int maxDeger)
    {

        PlayerSlider.maxValue = maxDeger;

        PlayerSlider.value = gecerliDeger;

    }

    public void coinAdetGuncelle()
    {
        CoinText.text = GameManager.Instance.toplananCoinAdet.ToString();
    }

    void tumBtnAlphasiniDus()
    {
        foreach (Transform btn in ButonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha=0.25f;
            btn.GetComponent<Button>().interactable = true;
        }
    }

    public void normalBtnBasildi()
    {
        tumBtnAlphasiniDus();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.herseyiKapatnormaliAc();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false; 

    }
    public void kilicBtnBasildi()
    {
        tumBtnAlphasiniDus();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.normaldenKilicaFonk();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

    }
    public void okBtnBasildi()
    {
        tumBtnAlphasiniDus();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.herseyiKapatokAc();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

    }
    public void mizrakBtnBasildi()
    {
        tumBtnAlphasiniDus();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.HerSeyiKapatMizrakAcFNC();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

    }

    public void pausePanelAcKapa()
    {

        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

        }else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void anaMenuyeDon()
    {
        SceneManager.LoadScene("anamenu");

    }

    public void bitisPaneliAc()
    {
      bitisPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void tekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void oyundanCik()
    {
        Application.Quit();
    }

}
