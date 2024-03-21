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
        }
        else if (other.CompareTag("attack"))
        {
            GameObject.FindWithTag("styler").GetComponent<captureMove>().ClearColliders(1);
        }
    }
}
