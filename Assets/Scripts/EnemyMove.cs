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

    public Vector2 currentTarg;
    void Start()
    {
        RandomDirection();
        StartCoroutine(changeDirection());
    }

    private void RandomDirection()
    {
        currentTarg.x = Random.Range(minTargX,maxTargX);
        currentTarg.y = Random.Range(minTargZ,maxTargZ);
    }
    IEnumerator changeDirection()
    {
        while (true)
        {
            RandomDirection();
            Vector3 v = new Vector3(currentTarg.x, 0.75f, currentTarg.y);
            transform.GetComponent<NavMeshAgent>().SetDestination(v);
            float time = Random.Range(0, 4);
            yield return new WaitForSeconds(time);
        }
    }
}
