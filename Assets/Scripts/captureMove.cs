using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class captureMove : MonoBehaviour
{
    //DRAW LINE RENDERER
    private Camera cam; //camera
    private Collider floor; //collision floor
    [SerializeField] private float speed; //speed of object follow
    RaycastHit hit;
    Ray ray; //raycast
    private LineRenderer lineRenderer; //styler
    private Vector3 antPosition; // anterior position of line
    [SerializeField] private float minDist; //distance with points

    //COLLIDERS
    [HideInInspector] public List<GameObject> lineColliders = new List<GameObject>();
    [HideInInspector] public List<Vector3> pointsLineRenderer = new List<Vector3>();
    public GameObject prefabCollider;

    //LINE RENDERER LENGTH
    public int lineMaxLength;

    //PROOF
    //public EdgeCollider2D edgeCollider;
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;


    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        floor = GameObject.Find("FakeFloor").GetComponent<Collider>();
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
                    if (Vector3.Distance(transform.position, antPosition) >= minDist)
                    {
                        if (lineRenderer.positionCount >= 4)
                        {
                            if(checkIntersection())
                            {
                                lineRenderer.positionCount = 1;
                                lineRenderer.SetPosition(0, antPosition);
                                pointsLineRenderer.Clear();
                                pointsLineRenderer.Add(antPosition);
                            }

                        }
                        if (lineRenderer.positionCount == 20)
                        {
                            for (int i = 0; i < pointsLineRenderer.Count-1; i++)
                                lineRenderer.SetPosition(i, pointsLineRenderer[i + 1]);
                            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
                            pointsLineRenderer.RemoveAt(1);
                            pointsLineRenderer.Add(transform.position);
                            //GameObject g = lineColliders[0];
                            //lineColliders.RemoveAt(1);
                            //Destroy(g);
                            //lineColliders.Add(prefabCollider);
                            //Instantiate(lineColliders[lineColliders.Count - 1], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            //if (antPosition == transform.position)
                            //{
                              //  lineRenderer.SetPosition(0, transform.position);
                                /*Vector2 newPoint = new Vector2(transform.position.x, transform.position.y);
                                points.Add(newPoint);
                                pointsCount++;*/

                                //collider.isTrigger = true;
                            //}
                            //else
                            //{
                                lineRenderer.positionCount++;
                                /*Vector2 newPoint = new Vector2(transform.position.x, transform.position.y);
                                points.Add(newPoint);
                                pointsCount++;*/
                                lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
                                //lineColliders.Add(prefabCollider);
                                //Instantiate(lineColliders[lineColliders.Count-1], transform.position, Quaternion.identity);

                                //lineLength
                                pointsLineRenderer.Add(transform.position);
                            //}
                        }
                        antPosition = transform.position;
                        /*if (pointsCount > 1)
                            edgeCollider.points = points.ToArray();
                        Instantiate(prefabCollider, transform.position, Quaternion.identity);
                        lineColliders.Add(prefabCollider);*/
                    }
                    /*if (Vector3.Distance(transform.position, antPosition) >= 0.8)
                    {
                        Instantiate(prefabCollider, transform.position, Quaternion.identity);
                        lineColliders.Add(prefabCollider);
                    }*/
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("YEAH");
        Debug.Log(other.name);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("YEAH");
        Debug.Log(other.name);
    }

    //CALCULO DE INTERSECCION
    public bool checkIntersection()
    {
        //vector 3
        Vector3 lastPoint2 = pointsLineRenderer[pointsLineRenderer.Count - 1];
        Vector3 lastPoint1 = pointsLineRenderer[pointsLineRenderer.Count - 2];
        int i = 0;
        bool intersection = false;

        while( i < pointsLineRenderer.Count-3 && !intersection)
        {
            Vector3 currentPoint1 = pointsLineRenderer[i];
            Vector3 currentPoint2 = pointsLineRenderer[i+1];

            if (checkSegmentIntersection(lastPoint2,lastPoint1,currentPoint2,currentPoint1))
            {
                Debug.Log("INTERSECTION");
                intersection = true;
            }
            else //Debug.Log("NOTHING");
                i++;
        }

        return intersection;
    }

    public bool checkSegmentIntersection(Vector3 lastPoint2, Vector3 lastPoint1, Vector3 currentPoint2, Vector3 currentPoint1)
    {
        Vector3 vectorLastPoint = lastPoint2 - lastPoint1;
        Vector3 vectorCurrentPoint = currentPoint2 - currentPoint1;

        if(calculateDeterminant(vectorLastPoint,currentPoint1,currentPoint2,lastPoint1))
        {
            if (calculateDeterminant(vectorCurrentPoint, lastPoint1, lastPoint2, currentPoint1))
                return true;
            else
                return false;
        }
        else return false;
    }

    public bool calculateDeterminant(Vector3 vector, Vector3 pointS2First, Vector3 pointS2Last, Vector3 pointS1)
    {
        double first = 0, second = 0;

        first = ((vector.x * (pointS2First.z - pointS1.z)) - (vector.z * (pointS2First.x - pointS1.x)));
        second = ((vector.x * (pointS2Last.z - pointS1.z)) - (vector.z * (pointS2Last.x - pointS1.x)));

        //Debug.Log(first + " + " + second);
        if ((first >= 0 && second < 0) || (first < 0 && second >= 0))
            return true;
        else return false;
    }
}
