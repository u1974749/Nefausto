using System.Collections;
using TESTING;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICapture : MonoBehaviour
{
    [SerializeField] GameObject options;
    public GameObject fadeOut;
    private bool activeFadeIn = false;

    private void Update()
    {
        if (activeFadeIn)
        {
            Color fade = fadeOut.GetComponent<RawImage>().color;
            if(fade.a < 1)
            {
                fade.a += 0.03f;
                fadeOut.GetComponent<RawImage>().color = fade;
            }
            else activeFadeIn = false;
        }
    }

    public void HelpOption()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().savePlayerPosition();
        fadeOut.SetActive(true);
        activeFadeIn = true;
        StartCoroutine("FadeOut");
    }
    public void IgnoreOption()
    {
        options.SetActive(false);
        GameObject dialogue = GameObject.Find("Vacio");
        if(dialogue != null)
            dialogue.GetComponent<Test_Architect>().offOptions();
        dialogue = GameObject.Find("Bocazas");
        if(dialogue != null)
            dialogue.GetComponent<Test_Architect>().offOptions();
        dialogue = GameObject.Find("Hook");
        if (dialogue != null)
            dialogue.GetComponent<Test_Architect>().offOptions();
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.3f);
        PlayerMove.playerMove = true;
        SceneManager.LoadScene("Charge");
    }
}