using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    private Wave currentWave;
    public int waveNumber = 1;
    public float waveProgression = 0;

    public Transform spawnLocation;
    bool isWaving = false;
   
    float spawnTimer = 2f;
    float timer = 0;
    int[] combination;
    int spawningMonster = 0;
    private void Start()
    {
        //monsterBatch = new List<GameObject>();
       
       

    }

    private void Update()
    {
        if(GameController.gameState == GameController.GameState.BeginWave)
        {
            if (!isWaving)
            {
                CreateWave();
            }
            else
            {
                waveProgression = spawningMonster / combination.Length;
                //Debug.Log(waveProgression);
                SpawnMonsters();
            }


        }
        if( transform.childCount == 0 && GameController.gameState == GameController.GameState.Action)
        {
            GameController.ChangeGameState(GameController.GameState.EndWave);
            spawningMonster = 0;
        }
        
        

    }

    void CreateWave()
    {
        Debug.Log("Creting wave...");
        currentWave = new Wave(waveNumber * 2, waveNumber);
        combination = currentWave.GetCombinaton();
        waveNumber++;
        isWaving = true;
       
    }
    
   

	void SpawnMonsters()
	{
        
        if(timer <= 0)
        {
            int monsterIndex = combination[spawningMonster] - 1;
            GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocation.position, Quaternion.identity);
            monsterBatch.Add(monster);

            monster.transform.parent = transform;
            spawningMonster++;
            timer = spawnTimer;

        }
        if (spawningMonster >= combination.Length)
        {
            isWaving = false;
            GameController.ChangeGameState(GameController.GameState.Action);
        }

        timer -= Time.deltaTime;

    }
}
