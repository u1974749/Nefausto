using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluffMechanic : MonoBehaviour
{
    public GameObject buttonNormal;
    public GameObject buttonMainCamera;
    public GameObject joystick;
    public GameObject line;
    public GameObject lampTutorial;
    public GameObject iconCat;
    bool activeMechanic = false;
    public bool firstLake = false;
    public bool secondLake = false;
    public bool activeTutorial = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activeMechanic = true;
            iconCat.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeMechanic = false;
            iconCat.SetActive(false);
        }
    }

    public void SwitchBluffCamera()
    {
        Animator camAnimator = GameObject.Find("CameraController").GetComponent<Animator>();
        iconCat.SetActive(false);
        if (activeMechanic)
        {
            if (firstLake)
            {
                //firstLake = false;
                LineMechanics.floorName = "DetectMechanic";
                camAnimator.Play("BluffMechanic");
            }
            else if (secondLake)
            {
                //secondLake = false;
                LineMechanics.floorName = "DetectMechanic (1)";
                camAnimator.Play("BluffMechanicTwo");
            }
            line.SetActive(true);
            buttonNormal.SetActive(false);
            joystick.SetActive(false);
            buttonMainCamera.SetActive(true);
            if (activeTutorial)
            {
                lampTutorial.SetActive(true);
                activeTutorial = false;
                StartCoroutine(activeLampTut());
            }
        }
    }

    public void SwitchMainCamera()
    {
        Animator camAnimator = GameObject.Find("CameraController").GetComponent<Animator>();
        camAnimator.Play("Main Camera");
        buttonNormal.SetActive(true);
        joystick.SetActive(true);
        buttonMainCamera.SetActive(false);
        line.SetActive(false);
        activeMechanic = false;

        GameObject g = GameObject.FindWithTag("styler");
        if (g != null)
        {
            if (g.GetComponent<LineMechanics>() != null)
                g.GetComponent<LineMechanics>().DestroyAllColliders();
            Destroy(g);
        }
    }

    IEnumerator activeLampTut()
    {
        yield return new WaitForSeconds(2);
        lampTutorial.SetActive(false);
    }

    public void changeActiveMechanic()
    {
        activeMechanic = !activeMechanic;
    }
}
