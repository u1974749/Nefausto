using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureInstance : MonoBehaviour
{
    public GameObject linePrefab;
    public Camera cam;
    public Collider floor;
    [SerializeField] private float speed;
    public static bool instanceFlama = true;
    public string cleanerName = "";

    public Material materialShine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(System.Environment.MachineName); name of computer
            Vector3 touch = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6.3f));
            if(instanceFlama)
            {
                GameObject line = Instantiate(linePrefab, touch, Quaternion.identity);
                if(captureMove.changeMaterial)
                    line.GetComponent<LineRenderer>().material = materialShine;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject cp = GameObject.FindWithTag("styler");
            if(cp != null)
            {
                if(cp.GetComponent<captureMove>() != null)
                {
                    cp.GetComponent<captureMove>().ClearColliders(-1);
                    StartCoroutine(waitDestroy(cp));
                }
                else if(cp.GetComponent<LineMechanics>() != null)
                {
                    GameObject.Find(cleanerName).GetComponent<selectLights>().cleanLights();
                    cp.GetComponent<LineMechanics>().DestroyAllColliders();
                    Destroy(cp);
                }
            }
        }
    }

    IEnumerator waitDestroy(GameObject cp)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(cp);
    }
}
