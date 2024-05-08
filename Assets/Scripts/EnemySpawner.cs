using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Type1_EnemyPrefab;
    [SerializeField]
    private GameObject Type2_EnemyPrefab;
    [SerializeField]
    private GameObject Type3_EnemyPrefab;
    [SerializeField]
    private GameObject Type4_EnemyPrefab;
    [SerializeField]
    private GameObject Type5_EnemyPrefab;
    [SerializeField]
    private GameObject Type6_EnemyPrefab;
    [SerializeField]
    private float minimumSpawnTime;
    [SerializeField]
    private float maximumSpawnTime;
    public AudioSource levelTrack1;
    public AudioSource levelTrack2;
    public AudioSource levelTrack3;
    public AudioSource levelTrack4;
    public AudioSource levelTrack5;
    private float timeUntilSpawn;
    float[] enemyProbabilities = {0.9f, 0.08f, 0.0f, 0.002f, 0f};
    float[] enemyProbabilities2 = {0.6f, 0.15f, 0.15f, 0.095f, 0.005f};
    float[] enemyProbabilities3 = {0.3f, 0.1f, 0.3f, 0.2f, 0.1f};
    float[] enemyProbabilities4 = {0.1f, 0.05f, 0.35f, 0.25f, 0.25f};

    int levelBefore = -1;

    // Start is called before the first frame update
    void Awake(){
        SetTimeUntilSpawn();
    }
    void Update()
    {
        GameObject selected;
        GameObject levelObject = GameObject.Find("Player");
        Level levelScript = levelObject.GetComponent<Level>();
        int level = levelScript.GetLevel();
        if(levelBefore != level){
            CheckLevelFX(levelBefore,level);
            levelBefore = level;
        }
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0)
        {
            float randomValue = Random.value;
            float cumulativeProbability = 0f;
            selected = Type1_EnemyPrefab;
            // Loop through the probabilities to select the enemy type
            for (int i = 0; i < enemyProbabilities.Length; i++) {
                cumulativeProbability += enemyProbabilities[i];
                if (randomValue <= cumulativeProbability) {
                    // Select the corresponding enemy prefab based on the current probability
                    switch (i) {
                        case 0:
                            selected = Type1_EnemyPrefab;
                            break;
                        case 1:
                            selected = Type2_EnemyPrefab;
                            break;
                        case 2:
                            selected = Type3_EnemyPrefab;
                            break;
                        case 3:
                            selected = Type4_EnemyPrefab;
                            break;
                        case 4:
                            selected = Type5_EnemyPrefab;
                            break;
                        // Add more cases if you have additional enemy types
                    }
                    break;
                }
            }
            Instantiate(selected,new Vector3(Random.Range(-5f,5),Random.Range(-6f,6),0),Quaternion.identity);
            SetTimeUntilSpawn();
            if(level == 2)
            enemyProbabilities = enemyProbabilities2;
            if(level == 3)
            enemyProbabilities = enemyProbabilities3;
            if(level == 4)
                    {
                        enemyProbabilities = enemyProbabilities4;
                        timeUntilSpawn = 60f;
                        RemoveAllEnemies();
                        Instantiate(Type6_EnemyPrefab,new Vector3(levelObject.transform.position.x + 2f,levelObject.transform.position.y + 2f,0),Quaternion.identity);
                    } 
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime,maximumSpawnTime);
    }
    private void RemoveAllEnemies()
    {
        // Find all enemy objects and remove them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void CheckLevelFX(int levelBefore, int level)
    {
        if(level == 1){
            if (levelTrack1 != null)
            {
                if (!levelTrack1.isPlaying)
                {
                    levelTrack1.Play();
                }
            }
        }
        if(level == 2){
            if (levelTrack2 != null)
            {
                if (!levelTrack2.isPlaying)
                {
                    levelTrack1.Stop();
                    levelTrack2.Play();
                }
            }
        }
        if(level == 3){
            if (levelTrack3 != null)
            {
                if (!levelTrack3.isPlaying)
                {
                    levelTrack2.Stop();
                    levelTrack3.Play();
                }
            }
        }
        if(level == 4){
            if (levelTrack4 != null)
            {
                if (!levelTrack4.isPlaying)
                {
                    levelTrack3.Stop();
                    levelTrack4.Play();
                }
            }
        }
        if(level == 5){
            if (levelTrack5 != null)
            {
                if (!levelTrack5.isPlaying)
                {
                    levelTrack4.Stop();
                    levelTrack5.Play();
                }
            }
        }
    }
}