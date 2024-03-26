using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
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

    //MATH CALCULATE
    [HideInInspector] public List<Vector3> pointsLineRenderer = new List<Vector3>();
    public List<Vector3> pointsLineRendererCopy = new List<Vector3>(); //DEBUG
    public int counterPointsCollide = 0;
    public int counterIntersection = 0;
    public Vector2 pointIntersection = new Vector2();

    //COLLIDERS
    [HideInInspector] public List<GameObject> lineColliders = new List<GameObject>();
    public GameObject prefabCollider;

    //LINE RENDERER LENGTH
    public int lineMaxLength = 20;

    //PROOF
    int counterCapture = 0;

    //EDGE COLLIDERS
    //public EdgeCollider2D edgeCollider;
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    //DEBUG
    int life = 5;
    //public TextMeshProUGUI life_ui;

    private void Start()
    { 
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        floor = GameObject.Find("FakeFloor").GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();
        antPosition = transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        pointsLineRenderer.Add(transform.position);
        //Init();
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
                    if (lineRenderer.positionCount >= 4)
                    {
                        /*if(checkCollision())
                        {
                            Debug.Log("COLLISION");
                            lineRenderer.positionCount = 1;
                            lineRenderer.SetPosition(0, antPosition);
                            pointsLineRenderer.Clear();
                            pointsLineRenderer.Add(antPosition);
                        }*/
                        if (checkIntersection())
                        {
                            pointsLineRendererCopy = pointsLineRenderer;
                            //if(detectInCircle())

                            if (WindingNumber()) Debug.Log("I'M IN ?");
                            else Debug.Log("NO IN");
                            /*if (detectInCircleVersion2())
                            {
                                counterCapture++;
                                Debug.Log("DENTRO "+counterCapture);
                            }*/
                            lineRenderer.positionCount = 1;
                            lineRenderer.SetPosition(0, antPosition);
                            pointsLineRenderer.Clear();
                            pointsLineRenderer.Add(antPosition);
                            DestroyAllColliders();
                            //life--;
                            //life_ui.text = life.ToString();
                        }

                    }
                    if (Vector3.Distance(transform.position, antPosition) >= minDist)
                    {
            
                        if (lineRenderer.positionCount == lineMaxLength)
                        {
                            for (int i = 0; i < pointsLineRenderer.Count-1; i++)
                                lineRenderer.SetPosition(i, pointsLineRenderer[i + 1]);
                            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
                            pointsLineRenderer.RemoveAt(0);
                            pointsLineRenderer.Add(transform.position);

                            GameObject g = lineColliders[0];
                            lineColliders.RemoveAt(0);
                            Destroy(g);
                            g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                            lineColliders.Add(g);
                            //g.transform.position = transform.position;
                            //lineColliders.Add(g);
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
                                GameObject g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                                lineColliders.Add(g);

                                //lineLength
                                pointsLineRenderer.Add(transform.position);
                            //}
                        }
                        antPosition = transform.position;
                        //AddMeshColliderVertices(vertices.Length);
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

    //DELETE COLLIDER
    void DestroyAllColliders()
    {
        for(int i = 0; i < lineColliders.Count; i++)
        {
            Destroy(lineColliders[i]);
        }
        lineColliders.Clear();
        GameObject g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
        lineColliders.Add(g);
    }

    public void ClearColliders(int option)
    {
        //if option == 0 is a enemy, else if option == 1 is a attack

        if (option == 0)
        {
            //ANIMATION CONFUSE + PARTICLES
        }
        else if(option == 1)
        {
            //ANIMATION EXPLODE STYLER + PARTICLES
            life--;
            //life_ui.text = life.ToString();
            Debug.Log("LIFE: " + life);
        }

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, antPosition);
        pointsLineRenderer.Clear();
        pointsLineRenderer.Add(antPosition);
        DestroyAllColliders();
    }

    //CALCULATE INTERSECTION
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
            if(lastPoint2 == currentPoint1 || lastPoint2 == currentPoint2 || lastPoint1 == currentPoint1 || lastPoint1 == currentPoint2)
                intersection = true;
            else if (checkSegmentIntersection(lastPoint2,lastPoint1,currentPoint2,currentPoint1))
            {
                //else if(doIntersect(lastPoint2,lastPoint1,currentPoint2,currentPoint1))
                Debug.Log("INTERSECTION");
                counterPointsCollide = i;
                pointIntersection = calculateIntersection(lastPoint2, lastPoint1, currentPoint2, currentPoint1);

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

        //Debug.Log("CALCULATE");
        //Debug.Log("current "+ currentPoint1 + " " + currentPoint2);
        //Debug.Log("last point "+ lastPoint1 + " " + lastPoint2);
        if(calculateDeterminant(vectorLastPoint,currentPoint1,currentPoint2,lastPoint1, lastPoint2))
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

        //Debug.Log(first + " + " + second);
        /*if(first == 0  && second == 0)
        {
            if( ((pointS1.x <= pointS2First.x && pointS1.z <= pointS2First.z) && (pointS2First.x <= pointS1Last.x && pointS2First.z <= pointS1Last.z)) ||
                ((pointS1.x <= pointS2Last.x && pointS1.z <= pointS2Last.z) && (pointS2Last.x <= pointS1Last.x && pointS2Last.z <= pointS1Last.z)) ||
                ((pointS2First.x <= pointS1.x && pointS2First.z <= pointS1.z) && (pointS1.x <= pointS2Last.x && pointS1.z <= pointS2Last.z)))
            {
                //C ? AB o D ? AB o A ? CD
                Debug.Log(" == ");
                return true;
            }
            else return false;
        }*/

        //COLLINEAR
        if (first == 0 && onSegment(pointS2First, pointS2Last, pointS1)) return true;
        if (second == 0 && onSegment(pointS2Last, pointS1, pointS1Last)) return true;
        if ((first > 0 && second < 0) || (first < 0 && second > 0))
            return true;
        else return false;
    }

    public Vector2 calculateIntersection(Vector3 lastPoint2, Vector3 lastPoint1, Vector3 currentPoint2, Vector3 currentPoint1)
    {
        Vector2 vAB = new Vector2 (lastPoint2.x -lastPoint1.x, lastPoint2.z - lastPoint1.z);
        Vector2 vAC = new Vector2 (currentPoint1.x -lastPoint1.x, currentPoint1.z - lastPoint1.z);
        Vector2 vCD = new Vector2 (currentPoint2.x -currentPoint1.x, currentPoint2.z - currentPoint1.z);

        float landa = ((vAC.x*vCD.y)-(vAC.y*vCD.x)) / ((vAB.x*vCD.y)-(vAB.y*vCD.x));
        Vector2 intersection = new Vector2((lastPoint1.x + (landa * vAB.x)), (lastPoint1.z + (landa * vAB.y)));

        return intersection;
    }

    //DETECTION IN POLYGON VERSION 2
    public float MIN(float x, float z)
    {
        return (x < z ? x : z);
    }

    public float MAX(float x, float z)
    {
        return (x > z ? x : z);
    }

    public bool detectInCircleVersion2()
    {
        Transform captureObject = GameObject.FindWithTag("captureObject").GetComponent<Transform>();
        Debug.Log("POSITION " + captureObject.position);
        Debug.Log("RESULTS " + counterPointsCollide + " " + (pointsLineRenderer.Count - 1));
        Debug.Log("pointoo + " + pointIntersection);
        List<Vector3> pointsLineAux = pointsLineRenderer;
        //pointsLineAux.Add(pointIntersection);
        int counter = 0;
        double xinters;
        Vector3 p1, p2;

        p1 = pointsLineAux[counterPointsCollide];
        for (int i = counterPointsCollide+1; i <= pointsLineAux.Count; i++)
        {
            p2 = pointsLineAux[i % pointsLineAux.Count];
            if (captureObject.position.z > MIN(p1.z, p2.z))
            {
                if (captureObject.position.z <= MAX(p1.z, p2.z))
                {
                    if (captureObject.position.x <= MAX(p1.x, p2.x))
                    {
                        if (p1.z != p2.z)
                        {
                            xinters = (captureObject.position.z - p1.z) * (p2.x - p1.x) / (p2.z - p1.z) + p1.x;
                            if (p1.x == p2.x || captureObject.position.x <= xinters)
                                counter++;
                        }
                        //float a = 2 * Mathf.PI; //como se usa pi
                    }
                }
            }
            p1 = p2;
        }

        if (counter % 2 == 0)
            return false;
        else
            return true;
    }

    //VERSION 4 DETECT CIRCLE (((((((ACTIVO)))))))))

    static int IsLeft(Vector3 a, Vector3 b, Vector3 point)
    {
        return (int)((b.x - a.x) * (point.z - a.z)
                    - (point.x - a.x) * (b.z - a.z));
    }

    public bool WindingNumber()
    {
        Transform captureObject = GameObject.FindWithTag("captureObject").GetComponent<Transform>();
        List<Vector3> pointsLineAux = pointsLineRenderer;
        int vCount = pointsLineAux.Count;
        if (vCount < 2) return (captureObject.position.x == pointsLineAux[0].x && captureObject.position.z == pointsLineAux[0].z);

        int wn = 0;
        for (int i = counterPointsCollide+1, j = vCount - 1; i < vCount; j = i++)
        {
            var a = pointsLineAux[j];
            var b = pointsLineAux[i];

            if (b.z <= captureObject.position.z)
            {
                // start y <= P.y
                if (pointsLineAux[j].z > captureObject.position.z)      // an upward crossing
                    if (IsLeft(b, a, captureObject.position) > 0) // P left of  edge
                        ++wn;                    // have  a valid up intersect
            }
            else
            {
                // start y > P.y (no test needed)
                if (a.z <= captureObject.position.z)              // a downward crossing
                    if (IsLeft(b, a, captureObject.position) < 0) // P right of  edge
                        --wn;                    // have  a valid down intersect
            }
        }

        return wn != 0;
        //return wn%2== 0;
    }

    //VERSION 2 CALCULAR INTERSECCION NO OPERATIVA
    static bool onSegment(Vector3 p, Vector3 q, Vector3 r) //UTILIZANDO
    {
        if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Min(p.x, r.x) &&
            q.z <= Math.Max(p.z, r.z) && q.z >= Math.Min(p.z, r.z))
            return true;
        return false;
    }

    static int orientation(Vector3 p, Vector3 q, Vector3 r)
    {
        // See 
        // for details of below formula. 
        float val = (int)((q.z - p.z) * (r.x - q.x) -
                (q.x - p.x) * (r.z - q.z));

        if (val == 0) return 0; // collinear 

        return (val > 0) ? 1 : 2; // clock or counterclock wise 
    }

    static bool doIntersect(Vector3 p1, Vector3 q1, Vector3 p2, Vector3 q2)
    {
        // Find the four orientations needed for general and 
        // special cases 
        int o1 = orientation(p1, q1, p2);
        int o2 = orientation(p1, q1, q2);
        int o3 = orientation(p2, q2, p1);
        int o4 = orientation(p2, q2, q1);

        // General case 
        if (o1 != o2 && o3 != o4)
            return true;

        // Special Cases 
        // p1, q1 and p2 are collinear and p2 lies on segment p1q1 
        if (o1 == 0 && onSegment(p1, p2, q1)) return true;

        // p1, q1 and q2 are collinear and q2 lies on segment p1q1 
        if (o2 == 0 && onSegment(p1, q2, q1)) return true;

        // p2, q2 and p1 are collinear and p1 lies on segment p2q2 
        if (o3 == 0 && onSegment(p2, p1, q2)) return true;

        // p2, q2 and q1 are collinear and q1 lies on segment p2q2 
        if (o4 == 0 && onSegment(p2, q1, q2)) return true;

        return false; // Doesn't fall in any of the above cases 
    }
}
