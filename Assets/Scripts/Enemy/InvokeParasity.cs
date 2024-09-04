using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeParasity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.name == "Parasity")
            {
                if(!captureMove.captureParasityOne)
                {
                    gameObject.GetComponent<InteractNPC>().interactParasity();
                }
            }
            else if (this.name == "Parasity (1)")
            {
                if (!captureMove.captureParasityTwo)
                {
                    gameObject.GetComponent<InteractNPC>().interactParasity();
                }
            }
        }
    }
}
