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
        /*if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }*/
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
