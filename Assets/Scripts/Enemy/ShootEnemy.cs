using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] public float timer = 15;
    [SerializeField] public float bulletTime;
    [SerializeField] public float speed = 0;
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform spawnPoint;
    
    void Update()
    {
        //spawnPoint = transform;
        Shoot();
    }

    void Shoot()
    {
        bulletTime -=Time.deltaTime;

        if(bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObject = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletRigidbody.transform.forward * speed, ForceMode.Force);
        Destroy(bulletObject, 5f);
    }
}
