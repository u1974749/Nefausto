using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public static bool active = true;

    void Start()
    {
        if(active)
        {
            tutorial.SetActive(true);
            active = false;
            CaptureInstance.instanceFlama = false;
            StartCoroutine(deleteTutorial());
        }
    }

    IEnumerator deleteTutorial()
    {
        yield return new WaitForSeconds(8);
        CaptureInstance.instanceFlama = true;
        tutorial.SetActive(false);
    }
}
