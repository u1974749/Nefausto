using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackFloatingText : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0));
    }
    public void activeDestroy()
    {
        StartCoroutine("destroyFloatingText");
    }

    IEnumerator destroyFloatingText()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }
}
