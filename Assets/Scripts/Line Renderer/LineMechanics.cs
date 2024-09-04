using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineMechanics : MonoBehaviour
{
    //DRAW LINE RENDERER
    private Camera cam; //camera
    private Collider floor; //collision floor
    public static string floorName = "DetectMechanic"; //collision floor
    [SerializeField] private float speed; //speed of object follow
    RaycastHit hit;
    Ray ray; //raycast
    private LineRenderer lineRenderer; //styler
    private Vector3 antPosition; // anterior position of line
    [SerializeField] private float minDist; //distance with points

    //MATH CALCULATE
    [HideInInspector] public List<Vector3> pointsLineRenderer = new List<Vector3>();

    //COLLIDERS
    [HideInInspector] public List<GameObject> lineColliders = new List<GameObject>();
    public GameObject prefabCollider;

    //LINE RENDERER LENGTH
    public int lineMaxLength = 25;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        floor = GameObject.Find(floorName).GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();
        antPosition = transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        pointsLineRenderer.Add(transform.position);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == floor)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * speed);
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    if (lineRenderer.positionCount >= 4)
                    {
                        if (checkIntersection())
                        {
                            lineRenderer.positionCount = 1;
                            lineRenderer.SetPosition(0, antPosition);
                            pointsLineRenderer.Clear();
                            pointsLineRenderer.Add(antPosition);
                            DestroyAllColliders();
                            GameObject g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                            lineColliders.Add(g);
                            if (floorName == "DetectMechanic")
                            {
                                GameObject.Find("Cleaner").GetComponent<selectLights>().cleanLights();
                                changeLights("Cleaner");
                            }
                            else if (floorName == "DetectMechanic (1)")
                            {
                                GameObject.Find("Cleaner (1)").GetComponent<selectLights>().cleanLights();
                                changeLights("Cleaner (1)");
                            }
                        }

                    }
                    if (Vector3.Distance(transform.position, antPosition) >= minDist)
                    {
                        if (lineRenderer.positionCount == lineMaxLength)
                        {
                            for (int i = 0; i < pointsLineRenderer.Count - 1; i++)
                                lineRenderer.SetPosition(i, pointsLineRenderer[i + 1]);
                            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
                            pointsLineRenderer.RemoveAt(0);
                            pointsLineRenderer.Add(transform.position);

                            GameObject g = lineColliders[0];
                            lineColliders.RemoveAt(0);
                            Destroy(g);
                            g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                            lineColliders.Add(g);
                        }
                        else
                        {
                            lineRenderer.positionCount++;

                            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
                            GameObject g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                            lineColliders.Add(g);

                            //lineLength
                            pointsLineRenderer.Add(transform.position);
                        }
                        antPosition = transform.position;
                    }
                }
            }
        }
    }

    //DELETE COLLIDER
    public void DestroyAllColliders()
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, antPosition);
        pointsLineRenderer.Clear();
        pointsLineRenderer.Add(antPosition);
        for (int i = 0; i < lineColliders.Count; i++)
        {
            Destroy(lineColliders[i]);
        }
        lineColliders.Clear();
    }

    public bool checkIntersection()
    {
        Vector3 lastPoint2 = pointsLineRenderer[pointsLineRenderer.Count - 1];
        Vector3 lastPoint1 = pointsLineRenderer[pointsLineRenderer.Count - 2];
        int i = 0;
        bool intersection = false;

        while (i < pointsLineRenderer.Count - 3 && !intersection)
        {
            Vector3 currentPoint1 = pointsLineRenderer[i];
            Vector3 currentPoint2 = pointsLineRenderer[i + 1];
            if (lastPoint2 == currentPoint1 || lastPoint2 == currentPoint2 || lastPoint1 == currentPoint1 || lastPoint1 == currentPoint2)
                intersection = true;
            else if (checkSegmentIntersection(lastPoint2, lastPoint1, currentPoint2, currentPoint1))
            {
                intersection = true;
            }
            else i++;
        }

        return intersection;
    }

    public bool checkSegmentIntersection(Vector3 lastPoint2, Vector3 lastPoint1, Vector3 currentPoint2, Vector3 currentPoint1)
    {
        Vector3 vectorLastPoint = lastPoint2 - lastPoint1;
        Vector3 vectorCurrentPoint = currentPoint2 - currentPoint1;

        if (calculateDeterminant(vectorLastPoint, currentPoint1, currentPoint2, lastPoint1, lastPoint2))
        {
            if (calculateDeterminant(vectorCurrentPoint, lastPoint1, lastPoint2, currentPoint1, currentPoint2))
                return true;
            else
                return false;
        }
        else return false;
    }

    public bool calculateDeterminant(Vector3 vector, Vector3 pointS2First, Vector3 pointS2Last, Vector3 pointS1, Vector3 pointS1Last)
    {
        double first = 0, second = 0;

        first = ((vector.x * (pointS2First.z - pointS1.z)) - (vector.z * (pointS2First.x - pointS1.x)));
        second = ((vector.x * (pointS2Last.z - pointS1.z)) - (vector.z * (pointS2Last.x - pointS1.x)));

        if ((first > 0 && second < 0) || (first < 0 && second > 0))
            return true;
        else return false;
    }

    public void changeLights(string cleaner)
    {
        GameObject g = GameObject.Find(cleaner);

        DetectLight[] f = g.GetComponentsInChildren<DetectLight>();
        for (int i = 0; i < f.Length; i++)
        {
            f[i].gameObject.GetComponent<DetectLight>().OffLight();
        }
    }
}