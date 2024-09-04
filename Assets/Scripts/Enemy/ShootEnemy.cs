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
    [SerializeField] public GameObject attackPoint;
    public static bool isShooting = false;

    private void Start()
    {
        StartCoroutine(waitTut());
    }

    void Update()
    {
        if(isShooting)
            StartCoroutine(waitAnimAttack());
    }

    void Shoot()
    {
        
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;

        GameObject bulletObject = Instantiate(bullet, spawnPoint.position, transform.rotation);
        bulletObject.GetComponent<Rigidbody>().velocity = transform.forward * speed; // Asigna la velocidad al proyectil

        Destroy(bulletObject, 5f);
    }

    IEnumerator waitTut()
    {
        yield return new WaitForSeconds(8);
        isShooting = true;
    }

    IEnumerator waitAnimAttack()
    {
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }
}
