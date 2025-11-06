using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

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
    public int counterPointsCollide = 0;
    public int counterIntersection = 0;
    public Vector2 pointIntersection = new Vector2();

    //COLLIDERS
    [HideInInspector] public List<GameObject> lineColliders = new List<GameObject>();
    public GameObject prefabCollider;

    //LINE RENDERER LENGTH
    public int lineMaxLength = 20;

    //FINISH CAPTURE COUNTER
    public int counterCapture;
    public static bool finishCapture = false;

    //FEEDBACK
    public GameObject floatingText;

    //FOG
    private GameObject fogController;
    public static bool flamaDisolveFog = false;
    public GameObject shineCenter;
    public Material matShine;

    //CURIOUS FEEDBACK
    public static bool changeMaterial = false;
    public Material materialOriginal;

    //HEALTH
    public UIHealth life;

    //SPHERE CONTAINER
    public GameObject sphereContainer;
    private GameObject sphereContainerInstance;
    public bool sphereContainerGrew;

    //CAPTURE CHECKS
    public static bool captureSad = true;
    public static bool captureCuriosity = true;
    public static bool captureHook = false;
    public static bool captureParasityOne = false;
    public static bool captureParasityTwo = false;
    public static bool captureDeath = false;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        floor = GameObject.Find("FakeFloor").GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();
        antPosition = transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        pointsLineRenderer.Add(transform.position);
        counterCapture = GameObject.FindWithTag("captureObject").GetComponent<EnemyMove>().counterCapture;
        if(flamaDisolveFog)
        {
            fogController = GameObject.Find("Fog");
            fogController.GetComponent<VisualEffect>().SetBool("CleanFog", true);
        }
        //FindObjectOfType<AudioManager>().Play("stylerSound");
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

                    if (flamaDisolveFog)
                    {
                        fogController.GetComponent<VisualEffect>().SetFloat("ColliderPosX", hit.point.x);
                        fogController.GetComponent<VisualEffect>().SetFloat("ColliderPosZ", hit.point.z+10);
                        shineCenter.GetComponent<MeshRenderer>().material = matShine;
                    }
                    if (lineRenderer.positionCount >= 4)
                    {
                        if (checkIntersection())
                        {
                            List<GameObject> captureObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("captureObject"));
                            for(int i = 0; i < captureObjects.Count; i++)
                            {
                                if (RayCastPointToPolygon(captureObjects[i].GetComponent<Transform>()))
                                {
                                    if (captureObjects[i].GetComponent<DestroyIllusion>() != null)
                                        captureObjects[i].GetComponent<DestroyIllusion>().eliminateIllusion();
                                    else
                                    {
                                        if (floatingText != null)
                                            ShowFloatingText();
                                        counterCapture--;
                                    }
                                }
                            }

                            lineRenderer.positionCount = 1;
                            lineRenderer.SetPosition(0, antPosition);
                            pointsLineRenderer.Clear();
                            pointsLineRenderer.Add(antPosition);
                            ClearColliders(2);
                            GameObject g = Instantiate(prefabCollider, transform.position, Quaternion.identity);
                            lineColliders.Add(g);

                            if (counterCapture == -1) finishCapture = true;
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
        if (finishCapture)
        {
            //show menus 
            //save dates if complete capture
            //change scene en la posicion indicada
            GameObject captureObject = GameObject.FindWithTag("captureObject");
            if (captureObject.name == "Sad")
                captureSad = true;
            else if (captureObject.name == "Curious")
                captureCuriosity = true;
            else if (captureObject.name == "Hook")
                captureHook = true;
            else if (captureObject.name == "ParasityOne")
                captureParasityOne = true;
            else if (captureObject.name == "ParasityTwo")
                captureParasityTwo = true;
            finishCapture = false;
            GameObject.FindWithTag("GameController").GetComponent<SelectInatanceEnemy>().CaptureFinishUI();
        }
        if(GameObject.FindWithTag("Canvas").GetComponent<UIHealth>().Health() <= 0)
        {
            captureDeath = true;
            SceneManager.LoadScene("Charge");
        }
    }

    //DELETE COLLIDER
    void DestroyAllColliders()
    {
        for (int i = 0; i < lineColliders.Count; i++)
        {
            Destroy(lineColliders[i]);
        }
        lineColliders.Clear();
    }

    public void ClearColliders(int option)
    {
        //if option == 0 is a enemy, else if option == 1 is a attack
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, antPosition);
        pointsLineRenderer.Clear();
        pointsLineRenderer.Add(antPosition);
        switch (option)
        {
            case 0: //ENEMY COLLISION WITH LINE RENDERER
            {
                    //ANIMATION CONFUSE + PARTICLES
                for (int i = 0; i < lineColliders.Count; i++)
                    lineColliders[i].GetComponent<VisualEffect>().Play();
                GameObject captureObject = GameObject.FindWithTag("captureObject");
                captureObject.GetComponent<EnemyMove>().stopMoveEnemy();
                Vector3 pos = new Vector3(captureObject.transform.position.x,1.6f, captureObject.transform.position.z);
                sphereContainerInstance = Instantiate(sphereContainer, pos, Quaternion.identity);
                sphereContainerInstance.GetComponent<SphereContainerBehaviour>().activeSphereContainer(captureObject);
            }
                break;
            case 1: //ATTACK COLLISION
            {
                //ANIMATION EXPLODE STYLER + PARTICLES
                for (int i = 0; i < lineColliders.Count; i++)
                {
                    
                    lineColliders[i].GetComponent<VisualEffect>().SetBool("Explosion", true);
                    lineColliders[i].GetComponent<VisualEffect>().Play();
                }

                FindObjectOfType<AudioManager>().Play("damageAttack");
            }
                break;
            case 2: //PLAYER - STYLER INTERSECTION
            {
                for (int i = 0; i < lineColliders.Count; i++)
                    lineColliders[i].GetComponent<VisualEffect>().Play();
            }
                break;
            case 3: //PLAYER INTERACT MENU HELPERS
            {
                for (int i = 0; i < lineColliders.Count; i++)
                    lineColliders[i].GetComponent<VisualEffect>().Play();
            }
                break;
        }
        StartCoroutine(waitParticles());
    }

    IEnumerator waitParticles()
    {
        yield return new WaitForSeconds(0.2f);
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

        while (i < pointsLineRenderer.Count - 3 && !intersection)
        {
            Vector3 currentPoint1 = pointsLineRenderer[i];
            Vector3 currentPoint2 = pointsLineRenderer[i + 1];
            if (lastPoint2 == currentPoint1 || lastPoint2 == currentPoint2 || lastPoint1 == currentPoint1 || lastPoint1 == currentPoint2)
                intersection = true;
            else if (checkSegmentIntersection(lastPoint2, lastPoint1, currentPoint2, currentPoint1))
            {
                counterPointsCollide = i;
                pointIntersection = calculateIntersection(lastPoint2, lastPoint1, currentPoint2, currentPoint1);

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

    public Vector2 calculateIntersection(Vector3 lastPoint2, Vector3 lastPoint1, Vector3 currentPoint2, Vector3 currentPoint1)
    {
        Vector2 vAB = new Vector2(lastPoint2.x - lastPoint1.x, lastPoint2.z - lastPoint1.z);
        Vector2 vAC = new Vector2(currentPoint1.x - lastPoint1.x, currentPoint1.z - lastPoint1.z);
        Vector2 vCD = new Vector2(currentPoint2.x - currentPoint1.x, currentPoint2.z - currentPoint1.z);

        float landa = ((vAC.x * vCD.y) - (vAC.y * vCD.x)) / ((vAB.x * vCD.y) - (vAB.y * vCD.x));
        Vector2 intersection = new Vector2((lastPoint1.x + (landa * vAB.x)), (lastPoint1.z + (landa * vAB.y)));

        return intersection;
    }

    //VERSION 5 DETECT CIRCLE (((((((ACTIVO)))))))))
    public bool RayCastPointToPolygon(Transform captureObject)
    {
        List<Vector3> pointsLineAux = pointsLineRenderer;
        
        int cnt = 0;
        int vCount = pointsLineAux.Count;
        for (int i = counterPointsCollide + 1, j = vCount - 1; i < vCount; j = i++)
        {
            var a = pointsLineAux[j];
            var b = pointsLineAux[i];
            if (((captureObject.position.z < b.z) != (captureObject.position.z < a.z)) && (captureObject.position.x < (b.x + ((captureObject.position.z - b.z)/(a.z - b.z))*(a.x-b.x))))
                cnt += 1;
        }
        return cnt % 2 == 1;
    }

    public void ShowFloatingText()
    {
        Transform captureObject = GameObject.FindWithTag("captureObject").GetComponent<Transform>();
        Vector3 rot = new Vector3(0, -90, 0);
        Vector3 pos = new Vector3(captureObject.transform.position.x, captureObject.transform.position.y+1.5f, captureObject.transform.position.z+0.4f);
        GameObject feedbackText = Instantiate(floatingText, pos, Quaternion.LookRotation(rot), captureObject.transform);
        feedbackText.GetComponent<TextMesh>().text = counterCapture.ToString();
        feedbackText.GetComponent<FeedbackFloatingText>().activeDestroy();
    }

    public void ChangeMaterialLine()
    {
        gameObject.GetComponent<LineRenderer>().material = materialOriginal;
    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
