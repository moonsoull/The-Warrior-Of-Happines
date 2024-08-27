using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuController : MonoBehaviour
{

    public string digerSahne;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void oyunaBasla()
    {
        FadeController.instance.seffafdanMata();
        StartCoroutine(sahneyeGec());
        

    }

    IEnumerator sahneyeGec()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(digerSahne);
    }

    public void oyundanCik()
    {
        Application.Quit();
    }
}
