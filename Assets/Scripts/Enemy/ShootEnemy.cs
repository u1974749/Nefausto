using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] public float timer = 15;
    [SerializeField] public float bulletTime;
    [SerializeField] public float speed = 100;
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform spawnPoint;
    public static bool isShooting = true;
    //public EnemyMove e;
    
    void Update()
    {
        //spawnPoint = transform;
        if(isShooting)
            Shoot();  
    }

    void Shoot()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;
        /*bulletTime -=Time.deltaTime;
        if(bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObject = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletRigidbody.transform.forward * speed, ForceMode.Force);
        */
        GameObject bulletObject = Instantiate(bullet, spawnPoint.position, transform.rotation);
        bulletObject.GetComponent<Rigidbody>().velocity = transform.forward * speed; // Asigna la velocidad al proyectil
        Destroy(bulletObject, 5f);
    }
}
