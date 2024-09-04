using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationIntroduction : MonoBehaviour
{
    public void Awake()
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("menuTheme");
        StartCoroutine(reproduceAnimation());
    }

    IEnumerator reproduceAnimation()
    {
        yield return new WaitForSeconds(19);
        SceneManager.LoadScene("Charge");
    }
}
