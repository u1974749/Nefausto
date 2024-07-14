using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEnemy : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> illusion = new List<GameObject>();
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public Transform spawnPoint2;

    void Update()
    {
        StartCoroutine("activeIllusion");
    }

    // Update is called once per frame
    IEnumerator activeIllusion()
    {
        yield return new WaitForSeconds(2);
        //while (true)
        //{
            if(illusion.Count == 0)
            {
                illusion.Add(Instantiate(enemy, spawnPoint.position, Quaternion.identity));
                illusion.Add(Instantiate(enemy, spawnPoint2.position, Quaternion.identity));
                Debug.Log("DATE CUENTA ");
            }
            else if (illusion[0] == null && illusion[1] == null)
            {
                illusion[0] = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                illusion[1] = Instantiate(enemy, spawnPoint2.position, Quaternion.identity);
            }
        //}
    }

    public void deleteDouble(GameObject copy)
    {
        bool founded = false;
        int i = 0;

        if (illusion.Contains(copy))
        {
            while(!founded && i < illusion.Count)
            {
                if (copy == illusion[i])
                {
                    GameObject g = illusion[i];
                    illusion.RemoveAt(i);
                    Destroy(g);
                    founded = true;
                }
                else i++;
            }
        }
    }
}
