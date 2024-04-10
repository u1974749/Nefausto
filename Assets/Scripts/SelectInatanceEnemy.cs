using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInatanceEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject belial;
    public GameObject trail;

    void Start()
    {
        if(InteractNPC.enemyInstance == 0)
        {
            belial.SetActive(true);
        }
        else if (InteractNPC.enemyInstance == 1)
        {
            trail.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
