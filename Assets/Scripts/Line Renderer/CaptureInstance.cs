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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touch = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6.3f));
            if(instanceFlama)
                Instantiate(linePrefab, touch, Quaternion.identity);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject cp = GameObject.FindWithTag("styler");
            if(cp != null)
            {
                cp.GetComponent<captureMove>().ClearColliders(-1);
                Destroy(cp);
            }
        }
    }
}
