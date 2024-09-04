using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    //private Camera cam; //camera
    private GameObject fausto; //collision floor
    public static int enemyInstance = 0; //select enemy 0-Sad 1-Curious 2-Hook 3-ParasityOne 4-ParasityTwo
    public GameObject iconCat;
    //RaycastHit hit;
    //Ray ray; //raycast
    //[SerializeField] private float minDist; //distance with points

    public void interactNPC()
    {
        FindObjectOfType<AudioManager>().Play("menuButton");
        fausto = GameObject.Find("FaustoModel");
        if (Vector3.Distance(transform.position, fausto.transform.position) <= 5)
        {
            if (this.name == "EmptyModel")
            {
                enemyInstance = 0;
                if(!captureMove.captureSad)
                    GetComponent<NPCDialogue>().ActiveIntroduction();
            }
            else if (this.name == "LouderModel")
            {
                enemyInstance = 1;
                if (!captureMove.captureCuriosity)
                    GetComponent<NPCDialogue>().ActiveIntroduction();
            }
            else if (this.name == "FabioModel")
            {
                enemyInstance = 2;
                GetComponent<NPCDialogue>().ActiveIntroduction();
            }
            if (iconCat != null)
                iconCat.SetActive(false);
        }
    }

    public void interactParasity()
    {
        FindObjectOfType<AudioManager>().Play("menuButton");
        PlayerMove.playerMove = false;
        if (this.name == "Parasity")
        {
            enemyInstance = 3;
            if (!captureMove.captureParasityOne)
                GetComponent<NPCDialogue>().ActiveIntroductionParasity();
        }
        if (this.name == "Parasity (1)")
        {
            enemyInstance = 4;
            if (!captureMove.captureParasityTwo)
                GetComponent<NPCDialogue>().ActiveIntroductionParasity();
        }
    }

    public int enemySelect()
    {
        return enemyInstance;
    }
}
