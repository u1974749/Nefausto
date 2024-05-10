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
    
    public static float llamitaTimer = 400;
    public static float llamitaTimerMax = 400;
    public static float sadTimer = 15; 
    public static float sadTimerMax = 15;
    public static float curiousTimer = 30;
    public static float curiousTimerMax = 30;

    public void threeHelp()
    {
        if(sadTimer >= sadTimerMax)
        {
            GameObject.FindWithTag("Canvas").GetComponent<HelpersUI>().DeactiveHelpers();
            GameObject captureObject = GameObject.FindWithTag("captureObject");
            captureObject.GetComponent<EnemyMove>().stopMoveEnemy();
            StartCoroutine(stopAttack(captureObject));
            sadTimer = 0;
        }
    }
    IEnumerator stopAttack(GameObject captureObject)
    {
        yield return new WaitForSeconds(10);
        captureObject.GetComponent<EnemyMove>().startMoveEnemy();
    }
    void Update()
    {
        circleTimer();
    }

    void circleTimer()
    {
        if (llamitaTimer < llamitaTimerMax)
        {
            llamitaTimer = llamitaTimer + Time.deltaTime;
            if(llamitaImage.IsActive())
                llamitaImage.fillAmount = llamitaTimer / llamitaTimerMax;
        }
        if (sadTimer < sadTimerMax)
        {
            Debug.Log( "TIEMPO "+sadTimer);
            sadTimer = sadTimer + Time.deltaTime;
            if(sadImage.IsActive())
                sadImage.fillAmount = sadTimer/ sadTimerMax;
        }
        if (curiousTimer < curiousTimerMax && curiousImage.IsActive())
        {
            curiousTimer = curiousTimer + Time.deltaTime;
            if(curiousImage.IsActive())
                curiousImage.fillAmount = curiousTimer / curiousTimerMax;
        }
    }
}
