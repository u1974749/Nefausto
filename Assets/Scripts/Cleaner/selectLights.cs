using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class selectLights : MonoBehaviour
{
    public int numberMax = 6;
    public List<GameObject> numberLights;

    public void AddLight(string g)
    {
        GameObject light = GameObject.Find(g);
        if(light != null)
            if(numberLights != null && !numberLights.Contains(light))
                numberLights.Add(light);
    }

    public void subtractionLight(GameObject g)
    {
        if (numberLights.Contains(g))
            numberLights.Remove(g.gameObject);
    }

    public bool checkLights()
    {
        return numberLights != null && numberLights.Count == numberMax;
    }

    public void cleanLights()
    {
        if(numberLights != null)
        {
            for (int i = 0; i < numberLights.Count; i++)
                numberLights[i].GetComponent<DetectLight>().OffLight();
            numberLights.Clear();
        }
    }

    public void clearLights()
    {
        numberLights.Clear();
    }
}
