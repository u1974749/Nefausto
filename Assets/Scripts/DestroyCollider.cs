using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyCollider : MonoBehaviour
{
    public static bool attackInmumne = false;
    public void Start()
    {
        gameObject.GetComponent<VisualEffect>().Stop();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("captureObject"))
        {
            GameObject styler = GameObject.FindWithTag("styler");
            if (other.GetComponent<DestroyIllusion>() != null)
            {
                GameObject.FindWithTag("Canvas").GetComponent<UIHealth>().Damage();
                if (styler.GetComponent<captureMove>() != null)
                    styler.GetComponent<captureMove>().ClearColliders(1);
            }
            else
            {
                if (styler.GetComponent<captureMove>() != null)
                    styler.GetComponent<captureMove>().ClearColliders(0);
            }
            if (styler.GetComponent<captureMove>() != null)
                styler.GetComponent<captureMove>().counterCapture = GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().counterCapture;
        }
        else if (other.CompareTag("attack"))
        {
            if(attackInmumne)
            {
                GameObject g = other.GetComponent<GameObject>();
                Destroy(g);
            }
            else
            {
                GameObject.FindWithTag("Canvas").GetComponent<UIHealth>().Damage();
                GameObject styler = GameObject.FindWithTag("styler");
                if(styler.GetComponent<captureMove>() != null)
                    styler.GetComponent<captureMove>().ClearColliders(1);
                styler.GetComponent<captureMove>().counterCapture = GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().counterCapture;
            }
        }
    }

}
