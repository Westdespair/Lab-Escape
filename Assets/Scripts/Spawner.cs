using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;
    private int enemyNum = 0;
    public float monsterInterval= 3.5f;
    public int maxEnemyNum = 10;


    private void Start()
    {
        StartCoroutine(spawnEnemy(monsterInterval, monster));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if(enemyNum <maxEnemyNum)
        {
            GameObject newMonster = Instantiate(enemy, transform.position, Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
            enemyNum += 1;
        }
            
    }
}
