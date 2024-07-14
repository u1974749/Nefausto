using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LinePlay : MonoBehaviour
{
    //DRAW LINE RENDERER
    private Camera cam; //camera
    private Collider floor; //collision floor
    [SerializeField] private float speed; //speed of object follow
    RaycastHit hit;
    Ray ray; //raycast

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        floor = GameObject.Find("FakeFloor").GetComponent<Collider>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(transform.position, hit.point, Color.green);
                if (hit.collider == floor)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * speed);
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                }
            }
        }
    }
}
