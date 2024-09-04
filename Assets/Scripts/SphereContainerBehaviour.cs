using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereContainerBehaviour : MonoBehaviour
{
    //public GameObject sphereContainer;
    //private GameObject sphereContainerInstance;
    public bool sphereContainerGrew;

    void Update()
    {
        if (sphereContainerGrew)
        {
            GameObject captureObject = GameObject.FindWithTag("captureObject");
            gameObject.transform.position = new Vector3(captureObject.transform.position.x, 1.6f, captureObject.transform.position.z);
            gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Grow", (gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Grow") + Time.deltaTime * 0.6f));
        }
    }

    public void activeSphereContainer(GameObject captureObject)
    {
        //gameObject.GetComponent<Animator>().Play("containerShake");
        StartCoroutine("waitDisapearSphere", captureObject);
        sphereContainerGrew = true;
    }

    IEnumerator waitDisapearSphere(GameObject captureObject)
    {
        yield return new WaitForSeconds(2);
        sphereContainerGrew = false;
        if(!Helpers.rootGrew)
            captureObject.GetComponent<EnemyMove>().startMoveEnemy();
        //sphereContainerInstance = gameObject;
        CaptureInstance.instanceFlama = true;
        while (gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Grow") > 0)
            gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Grow", (gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Grow") - Time.deltaTime * 1.5f));
        Destroy(this);
    }

}
