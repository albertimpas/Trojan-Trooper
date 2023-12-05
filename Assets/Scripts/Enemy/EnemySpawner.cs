using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject alienPrefab;

    [SerializeField]
    private GameObject bipedalPrefab;

    [SerializeField]
    private GameObject mechPrefab;

    [SerializeField]
    private float alienInterval = 3.5f;

    [SerializeField]
    private float bipedalInterval = 10f;

    [SerializeField]
    private float mechInterval = 20f;

    // Define the specific spawn positions for swarmer and big swarmer
    [SerializeField]
    private Vector3[] alienSpawnPositions;
    [SerializeField]
    private Vector3[] bipedalSpawnPositions;

    [SerializeField]
    private Vector3[] mechSpawnPositions;


    // Start is called before the first frame update
    void Start()
    {
        alienSpawnPositions = new Vector3[]
        {
            new Vector3(-20f, -2.34503f, 0),
            new Vector3(22f, -2.34503f, 0)
        };

        bipedalSpawnPositions = new Vector3[]
        {
            new Vector3(-20f, -2.34503f, 0),
            new Vector3(22f, -2.34503f, 0)
        };

        mechSpawnPositions = new Vector3[]
        {
            new Vector3(-20f, -2.34503f, 0),
            new Vector3(22f, -2.34503f, 0)
        };

        StartCoroutine(SpawnEnemy(alienInterval, alienPrefab, alienSpawnPositions));
        StartCoroutine(SpawnEnemy(bipedalInterval, bipedalPrefab, bipedalSpawnPositions));
        StartCoroutine(SpawnEnemy(mechInterval, mechPrefab, mechSpawnPositions));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy, Vector3[] spawnPositions)
    {
        yield return new WaitForSeconds(interval);

        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy, spawnPositions));
    }
}
