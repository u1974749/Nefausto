using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CleanZone : MonoBehaviour
{
    float speed = 0.3f;
    GameObject lake;
    GameObject lakeTwo;
    bool down = false;
    bool downTwo = false;
    public Collider col;
    public Collider colTwo;

    public static bool completeBluffOne = false;
    public static bool completeBluffSecond = false;

    private void Start()
    {
        if(completeBluffOne) col.enabled = false;
        if(completeBluffSecond) colTwo.enabled = false;
    }

    private void Update()
    {
        if(down)
        {
            lake = GameObject.Find("lake");
            lake.transform.position = new Vector3(lake.transform.position.x, lake.transform.position.y - Time.deltaTime * speed, lake.transform.position.z);
            completeBluffOne = true;
        }
        else if (downTwo)
        {
            lakeTwo = GameObject.Find("lake (1)");
            lakeTwo.transform.position = new Vector3(lakeTwo.transform.position.x, lakeTwo.transform.position.y - Time.deltaTime * 0.45f, lakeTwo.transform.position.z);
            completeBluffSecond = true;
        }
    }
    public void actualizeLakeVenom()
    {
        if (gameObject.GetComponent<selectLights>().checkLights())
        {
            SwitchCamera("FirstLakeCamera");
            gameObject.GetComponent<selectLights>().clearLights();
            down = true;
            GameObject g = GameObject.FindWithTag("styler");
            if(g != null)
            {
                if(g.GetComponent<LineMechanics>() != null)
                    g.GetComponent<LineMechanics>().DestroyAllColliders();
                Destroy(g);
            }
            Destroy(col);
            StartCoroutine("activeWait");
        }
    }

    public void actualizeLakeVenomTwo()
    {
        if (gameObject.GetComponent<selectLights>().checkLights())
        {
            SwitchCamera("SecondLakeCamera");
            gameObject.GetComponent<selectLights>().clearLights();
            //gameObject.GetComponent<selectLights>().cleanLights();
            downTwo = true;
            GameObject g = GameObject.FindWithTag("styler");
            if (g != null)
            {
                if (g.GetComponent<LineMechanics>() != null)
                    g.GetComponent<LineMechanics>().DestroyAllColliders();
                Destroy(g);
            }
            
            Destroy(colTwo);
            StartCoroutine("activeWaitTwo");
        }
    }

    void SwitchCamera(string name)
    {
        Animator camAnimator = GameObject.Find("CameraController").GetComponent<Animator>();
        if (name == "FirstLakeCamera")
            camAnimator.Play("FirstLakeCamera");
        else if (name == "SecondLakeCamera")
            camAnimator.Play("SecondLakeCamera");
        else
        {
            if (this.name == "Cleaner")
                GameObject.Find("DetectMechanic").GetComponent<BluffMechanic>().SwitchMainCamera();
            else if (this.name == "Cleaner (1)")
                GameObject.Find("DetectMechanic (1)").GetComponent<BluffMechanic>().SwitchMainCamera();
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
        PlayerPrefs.SetFloat("VenonX", lake.transform.position.x);
        PlayerPrefs.SetFloat("VenonY", lake.transform.position.y);
        PlayerPrefs.SetFloat("VenonZ", lake.transform.position.z);
        PlayerPrefs.Save();
    }

    public void loadLakePosition()
    {
        lake = GameObject.Find("lake");
        lake.transform.position = new Vector3(PlayerPrefs.GetFloat("VenonX"), PlayerPrefs.GetFloat("VenonY"), PlayerPrefs.GetFloat("VenonZ"));
    }

    IEnumerator activeWaitTwo()
    {
        yield return new WaitForSeconds(2);
        SwitchCamera("Main Camera");
        saveLakePositionTwo();
        downTwo = false;
    }

    public void saveLakePositionTwo()
    {
        PlayerPrefs.SetFloat("VenonXTwo", lakeTwo.transform.position.x);
        PlayerPrefs.SetFloat("VenonYTwo", lakeTwo.transform.position.y);
        PlayerPrefs.SetFloat("VenonZTwo", lakeTwo.transform.position.z);
        PlayerPrefs.Save();
    }

    public void loadLakePositionTwo()
    {
        lakeTwo = GameObject.Find("lake (1)");
        lakeTwo.transform.position = new Vector3(PlayerPrefs.GetFloat("VenonXTwo"), PlayerPrefs.GetFloat("VenonYTwo"), PlayerPrefs.GetFloat("VenonZTwo"));
    }
}
