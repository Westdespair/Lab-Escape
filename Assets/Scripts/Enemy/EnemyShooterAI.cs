using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooterAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float EnemyRange = 50f;
    private float distanceBetweenTarget;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenTarget = Vector3.Distance(target.position, transform.position);

        if(distanceBetweenTarget <= EnemyRange)
        {
            navMeshAgent.SetDestination(target.position);
        }

       


        if(distanceBetweenTarget <= navMeshAgent.stoppingDistance)
        {
            if (countdownBetweenFire <= 0f)
            {
                foreach (Transform SpawnPoints in projectileSpawnPoint)
                {
                    Instantiate(projectilePrefab, SpawnPoints.position, transform.rotation);
                }

                countdownBetweenFire = 1f / fireRate;
            }
            countdownBetweenFire -= Time.deltaTime;
        }
    }
}
