using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    //private GameObject fausto; //collision floor
    public Material mat_LightOn; //collision floor
    public Material mat_LightOff; //collision floor
    public GameObject mat_current; //collision floor

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "styler")
        {
            Debug.Log("PARENT NAME "+ gameObject.transform.parent.name);
            GameObject cleaner = GameObject.Find(gameObject.transform.parent.name);
            mat_current.GetComponent<MeshRenderer>().material = mat_LightOn;
            cleaner.GetComponent<selectLights>().AddLight(name);
            if(gameObject.transform.parent.name == "Cleaner")
                cleaner.GetComponent<CleanZone>().actualizeLakeVenom();
            else if(gameObject.transform.parent.name == "Cleaner (1)")
                cleaner.GetComponent<CleanZone>().actualizeLakeVenomTwo();
            //gameObject.GetComponentInChildren("Sphere");
            //gameObject = mat_LightOn;
        }
    }

    public void OffLight()
    {
        mat_current.GetComponent<MeshRenderer>().material = mat_LightOff;
    }
}
