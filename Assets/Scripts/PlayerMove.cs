using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyPlayer;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;
    public static Transform faustoPosition;
    public Transform cameraPosition;
    public static bool playerMove = true;

    [SerializeField] private float speed;
    private void Start()
    {
        StartCoroutine(loadPlayerPos());
    }

    private void FixedUpdate()
    {
        if(playerMove)
        {
            rigidbodyPlayer.velocity = new Vector3(joystick.Horizontal * speed, rigidbodyPlayer.velocity.y, joystick.Vertical * speed);
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rigidbodyPlayer.velocity);
            }
        }
    }

    public void savePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        PlayerPrefs.Save();

        PlayerPrefs.SetFloat("CameraX", cameraPosition.position.x);
        PlayerPrefs.SetFloat("CameraY", cameraPosition.position.y);
        PlayerPrefs.SetFloat("CameraZ", cameraPosition.position.z);
        PlayerPrefs.Save();
    }

    public void loadPlayerPosition()
    {
        Vector3 pos = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        transform.position = pos;
        Vector3 posCamera = new Vector3(PlayerPrefs.GetFloat("CameraX"), PlayerPrefs.GetFloat("CameraY"), PlayerPrefs.GetFloat("CameraZ"));
        cameraPosition.transform.position = posCamera;
    }

    IEnumerator loadPlayerPos()
    {
        yield return new WaitForEndOfFrame();
        Vector3 pos = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        transform.position = pos;
        Vector3 posCamera = new Vector3(PlayerPrefs.GetFloat("CameraX"), PlayerPrefs.GetFloat("CameraY"), PlayerPrefs.GetFloat("CameraZ"));
        cameraPosition.transform.position = posCamera;
    }
}
