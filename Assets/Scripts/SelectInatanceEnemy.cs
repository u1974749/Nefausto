using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInatanceEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sad;
    public GameObject curiosity;

    void Start()
    {
        if(InteractNPC.enemyInstance == 0)
        {
            sad.SetActive(true);
        }
        else if (InteractNPC.enemyInstance == 1)
        {
            curiosity.SetActive(true);
        }
    }
}
