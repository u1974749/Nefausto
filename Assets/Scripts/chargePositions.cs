using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargePositions : MonoBehaviour
{
    public GameObject player;
    public GameObject lake;
    public GameObject lake1;
    public GameObject lake2;

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
        if (PlayerPrefs.HasKey("Venon1X") && PlayerPrefs.HasKey("Venon1Y") && PlayerPrefs.HasKey("Venon1Z"))
        {
            lake.transform.position = new Vector3(PlayerPrefs.GetFloat("Venon1X"), PlayerPrefs.GetFloat("Venon1Y"), PlayerPrefs.GetFloat("Venon1Z"));
        }
        if (PlayerPrefs.HasKey("Venon2X") && PlayerPrefs.HasKey("Venon2Y") && PlayerPrefs.HasKey("Venon2Z"))
        {
            lake.transform.position = new Vector3(PlayerPrefs.GetFloat("Venon2X"), PlayerPrefs.GetFloat("Venon2Y"), PlayerPrefs.GetFloat("Venon2Z"));
        }
    }
}
