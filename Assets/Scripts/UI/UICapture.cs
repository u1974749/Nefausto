using System.Collections;
using System.Collections.Generic;
using TESTING;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICapture : MonoBehaviour
{
    [SerializeField] GameObject options;

    public void HelpOption()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().savePlayerPosition();
        SceneManager.LoadScene("Capture");
    }
    public void IgnoreOption()
    {
        options.SetActive(false);
        GameObject.Find("DialogueVacioIntroduction").GetComponent<Test_Architect>().offOptions();
    }
}
