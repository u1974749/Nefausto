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
            StartCoroutine(deleteTutorial());
        }
    }

    IEnumerator deleteTutorial()
    {
        yield return new WaitForSeconds(8);
        tutorial.SetActive(false);
    }
}
