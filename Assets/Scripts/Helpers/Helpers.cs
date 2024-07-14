using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Helpers : MonoBehaviour
{
    public Image llamitaImage;
    public Image sadImage;
    public Image curiousImage;

    public GameObject root;
    private GameObject rootInstance;
    public bool rootGrew;
    
    public static float llamitaTimer = 400;
    public static float llamitaTimerMax = 400;
    public static float sadTimer = 15; 
    public static float sadTimerMax = 15;
    public static float curiousTimer = 30;
    public static float curiousTimerMax = 30;

    public void flamaHelp()
    {
        captureMove.flamaDisolveFog = true;
        GameObject.FindWithTag("Canvas").GetComponent<HelpersUI>().DeactiveHelpers();
    }

    public void threeHelp()
    {
        if (sadTimer >= sadTimerMax)
        {
            GameObject.FindWithTag("Canvas").GetComponent<HelpersUI>().DeactiveHelpers();
            GameObject captureObject = GameObject.FindWithTag("captureObject");
            captureObject.GetComponent<EnemyMove>().stopMoveEnemy();
            rootInstance = Instantiate(root, captureObject.transform.position, Quaternion.identity);
            rootGrew = true;
            Debug.Log("Que pasa");
            sadTimer = 0;
            StartCoroutine(stopAttack(captureObject));
        }
    }
    IEnumerator stopAttack(GameObject captureObject)
    {
        yield return new WaitForSeconds(10);
        rootGrew = false;
        captureObject.GetComponent<EnemyMove>().startMoveEnemy();
        while (rootInstance.GetComponent<MeshRenderer>().material.GetFloat("_Grow") > 0)
            rootInstance.GetComponent<MeshRenderer>().material.SetFloat("_Grow", (rootInstance.GetComponent<MeshRenderer>().material.GetFloat("_Grow") - Time.deltaTime * 1.5f));
        Destroy(rootInstance);
    }

    public void curiousHelp()
    {
        DestroyCollider.attackInmumne = true;
        captureMove.changeMaterial = true;
        //captureMove styler = GameObject.FindWithTag("styler").GetComponent<captureMove>();
        //if (styler != null)
          //  styler.ChangeMaterialLine();
        GameObject.FindWithTag("Canvas").GetComponent<HelpersUI>().DeactiveHelpers();
        curiousTimer = 0;
        StartCoroutine(painAttackOn());
    }
    IEnumerator painAttackOn()
    {
        yield return new WaitForSeconds(10);
        captureMove.changeMaterial = false;
        captureMove styler = GameObject.FindWithTag("styler").GetComponent<captureMove>();
        if (styler != null)
            styler.ChangeMaterialLine();
        DestroyCollider.attackInmumne = false;
    }

    void Update()
    {
        circleTimer();
        if(rootGrew)
        {
            rootInstance.transform.position = GameObject.FindWithTag("captureObject").transform.position;
            rootInstance.GetComponent<MeshRenderer>().material.SetFloat("_Grow", (rootInstance.GetComponent<MeshRenderer>().material.GetFloat("_Grow") + Time.deltaTime * 1.5f));
        }
        //else if (!rootGrew && rootInstance.GetComponent<MeshRenderer>().material.GetFloat("_Grow") > 0)
        //{
          //  rootInstance.GetComponent<MeshRenderer>().material.SetFloat("_Grow", (rootInstance.GetComponent<MeshRenderer>().material.GetFloat("_Grow") - Time.deltaTime * 1.5f));
        //}
    }

    void circleTimer()
    {
        /*if (llamitaTimer < llamitaTimerMax)
        {
            llamitaTimer = llamitaTimer + Time.deltaTime;
            if(llamitaImage.IsActive())
                llamitaImage.fillAmount = llamitaTimer / llamitaTimerMax;
        }*/
        if (sadTimer < sadTimerMax)
        {
            sadTimer = sadTimer + Time.deltaTime;
            if(sadImage.IsActive())
                sadImage.fillAmount = sadTimer/ sadTimerMax;
        }
        if (curiousTimer < curiousTimerMax)
        {
            curiousTimer = curiousTimer + Time.deltaTime;
            if(curiousImage.IsActive())
                curiousImage.fillAmount = curiousTimer / curiousTimerMax;
        }
    }
}
