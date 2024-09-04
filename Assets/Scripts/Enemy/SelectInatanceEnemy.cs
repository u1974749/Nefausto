using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectInatanceEnemy : MonoBehaviour
{
    public GameObject sad;
    public GameObject curiosity;
    public GameObject hook;
    public GameObject parasityOne;
    public GameObject parasityTwo;
    public GameObject fog;

    public GameObject UICapture;
    public GameObject UICaptureSad;
    public GameObject UICaptureCurious;
    public GameObject UICaptureHook;
    public GameObject UICaptureParasity;
    public GameObject UIFlamitaInactive;
    public GameObject UIHand;
    public bool captureFail = false;

    public bool sceneReset = false;

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
            if(!captureMove.captureSad || !captureMove.captureCuriosity)
            {
                UIFlamitaInactive.SetActive(true);
                sceneReset = true;
                captureFail = true;
            }
            else
            {
                UIHand.SetActive(true);
                StartCoroutine("handInactive");
            }
        }
        else if (InteractNPC.enemyInstance == 3)
            parasityOne.SetActive(true);
        else if (InteractNPC.enemyInstance == 4)
            parasityTwo.SetActive(true);
    }

    IEnumerator handInactive()
    {
        yield return new WaitForSeconds(2);
        UIHand.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && sceneReset)
        {
            CaptureInstance.instanceFlama = true;
            if(captureMove.captureHook)
                SceneManager.LoadScene("EndMenu");
            else if(captureFail) SceneManager.LoadScene("ExplainAnimation");
            else SceneManager.LoadScene("Charge");

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
        else if (InteractNPC.enemyInstance == 3 || InteractNPC.enemyInstance == 4)
            UICaptureParasity.SetActive(true);
        GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().stopMoveEnemy();
        CaptureInstance.instanceFlama = false;
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("winTheme");
        StartCoroutine(ScreenCharge());
    }

    IEnumerator ScreenCharge()
    {
        yield return new WaitForSeconds(2);
        sceneReset = true;
    }
}
