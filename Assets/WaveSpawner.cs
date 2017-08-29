using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    private Wave currentWave;
    public int waveNumber = 1;

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
        if(GameController.gc.gameState == GameController.GameState.BeginWave)
        {
            CreateWave();
            GameController.gc.ChangeGameState(GameController.GameState.Action);
        }
        if( transform.childCount == 0 && GameController.gc.gameState == GameController.GameState.Action)
        {
            GameController.gc.ChangeGameState(GameController.GameState.EndWave);
        }
        
        if(isWaving)
        {
            BeginSpawn();
        }

    }

    void CreateWave()
    {

        currentWave = new Wave(waveNumber * 2, waveNumber);
        combination = currentWave.GetCombinaton();
        waveNumber++;
        isWaving = true;
       
    }
    
    private IEnumerator WaitandSpawn(int monsterIndex)
    {
        yield return new WaitForSeconds(3);

    }

	void BeginSpawn()
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
            isWaving = false;

        timer -= Time.deltaTime;

    }
}
