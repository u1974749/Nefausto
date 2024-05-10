using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluffMechanic : MonoBehaviour
{
    public GameObject buttonNormal;
    public GameObject buttonMainCamera;
    public GameObject joystick;
    public GameObject line;
    bool activeMechanic = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activeMechanic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeMechanic = false;
        }
    }

    public void SwitchBluffCamera()
    {
        Animator camAnimator = GameObject.Find("CameraController").GetComponent<Animator>();
        if (activeMechanic)
        {
            camAnimator.Play("BluffMechanic");
            buttonNormal.SetActive(false);
            joystick.SetActive(false);
            buttonMainCamera.SetActive(true);
            line.SetActive(true);
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
    }
}
