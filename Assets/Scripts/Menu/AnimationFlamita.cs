using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFlamita : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject flamitaAnimation;
    public GameObject finalPanel;

    private void Awake()
    {
        flamitaAnimation.SetActive(true);
        StartCoroutine("playAnimationFlamita");
    }

    IEnumerator playAnimationFlamita()
    {
        yield return new WaitForSeconds(3);
        flamitaAnimation.SetActive(false);
        fadeOut.SetActive(false);
        finalPanel.SetActive(true);
    }
}
