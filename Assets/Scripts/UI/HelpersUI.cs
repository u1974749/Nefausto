using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpersUI : MonoBehaviour
{
    public GameObject sadCircle;
    public GameObject sadObject;
    public GameObject curiosityCircle;
    public GameObject curiosityObject;
    public GameObject flamaCircle;
    public GameObject flamaObject;
    public GameObject canvasHelpers;
    public GameObject buttonOn;
    public GameObject buttonOff;

    void Awake()
    {
        ActiveHelpers();
    }

    public void ActiveHelpers()
    {
        canvasHelpers.SetActive(true);

        if (captureMove.captureSad)
        {
            sadCircle.SetActive(true);
            sadObject.SetActive(true);
        }
        if (captureMove.captureCuriosity)
        {
            curiosityCircle.SetActive(true);
            curiosityObject.SetActive(true);
        }
        if (captureMove.captureSad && captureMove.captureCuriosity)
        {
            flamaCircle.SetActive(true);
            flamaObject.SetActive(true);
        }
    }

    public void DeactiveHelpers()
    {
        canvasHelpers.SetActive(false);
    }
}
