using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("captureObject"))
        {
            Debug.Log("CAPTUREOBJECT LOLOLOL");
            GameObject.FindWithTag("styler").GetComponent<captureMove>().ClearColliders(0);
            GameObject.FindWithTag("styler").GetComponent<captureMove>().counterCapture = GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().counterCapture;
        }
        else if (other.CompareTag("attack"))
        {
            GameObject.FindWithTag("styler").GetComponent<captureMove>().ClearColliders(1);
            GameObject.FindWithTag("styler").GetComponent<captureMove>().counterCapture = GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().counterCapture;
        }
    }
}
