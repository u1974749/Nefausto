using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Charge : MonoBehaviour
{
    private static bool change = false; //MODIFY FALSE
    public void Awake()
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("stepsSand");
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2);
        if (!change)
        {
            FindObjectOfType<AudioManager>().Play("principalTheme");
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("battleTheme");
            SceneManager.LoadScene("Capture");
        }
        change = !change;
    }
}
