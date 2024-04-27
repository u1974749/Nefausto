using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackFloatingText : MonoBehaviour
{
    public void activeDestroy()
    {
        StartCoroutine("destroyFloatingText");
    }

    IEnumerator destroyFloatingText()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
