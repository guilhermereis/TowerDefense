﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    public int[] monstersType;

    public int totalMonsters = 3;

    int waveMonsters = 0;

    public GameObject King1;
    
#region Waves
    private Wave waveLane1;
    private Wave waveLane2;
    private Wave waveLane3;
    private Wave waveLane4;
#endregion

#region SpawnLocations
    public Transform spawnLocationLane1;
    public Transform spawnLocationLane2;
    public Transform spawnLocationLane3;
    public Transform spawnLocationLane4;
    #endregion

#region SpawningMonstersNumber
    public int spawningMonsterLane1 = 0;
    public int spawningMonsterLane2 = 0;
    public int spawningMonsterLane3 = 0;
    public int spawningMonsterLane4 = 0;
    #endregion

#region Combinations
    int[] combinationLane1;
    int[] combinationLane2;
    int[] combinationLane3;
    int[] combinationLane4;
    #endregion

    public int waveNumber = 1;
#region WaveNumber
    public int waveNumberLane1 = 1;
    public int waveNumberLane2 = 1;
    public int waveNumberLane3 = 1;
    public int waveNumberLane4 = 1;
#endregion



    public float waveProgression = 0;

    public Transform spawnLocation;
    bool isWaving = false;


    float spawnTimer = 2f;

    public Canvas hud;

  
    int finishedSpawns = 0;
    int maxLanes = 1;

    private void Start()
    {
        monstersType = new int[totalMonsters];
        Canvas[] canvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].CompareTag("HUD"))
            {
                hud = canvas[i];
                break;
            }
        }

    }


    //return total monsters spawned at moment
    public float GetTotalSpawningMonsters()
    {
        return (float) (spawningMonsterLane1 + spawningMonsterLane2 + spawningMonsterLane3 + spawningMonsterLane4);
    }

    IEnumerator SpawnLane1(int[] combination_)
    {
        float timer = 0;

        //spawns king
        if( waveNumberLane1 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane1.position, Quaternion.identity);
            monster.GetComponent<PawnController>().SetupWaypoints(1);
            monsterBatch.Add(monster);
        }

        while (spawningMonsterLane1 < combination_.Length)
        {

            if(timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane1] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane1.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(1);
                monsterBatch.Add(monster);

                monster.transform.parent = transform;
                spawningMonsterLane1++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();

            }
            timer -= Time.deltaTime;
            yield return null;

        }

        finishedSpawns++;
        if (finishedSpawns >= maxLanes)
        {
            isWaving = false;
            GameController.ChangeGameState(GameState.Action);
        }
    }

    IEnumerator SpawnLane2(int[] combination_)
    {
        float timer = 0;
        //spawns king
        if (waveNumberLane2 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane2.position, Quaternion.identity);
            monster.GetComponent<PawnController>().SetupWaypoints(2);
            monsterBatch.Add(monster);
        }
        while (spawningMonsterLane2 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane2] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane2.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(2);
                monsterBatch.Add(monster);

                monster.transform.parent = transform;
                spawningMonsterLane2++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();

            }
            timer -= Time.deltaTime;
            yield return null;

        }

        finishedSpawns++;
        if (finishedSpawns >= maxLanes)
        {
            isWaving = false;
            GameController.ChangeGameState(GameState.Action);
        }

    }

    IEnumerator SpawnLane3(int[] combination_)
    {
        float timer = 0;
        //spawns king
        if (waveNumberLane3 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane3.position, Quaternion.identity);
            monster.GetComponent<PawnController>().SetupWaypoints(3);
            monsterBatch.Add(monster);
        }
        while (spawningMonsterLane3 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane3] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane3.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(3);
                monsterBatch.Add(monster);

                monster.transform.parent = transform;
                spawningMonsterLane3++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();

            }
            timer -= Time.deltaTime;
            yield return null;

        }

        finishedSpawns++;
        if (finishedSpawns >= maxLanes)
        {
            isWaving = false;
            GameController.ChangeGameState(GameState.Action);
        }
    }

    IEnumerator SpawnLane4(int[] combination_)
    {
        float timer = 0;

        //spawns king
        if (waveNumberLane4 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane4.position, Quaternion.identity);
            monster.GetComponent<PawnController>().SetupWaypoints(4);
            monsterBatch.Add(monster);
        }

        while (spawningMonsterLane4 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane4] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane2.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(4);
                monsterBatch.Add(monster);

                monster.transform.parent = transform;
                spawningMonsterLane2++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();

            }
            timer -= Time.deltaTime;
            yield return null;

        }

        finishedSpawns++;

        if( finishedSpawns >= maxLanes)
        {
            isWaving = false;
            GameController.ChangeGameState(GameState.Action);
        }
    }


    private void Update()
    {
        if(GameController.gameState == GameState.BeginWave)
        {
            if (!isWaving)
            {
                CreateWave();
            }
            else
            {
                //Debug.Log(waveProgression);
                GameController.ChangeGameState(GameState.Waving);
                StopAllCoroutines();

                //setting when lanes a free to spawn monsters

                if(waveNumber -1 < 10)
                {
                    maxLanes = 1;
                    StartCoroutine("SpawnLane1", combinationLane1);

                }
                else if( waveNumber -1  >= 10 && waveNumber -1 < 20)
                {
                    maxLanes = 2;
                    StartCoroutine("SpawnLane1", combinationLane1);
                    StartCoroutine("SpawnLane2", combinationLane2);

                }
                else if( waveNumber -1  >= 20 && waveNumber -1 < 30)
                {
                    maxLanes = 3;
                    StartCoroutine("SpawnLane1", combinationLane1);
                    StartCoroutine("SpawnLane2", combinationLane2);
                    StartCoroutine("SpawnLane3", combinationLane3);
                }
                else
                {
                    maxLanes = 4;
                    StartCoroutine("SpawnLane1", combinationLane1);
                    StartCoroutine("SpawnLane2", combinationLane2);
                    StartCoroutine("SpawnLane3", combinationLane3);
                    StartCoroutine("SpawnLane4", combinationLane4);
                }

                
            }


        }
        else if(GameController.gameState == GameState.Action)
        {
            //hud.transform.Find("Wave").transform.Find("Progress").GetComponent<Text>().text = "100%";
            if(transform.childCount == 0)
                GameController.ChangeGameState(GameState.EndWave);
        }
    }

    public void FillMonstersType(int[] combination)
    {
        monstersType = new int[totalMonsters];
        for (int i = 0; i < combination.Length; i++)
        {
            monstersType[combination[i]-1]++;
           
        }
    }


    //waves set up
    void CreateWave()
    {
        finishedSpawns = 0;
        //reseting monsters indexes;
        spawningMonsterLane1 = 0;
        spawningMonsterLane2 = 0;
        spawningMonsterLane3 = 0;
        spawningMonsterLane4 = 0;

        waveMonsters = 0;

        if(maxLanes == 1)
        {
            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            combinationLane1 = waveLane1.GetCombinaton();
            waveNumberLane1++;
            waveMonsters += combinationLane1.Length;
        }
        else if(maxLanes == 2)
        {
            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length;

            waveNumberLane1++;
            waveNumberLane2++;
        }
        else if(maxLanes == 3)
        {
            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);
            waveLane3 = new Wave(waveNumberLane3 * 2, waveNumberLane3);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length;

            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;
        }
        else
        {
            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);
            waveLane3 = new Wave(waveNumberLane3 * 2, waveNumberLane3);
            waveLane4 = new Wave(waveNumberLane4 * 2, waveNumberLane4);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();
            combinationLane4 = waveLane4.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length + combinationLane4.Length;

            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;
            waveNumberLane4++;
        }
        
        waveNumber++;
        
        

       
       
     


        
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterText").GetComponent<Text>().text = "" + waveNumber;
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterTextShadow").GetComponent<Text>().text = "" + waveNumber;
        //FillMonstersType();
        monsterBatch.Clear();
        isWaving = true;
    }

	//void SpawnMonsters()
	//{
 //       if(timer <= 0)
 //       {
 //           int monsterIndex = combination[spawningMonster] - 1;
 //           GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocation.position, Quaternion.identity);
 //           monsterBatch.Add(monster);

 //           monster.transform.parent = transform;
 //           spawningMonster++;
 //           timer = spawnTimer;

 //           //wave progression
 //           waveProgression = (float)spawningMonster / (float)combination.Length;
 //           hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();
 //       }

 //       if (spawningMonster >= combination.Length)
 //       {
 //           isWaving = false;
 //           GameController.ChangeGameState(GameState.Action);
 //       }

 //       timer -= Time.deltaTime;
 //   }
}
