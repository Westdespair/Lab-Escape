using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    public float monsterInterval= 3.5f;


    private void Start()
    {
        StartCoroutine(spawnEnemy(monsterInterval, monster));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
            yield return new WaitForSeconds(interval);
            GameObject newMonster = Instantiate(enemy, transform.position, Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
    }
}
