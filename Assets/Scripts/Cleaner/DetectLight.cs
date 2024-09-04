using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    public Material mat_LightOn; 
    public Material mat_LightOff; 
    public GameObject mat_current; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "styler")
        {
            GameObject cleaner = GameObject.Find(gameObject.transform.parent.name);
            mat_current.GetComponent<MeshRenderer>().material = mat_LightOn;
            cleaner.GetComponent<selectLights>().AddLight(name);
            if(gameObject.transform.parent.name == "Cleaner")
                cleaner.GetComponent<CleanZone>().actualizeLakeVenom();
            else if(gameObject.transform.parent.name == "Cleaner (1)")
                cleaner.GetComponent<CleanZone>().actualizeLakeVenomTwo();
        }
    }

    public void OffLight()
    {
        mat_current.GetComponent<MeshRenderer>().material = mat_LightOff;
    }
}
