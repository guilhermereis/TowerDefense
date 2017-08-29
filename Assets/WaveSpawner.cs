using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    private Wave currentWave;
    public int waveNumber = 1;

    public Transform spawnLocation;

    


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
        
    }

    void CreateWave()
    {
        currentWave = new Wave(waveNumber * 2, waveNumber);
        waveNumber++;
        BeginSpawn();
    }
    

	void BeginSpawn()
	{
        int[] combination = currentWave.GetCombinaton();
        for (int i = 0; i < combination.Length; i++)
        {
            int monsterIndex = combination[i] - 1;
           

            GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocation.position,Quaternion.identity);
            monsterBatch.Add(monster);
            monster.transform.parent = transform;
        }

       


    }
}
