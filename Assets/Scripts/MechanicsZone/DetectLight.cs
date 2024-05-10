using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    private GameObject fausto; //collision floor
    public Material mat_LightOn; //collision floor
    public Material mat_LightOff; //collision floor
    public GameObject mat_current; //collision floor

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "styler")
        {
            GameObject cleaner = GameObject.Find("Cleaner");
            cleaner.GetComponent<selectLights>().AddLight(gameObject);
            cleaner.GetComponent<CleanZone>().actualizeLakeVenom();
            //gameObject.GetComponentInChildren("Sphere");
            //gameObject = mat_LightOn;
            mat_current.GetComponent<MeshRenderer>().material = mat_LightOn;
        }
    }

    public void OffLight()
    {
        mat_current.GetComponent<MeshRenderer>().material = mat_LightOff;
    }
}
