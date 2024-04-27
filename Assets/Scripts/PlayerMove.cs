using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyPlayer;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;
    //public static Transform faustoPosition;

    [SerializeField] private float speed;

    /*private void Start()
    {
        transform.position = faustoPosition.position;
    }*/

    // Start is called before the first frame update
    /*private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Debug.Log("POSITION " + PlayerPrefs.GetFloat("PlayerX") + " " + PlayerPrefs.GetFloat("PlayerY") + " " + PlayerPrefs.GetFloat("PlayerZ"));
            loadPlayerPosition();
        }
        //else savePlayerPosition();
    }*/

    private void FixedUpdate()
    {
        rigidbodyPlayer.velocity = new Vector3(joystick.Horizontal * speed, rigidbodyPlayer.velocity.y, joystick.Vertical * speed);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(rigidbodyPlayer.velocity);
        //loadPlayerPosition();
        //faustoPosition = transform;
    }

    public void savePlayerPosition()
    {
        Debug.Log("Save position ////");
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        PlayerPrefs.Save();
        Debug.Log("POSITION " + PlayerPrefs.GetFloat("PlayerX") + " " + PlayerPrefs.GetFloat("PlayerY") + " " + PlayerPrefs.GetFloat("PlayerZ"));

        //Debug.Log("SAVE :::" + PlayerPrefs.GetFloat("PlayerX") + " " + PlayerPrefs.GetFloat("PlayerY") + " " + PlayerPrefs.GetFloat("PlayerZ"));
    }

    public void loadPlayerPosition()
    {
        //Debug.Log("LOAD POSITION :::" + PlayerPrefs.GetFloat("PlayerX") + " " + PlayerPrefs.GetFloat("PlayerY") + " " + PlayerPrefs.GetFloat("PlayerZ"));
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));

    }
}
