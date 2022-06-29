using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int round;
    [SerializeField] Transform leftSpawn;
    [SerializeField] Transform rightSpawn;
    [SerializeField] Transform[] enemies;
    [SerializeField] float spawnDelay = 1f;

    void Start () 
    {
        StartCoroutine(StartWave(10));
    }

    IEnumerator StartWave (int enemyCount, bool spawnLeftside = true)
    {
        Transform enemy = enemies[Random.Range(0, enemies.Length)];

        if (spawnLeftside)
            Instantiate(enemy, leftSpawn.position, Quaternion.identity);
        else
            Instantiate(enemy, rightSpawn.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);

        // base call, recursion call
        if (enemyCount > 0) 
            StartCoroutine(StartWave(enemyCount - 1, !spawnLeftside));
    }
}
