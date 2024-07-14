using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //public Transform target;

    public float minTargX;
    public float maxTargX;
    public float minTargZ;
    public float maxTargZ;
    
    public int waitTimeMin;
    public int waitTimeMax;

    public Vector2 currentTarg;
    public int counterCapture;
    public bool move = true;

    public bool waitTutorial = true;

    void Start()
    {
        if(waitTutorial)
        {
            waitTutorial = false;
            StartCoroutine(waitTut());
        }
        else
        {
            RandomDirection();
            StartCoroutine(changeDirection());
        }
    }

    private void RandomDirection()
    {
        currentTarg.x = Random.Range(minTargX,maxTargX);
        currentTarg.y = Random.Range(minTargZ,maxTargZ);
    }
    IEnumerator changeDirection()
    {
        while (move)
        {
            RandomDirection();
            Vector3 v = new Vector3(currentTarg.x, 0.75f, currentTarg.y);
            transform.GetComponent<NavMeshAgent>().SetDestination(v);
            float time = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator waitTut()
    {
        yield return new WaitForSeconds(8);
        RandomDirection();
        StartCoroutine(changeDirection());
    }

    public void stopMoveEnemy()
    {
        move = false;
        ShootEnemy.isShooting = false;
        StopCoroutine(changeDirection());
    }

    public void startMoveEnemy()
    {
        move = true;
        ShootEnemy.isShooting = true;
        StartCoroutine(changeDirection());
    }
}
