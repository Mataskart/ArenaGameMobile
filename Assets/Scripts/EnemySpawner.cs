using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Type1_EnemyPrefab;

    [SerializeField]
    private float Type1_EnemyInterval = 5f;

    // Reference to the PlayerScore script
    private PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = FindObjectOfType<PlayerScore>();
        StartCoroutine(spawnEnemy(Type1_EnemyInterval, Type1_EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);

        // Subscribe to the OnEnemyKilled event of the new enemy
        newEnemy.GetComponent<EnemyScript>().OnEnemyKilled += playerScore.Enemy_OnEnemyKilled;

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}