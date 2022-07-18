using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] int wave;
    [SerializeField] Transform leftSpawn;
    [SerializeField] Transform rightSpawn;
    [SerializeField] Transform[] enemies;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] TextMeshProUGUI waveText;

    [Header("Runtime")]
    [SerializeField] int waveEnemyCount;

    void Awake () => instance = this;

    void Start () 
    {
        SpawnNextWave();
    }

    public void SpawnNextWave () 
    {
        wave++;
        waveText.text = "Wave " + wave;

        if (wave % 5 != 0) 
        {
            int formula = (wave * 2) + 2;
            waveEnemyCount = formula;

            StartCoroutine(SpawnEnemies(waveEnemyCount));
        }

        else 
        {
            SpawnShopAndHealing();
        }
    }

    IEnumerator SpawnEnemies (int enemyCount, bool spawnLeftside = true)
    {
        Transform enemy = enemies[Random.Range(0, enemies.Length)];
        //print(enemyCount);

        if (spawnLeftside)
            Instantiate(enemy, leftSpawn.position, Quaternion.identity);
        else
            Instantiate(enemy, rightSpawn.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);

        // base call, recursion call
        if (enemyCount > 1) 
        {
            StartCoroutine(SpawnEnemies(enemyCount - 1, !spawnLeftside));
        }
    }

    public void DecEnemyCount () 
    {
        waveEnemyCount -= 1;
        
        if (waveEnemyCount <= 0) 
        {
            StartCoroutine(Intermission());
        }
    }

    IEnumerator Intermission () 
    {
        yield return new WaitForSeconds(5f);
        SpawnNextWave();
    }

    void SpawnShopAndHealing () 
    {
        print("spawning shop and healing orbs");
    }
}
