using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackModels : MonoBehaviour
{
    public GameObject iconCat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if ((this.name == "EmptyModel" && !captureMove.captureSad) ||
                (this.name == "LouderModel" && !captureMove.captureCuriosity))
                iconCat.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            iconCat.SetActive(false);
    }
}
