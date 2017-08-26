using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    private Wave currentWave;
    public int waveNumber = 1;

    public Transform spawnLocation;

    public float preparationTime = 30.0f;
    float countDown;


    private void Start()
    {
        //monsterBatch = new List<GameObject>();
        countDown = preparationTime;

    }

    private void Update()
    {
        if (countDown <= 0)
        {
            CreateWave();
            countDown = preparationTime;

        }

        countDown -= Time.deltaTime;
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
            GameObject monster = Instantiate(monstersPrefab[combination[i] -1], spawnLocation.position,Quaternion.identity);
            monsterBatch.Add(monster);
        }
        
	}
}
