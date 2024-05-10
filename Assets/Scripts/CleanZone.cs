using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CleanZone : MonoBehaviour
{
    float speed = 0.3f;
    GameObject lake;
    GameObject lake1;
    GameObject lake2;
    bool down = false;
    public Collider col;

    private void Update()
    {
        if(down)
        {
            lake = GameObject.Find("lake");
            lake.transform.position = new Vector3(lake.transform.position.x, lake.transform.position.y - Time.deltaTime * speed, lake.transform.position.z);
        }

    }
    public void actualizeLakeVenom()
    {
        if (gameObject.GetComponent<selectLights>().checkLights())
        {
            SwitchCamera("FirstLakeCamera");
            gameObject.GetComponent<selectLights>().cleanLights();
            down = true;
            GameObject g = GameObject.FindWithTag("styler");
            if(g != null)
            {
                if(g.GetComponent<LineMechanics>() != null)
                    g.GetComponent<LineMechanics>().DestroyAllColliders();
                Destroy(g);
            }
            Destroy(col);
            //if (lake.transform.position.y < -2) checkDown = false;
            StartCoroutine("activeWait");
        }
    }

    void SwitchCamera(string name)
    {
        Animator camAnimator = GameObject.Find("CameraController").GetComponent<Animator>();
        if (name == "FirstLakeCamera")
        {
            camAnimator.Play("FirstLakeCamera");
        }
        else
        {
            GameObject.Find("DetectMechanic").GetComponent<BluffMechanic>().SwitchMainCamera();
            //camAnimator.Play("Main Camera");
        }
    }

    IEnumerator activeWait()
    {
        yield return new WaitForSeconds(2);
        SwitchCamera("Main Camera");
        saveLakePosition();
        down = false;
    }

    public void saveLakePosition()
    {
        Debug.Log("Save position ////");
        PlayerPrefs.SetFloat("VenonX", lake.transform.position.x);
        PlayerPrefs.SetFloat("VenonY", lake.transform.position.y);
        PlayerPrefs.SetFloat("VenonZ", lake.transform.position.z);
        PlayerPrefs.Save();
    }

    public void loadLakePosition()
    {
        lake = GameObject.Find("lake");
        lake.transform.position = new Vector3(PlayerPrefs.GetFloat("VenonX"), PlayerPrefs.GetFloat("VenonY"), PlayerPrefs.GetFloat("VenonZ"));

        //lake1 = GameObject.Find("lake1");
        //lake1.transform.position = new Vector3(PlayerPrefs.GetFloat("Venon1X"), PlayerPrefs.GetFloat("Venon1Y"), PlayerPrefs.GetFloat("Venon1Z"));
    }
}
