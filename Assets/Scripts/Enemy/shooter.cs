using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private float minShootWaitTime = 1f, maxShootWaitTime = 3f;
    private float waitTime;



    private void Start()
    {
        waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
    }


    void Shoot()
    {
        Instantiate(bullet, bulletSpawnPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > waitTime)
        {
            waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
            Shoot();
        }
    }
}
