using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Type1_EnemyPrefab;

    [SerializeField]
    private float Type1_EnemyInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(Type1_EnemyInterval, Type1_EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}