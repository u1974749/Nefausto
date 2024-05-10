using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectLights : MonoBehaviour
{
    public static int numberMax = 6;
    public static int numberLights = 0;
    //public static List<GameObject> numberLights;

    public void AddLight(GameObject g)
    {
        /*if(!numberLights.Contains(g) && g !=null)
            numberLights.Add(g.gameObject);*/
        numberLights++;
    }

    public void subtractionLight(GameObject g)
    {
        //if (numberLights.Contains(g))
         //   numberLights.Remove(g.gameObject);
         numberLights--;
    }

    public bool checkLights()
    {
        //Debug.Log("number of lights " + numberLights);
        return numberLights == numberMax;
    }

    public void cleanLights()
    {
        //numberLights.Clear();
        numberLights = 0;
    }
}
