using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    //private Camera cam; //camera
    private GameObject fausto; //collision floor
    public static int enemyInstance = 0; //select enemy
    //RaycastHit hit;
    //Ray ray; //raycast
    //[SerializeField] private float minDist; //distance with points

    /*private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        fausto = GameObject.Find("Fausto");
    }*/

    public void interactNPC()
    {
        fausto = GameObject.Find("Fausto");
        if (Vector3.Distance(transform.position, fausto.transform.position) <= 5)
        {
            Debug.Log("TRAIL ON");
            if (this.name == "BELIAL_Lineart")
            {
                GetComponent<VacíoDialogue>().ActiveIntroduction();
                Debug.Log("BELIAL ON");
                enemyInstance = 0;
            }
            else if (this.name == "trail_cM")
            {
                GetComponent<VacíoDialogue>().ActiveIntroduction();
                Debug.Log("TRAIL ON");
                enemyInstance = 1;
            }
        }
    }

    public int enemySelect()
    {
        return enemyInstance;
    }

    /*void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Transform touch = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime);
                Debug.DrawRay(transform.position, hit.point, Color.green);
                if (hit.collider == this.GetComponent<Collider>())
                {
                    if (Vector3.Distance(transform.position, fausto.transform.position) <= 7)
                    {
                        if (Vector3.Distance(transform.position, hit.point) >= minDist)
                        {
                            if(this.name == "BELIAL_Lineart")
                            {
                                GetComponent<VacíoDialogue>().ActiveIntroduction();
                                Debug.Log("BELIAL ON");
                            }
                            Debug.Log("Touch NICE");
                        }
                    }
                }
            }
        }
    }*/
}
