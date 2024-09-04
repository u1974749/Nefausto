using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargePositions : MonoBehaviour
{
    public GameObject player;
    public GameObject lake;
    public GameObject lake1;

    void Awake()
    {
        if(PlayerPrefs.HasKey("VenonX") && PlayerPrefs.HasKey("VenonY") && PlayerPrefs.HasKey("VenonZ"))
        {
            lake.transform.position = new Vector3(PlayerPrefs.GetFloat("VenonX"), PlayerPrefs.GetFloat("VenonY"), PlayerPrefs.GetFloat("VenonZ"));
        }
        if (PlayerPrefs.HasKey("VenonXTwo") && PlayerPrefs.HasKey("VenonYTwo") && PlayerPrefs.HasKey("VenonZTwo"))
        {
            lake1.transform.position = new Vector3(PlayerPrefs.GetFloat("VenonXTwo"), PlayerPrefs.GetFloat("VenonYTwo"), PlayerPrefs.GetFloat("VenonZTwo"));
        }
    }
}
