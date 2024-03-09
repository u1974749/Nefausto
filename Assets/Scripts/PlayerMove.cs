using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyPlayer;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        rigidbodyPlayer.velocity = new Vector3(joystick.Horizontal * speed, rigidbodyPlayer.velocity.y, joystick.Vertical * speed);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(rigidbodyPlayer.velocity);
    }
}
