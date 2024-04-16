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
    private float minimumSpawnTime;
    [SerializeField]
    private float maximumSpawnTime;
    private float timeUntilSpawn;

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
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0)
        {   
            if(level == 1){
            selected = Type1_EnemyPrefab;
            }
            else{
            selected = Type2_EnemyPrefab;
            }
            Instantiate(selected,new Vector3(Random.Range(-5f,5),Random.Range(-6f,6),0),Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime,maximumSpawnTime);
    }

}