using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectInatanceEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sad;
    public GameObject curiosity;
    public GameObject hook;
    public GameObject fog;

    public GameObject UICapture;
    public GameObject UICaptureSad;
    public GameObject UICaptureCurious;
    public GameObject UICaptureHook;

    void Start()
    {
        if(InteractNPC.enemyInstance == 0)
            sad.SetActive(true);
        else if (InteractNPC.enemyInstance == 1)
            curiosity.SetActive(true);
        else if (InteractNPC.enemyInstance == 2)
        {
            hook.SetActive(true);
            fog.SetActive(true);
        }
    }

    public void CaptureFinishUI()
    {
        UICapture.SetActive(true);
        if (InteractNPC.enemyInstance == 0)
            UICaptureSad.SetActive(true);
        else if (InteractNPC.enemyInstance == 1)
            UICaptureCurious.SetActive(true);
        else if (InteractNPC.enemyInstance == 2)
            UICaptureHook.SetActive(true);
        StartCoroutine(ScreenCharge());
    }

    IEnumerator ScreenCharge()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("SampleScene");
    }
}
