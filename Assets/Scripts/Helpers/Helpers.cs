using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    GameObject canvas;
    public void threeHelp()
    {
        GameObject.FindWithTag("Canvas").GetComponent<HelpersUI>().DeactiveHelpers();
        GameObject captureObject = GameObject.FindWithTag("captureObject");
        captureObject.GetComponent<EnemyMove>().stopMoveEnemy();
        StartCoroutine(stopAttack(captureObject));
    }
    IEnumerator stopAttack(GameObject captureObject)
    {
        yield return new WaitForSeconds(10);
        captureObject.GetComponent<EnemyMove>().startMoveEnemy();
    }
}
