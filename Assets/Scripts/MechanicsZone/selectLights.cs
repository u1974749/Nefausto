using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class selectLights : MonoBehaviour
{
    public static int numberMax = 6;
    //public static int numberLights = 0;
    public List<GameObject> numberLights;

    public void AddLight(string g)
    {
        //Debug.Log("light type : "+ numberLights.Count);
        GameObject light = GameObject.Find(g);
        if(light != null)
            if(numberLights != null && !numberLights.Contains(light))
                numberLights.Add(light);
        //numberLights++;
    }

    public void subtractionLight(GameObject g)
    {
        if (numberLights.Contains(g))
        {
            //g.GetComponent<DetectLight>().OffLight();
            numberLights.Remove(g.gameObject);
        }
         //numberLights--;
    }

    public bool checkLights()
    {
        //Debug.Log("NUMBER OF LIGHTS: "+numberLights.Count);
        return numberLights != null && numberLights.Count == numberMax;
    }

    public void cleanLights()
    {
        if(numberLights != null)
        {
            for (int i = 0; i < numberLights.Count; i++)
            {
                numberLights[i].GetComponent<DetectLight>().OffLight();
            }
            numberLights.Clear();
        }
        //numberLights = 0;
    }

    public void clearLights()
    {
        numberLights.Clear();
    }
}
