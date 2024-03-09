using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 speed = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos,ref speed, smoothTime);
    }
}
