﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public struct Milestone
{
    public int[] combination;
    public int[] special;
     
}


public class WaveSpawner : MonoBehaviour {
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;
    public GameObject castlePrefab;
    private GameObject waveSpawnerUIHolder;
    public WarningBoardController waveWarningBoardController;

    private Minimap minimap;
    Canvas hud_canvas;
    private int saved_money = 0;
    public static int gainSecondChanceCounter = 0;
    public static int secondChanceWaveCountTarget = 30;
    public int[] monstersType;
    public GameObject WaveSpawnerUIPrefab;
    private Dictionary<string, GameObject> waveSpawnerUIs;
    private bool[] openLanes = new bool[4];

    public int totalMonsters = 6;

    int waveMonsters = 0;
    float waveForce;
    public static bool loadingAll = false;

    private int[,] timesMonsterAppearedById;

    AnimationCurve curve;

    public GameObject King1;
    
#region Waves
    private Wave waveLane1;
    private Wave waveLane2;
    private Wave waveLane3;
    private Wave waveLane4;

    private GameObject waveCounterUI;
#endregion

#region SpawnLocations
    public Transform spawnLocationLane1;
    public Transform spawnLocationLane2A;
    public Transform spawnLocationLane2B;
    public Transform spawnLocationLane2;
    public Transform spawnLocationLane3A;
    public Transform spawnLocationLane3B;
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

    public int waveNumber = 0;
#region WaveNumber
    public int waveNumberLane1 = 0;
    public int waveNumberLane2 = 0;
    public int waveNumberLane3 = 0;
    public int waveNumberLane4 = 0;
    #endregion

    public int currentMS1 = 0;
    public int currentMS2 = 0;
    public int currentMS3 = 0;
    public int currentMS4 = 0;



    public float waveProgression = 0;

    public Transform spawnLocation;
    bool isWaving = false;


    float spawnTimer = 2f;

    public Canvas hud;

    bool startedSpawn = false;
  
    int finishedSpawns = 0;
    public int maxLanes = 1;

    GridMouse gridMouse;
    BuildManager buildManager;
    List<PropertyScript.StructureState> listOfStates;
    Shop shop;

    int kingWaveLane1 = -1;
    int kingWaveLane2 = -1;
    int kingWaveLane3 = -1;
    int kingWaveLane4 = -1;
    bool isStartingKing = false;

    int bomberWaveLane1 = -1;
    int bomberWaveLane2 = -1;
    int bomberWaveLane3 = -1;
    int bomberWaveLane4 = -1;
    bool isStartingBomber = false;

    int numberofWavesToBombers = 3;
    int numberOfWavesToKing = 10;

    int[] bomberCombinationLane1;
    int[] bomberCombinationLane2;
    int[] bomberCombinationLane3;
    int[] bomberCombinationLane4;

    private int totalCombinations = 15;
    public float interval = 10;
    public static int repetition = 0;

    public void StartKing(int lane)
    {
        switch (lane)
        {
            case 1:
                kingWaveLane1 = numberOfWavesToKing + waveNumberLane1;
                break;
            case 2:
                kingWaveLane2 = numberOfWavesToKing + waveNumberLane2;
                break;
            case 3:
                kingWaveLane3 = numberOfWavesToKing + waveNumberLane3;
                break;
            case 4:
                kingWaveLane4 = numberOfWavesToKing + waveNumberLane4;
                break;
        }
    }

    public void StartBomberWave(int lane)
    {
        switch (lane)
        {
            case 1:
                bomberWaveLane1 = numberofWavesToBombers + waveNumberLane1;
                break;
            case 2:
                bomberWaveLane2 = numberofWavesToBombers + waveNumberLane2;
                break;
            case 3:
                bomberWaveLane3 = numberofWavesToBombers + waveNumberLane3;
                break;
            case 4:
                bomberWaveLane4 = numberofWavesToBombers + waveNumberLane4;
                break;
        }
    }


    public static Milestone[] combinations;


    public void StartCombinations()
    {
        //1  Wanderer 1
        //2  Warrior 2
        //3  Bomber 3
        //4  King 
        //5  Great wanderer
        //6  Great warrior
        //7  Great bomber
        //8  Great king
        //9  Chief wanderer
        //10 Chief warrior
        //11 Chief bomber
        //12 Chief king
        //13 Lord wanderer
        //14 Lord warrior
        //15 Lord bomber
        //16 Lord king 
        //17 Master wanderer
        //18 Master warrior
        //19 Master bomber
        //20 Master king


        combinations = new Milestone[totalCombinations];
        //creating basics milestones combinations
        combinations[0] = new Milestone();
        combinations[0].combination = new int[] {1,1,1,1};
        combinations[0].special = new int[] {};
        combinations[1] = new Milestone();
        combinations[1].combination = new int[] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
        combinations[1].special = new int[] {2,2,2};
        combinations[2] = new Milestone();
        combinations[2].combination = new int[] {1,1,1,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2};
        combinations[2].special = new int[] {3,3,3,3,3};
        combinations[3] = new Milestone();
        combinations[3].combination = new int[] {5,5,5,5,5,5,5,3,3,3,3,3,3,5,5,5,5,5,5,5,5,5,5,5,3,3,3};
        combinations[3].special = new int[] {4,6,3,3,3};
        combinations[4] = new Milestone();
        combinations[4].combination = new int[] {3,3,3,3,3,3,3,3,5,5,5,6,6,6,6,6,6,6,6,6,5,5,5,6,6,6,6,6,6,6,6,6};
        combinations[4].special = new int[] {3,3,3,3,3,3,3,3,3,7,7,7,4};
        combinations[5] = new Milestone();
        combinations[5].combination = new int[] {6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,7,7,7,7,7,7,7,7,7,7,7,7,7};
        combinations[5].special = new int[] {9,9,9,9};
        combinations[6] = new Milestone();
        combinations[6].combination = new int[] {9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,7,7,7,7,7,7,7,7,7,7,7,7,7,7};
        combinations[6].special = new int[] {8,10,10};
        combinations[7] = new Milestone();
        combinations[7].combination = new int[] {8,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,10,10,10,10,10,10,10,10,10,10,10};
        combinations[7].special = new int[] {10,10,10,10,10,11,11};
        combinations[8] = new Milestone();
        combinations[8].combination = new int[] {11,11,11,11,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10};
        combinations[8].special = new int[] {8,10,8,10,8,11,8,11,11,11,11,11,10,10,10,10,13,13};
        combinations[9].combination = new int[] {10,13,10,13,10,13,10,13,10,13,13,13,13,13,10,13,10,13,13,13,13,13,13,11,11,11,11};
        combinations[9].special = new int[] {14,14,14,14,14,14,14,14,14};
        combinations[10] = new Milestone();
        combinations[10].combination = new int[] {13,13,13,13,13,13,13,13,13,13,13,13,13,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14};
        combinations[10].special = new int[] {15,15,15,15,14,14,14,14,14,14,15};
        combinations[11] = new Milestone();
        combinations[11].combination = new int[] {14,14,14,15,15,15,14,14,14,14,14,15,15,15,15,15,15,15,15,15,14,14,14,13,13,13,14,15,15,14,14,14};
        combinations[11].special = new int[] {12,15,15,15,15,15,15,15,15,15,15,17,17,17};
        combinations[12] = new Milestone();
        combinations[12].combination = new int[] {12,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17};
        combinations[12].special = new int[] {17,17,17,15,15,12,15,18,12,18};
        combinations[13] = new Milestone();
        combinations[13].combination = new int[] {12,17,17,17,17,17,17,17,17,17,17,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,17,17,12};
        combinations[13].special = new int[] {16,19,19,19,19,19,19,19,19,19,19,19};
        combinations[14] = new Milestone();
        combinations[14].combination = new int[] { 17, 17, 17, 17, 17, 17, 17, 17 };
        combinations[14].special = new int[] { 18, 18, 18, 18 };
        combinations[14] = new Milestone();
        combinations[14].combination = new int[] { 18, 18, 18, 18, 18, 18, 18, 18 };
        combinations[14].special = new int[] { 19, 18, 18, 18 };
    }

    private void Start()
    {
        gainSecondChanceCounter = 0;
        secondChanceWaveCountTarget = 30;
        loadingAll = false;
        repetition = 0;
        StartCombinations();
        minimap = GameObject.Find("Minimap").GetComponent<Minimap>();
        waveSpawnerUIs = new Dictionary<string, GameObject>();
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

        waveCounterUI = hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveProgress").gameObject;
        gridMouse = GridMouse.instance;
        buildManager = BuildManager.instance;
        shop = Shop.instance;
        listOfStates = new List<PropertyScript.StructureState>();
        waveSpawnerUIHolder = hud.transform.Find("WaveSpawnerUI").gameObject;

        instantiateWaveSpawnerUI("Lane1", spawnLocationLane1);
        instantiateWaveSpawnerUI("Lane2A", spawnLocationLane2A);
        instantiateWaveSpawnerUI("Lane2B", spawnLocationLane2B);
        instantiateWaveSpawnerUI("Lane3A", spawnLocationLane3A);
        instantiateWaveSpawnerUI("Lane3B", spawnLocationLane3B);
        instantiateWaveSpawnerUI("Lane4", spawnLocationLane4);

        timesMonsterAppearedById = new int[4,50];
        for (int k = 0; k < timesMonsterAppearedById.GetLength(0); k++)
        {
            for (int l = 0; l < timesMonsterAppearedById.GetLength(1); l++)
            {
                timesMonsterAppearedById[k, l] = 0;
            }
        }
    }

    public void instantiateWaveSpawnerUI(string spawnUILaneKey, Transform spawnTransform) {
        GameObject spawner = Instantiate(WaveSpawnerUIPrefab, waveSpawnerUIHolder.transform);
        spawner.GetComponentInChildren<WaveSpawnerUIController>().mapLocation = spawnTransform.position;
        waveSpawnerUIs.Add(spawnUILaneKey, spawner);
        spawner.GetComponent<WaveSpawnerUIController>().hideUI();
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
        if( waveNumberLane1 == kingWaveLane1)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane1.position, Quaternion.identity);
            monster.GetComponent<PawnController>().SetupWaypoints(1,0);

            
            monsterBatch.Add(monster);
            monster.transform.parent = transform;
            
            minimap.UpdateMonsterBatch();
            kingWaveLane1 = -1;
        }

        bool[] alreadyBoostedMonsterInThisWaveById = new bool[timesMonsterAppearedById.Length];
        for (int i = 0; i < alreadyBoostedMonsterInThisWaveById.Length; i++) { alreadyBoostedMonsterInThisWaveById[i] = false; }
        while (spawningMonsterLane1 < combination_.Length)
        {

            if(timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane1] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane1.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(1,0);
                BoostMonsterHP(ref monster, ref alreadyBoostedMonsterInThisWaveById, 0);

                monsterBatch.Add(monster);
                minimap.UpdateMonsterBatch();

                monster.transform.parent = transform;
                spawningMonsterLane1++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                waveCounterUI.GetComponent<Image>().fillAmount = waveProgression;

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
        if (waveNumberLane2  == kingWaveLane2)
        {
            spawnLocationLane2 = Mathf.Round(Random.Range(0, 1)) > 0 ? spawnLocationLane2A : spawnLocationLane2B;
            int waypoint = spawnLocationLane2 == spawnLocationLane2A ? 0 : 1;
            GameObject monster = Instantiate(King1, spawnLocationLane2A.position, Quaternion.Euler(new Vector3(0, 90, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(2,waypoint);
            
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
            kingWaveLane2 = -1;
        }

        bool[] alreadyBoostedMonsterInThisWaveById = new bool[timesMonsterAppearedById.Length];
        for (int i = 0; i < alreadyBoostedMonsterInThisWaveById.Length; i++) { alreadyBoostedMonsterInThisWaveById[i] = false; }
        while (spawningMonsterLane2 < combination_.Length)
        {

            if (timer <= 0)
            {
                //int randomLane = Random.Range(0, 2);
                //if(randomLane > 0)
                spawnLocationLane2 = Random.Range(0, 2) > 0 ? spawnLocationLane2A : spawnLocationLane2B;
                int waypoint = spawnLocationLane2 == spawnLocationLane2A ? 0 : 1;

                int monsterIndex = combination_[spawningMonsterLane2] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane2.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                monster.GetComponent<PawnController>().SetupWaypoints(2,waypoint);
                BoostMonsterHP(ref monster, ref alreadyBoostedMonsterInThisWaveById, 1);

                monsterBatch.Add(monster);
                minimap.UpdateMonsterBatch();

                monster.transform.parent = transform;
                spawningMonsterLane2++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                waveCounterUI.GetComponent<Image>().fillAmount = waveProgression;

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
        if (waveNumberLane3  == kingWaveLane3)
        {
            spawnLocationLane3 = Mathf.Round(Random.Range(0, 1)) > 0 ? spawnLocationLane3A : spawnLocationLane3B;
            int waypoint = spawnLocationLane3 == spawnLocationLane3A ? 0 : 1;
            GameObject monster = Instantiate(King1, spawnLocationLane3.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(3,waypoint);
            
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
            kingWaveLane3 = -1;
        }

        bool[] alreadyBoostedMonsterInThisWaveById = new bool[timesMonsterAppearedById.Length];
        for (int i = 0; i < alreadyBoostedMonsterInThisWaveById.Length; i++) { alreadyBoostedMonsterInThisWaveById[i] = false; }
        while (spawningMonsterLane3 < combination_.Length)
        {

            if (timer <= 0)
            {
                spawnLocationLane3 = Random.Range(0, 2) > 0 ? spawnLocationLane3A : spawnLocationLane3B;
                int waypoint = spawnLocationLane3 == spawnLocationLane3A ? 0 : 1;
                int monsterIndex = combination_[spawningMonsterLane3] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane3.position, Quaternion.Euler(new Vector3(0, -90, 0)));
                monster.GetComponent<PawnController>().SetupWaypoints(3,waypoint);
                BoostMonsterHP(ref monster, ref alreadyBoostedMonsterInThisWaveById, 2);

                monsterBatch.Add(monster);
                minimap.UpdateMonsterBatch();

                monster.transform.parent = transform;
                spawningMonsterLane3++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                waveCounterUI.GetComponent<Image>().fillAmount = waveProgression;

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


    public void BoostMonsterHP(ref GameObject monster, ref bool[] alreadyBoostedMonsterInThisWaveById, int lane)
    {
        
        PawnController pc = monster.GetComponent<PawnController>();
        int id = (int) pc.monster_id;

        
        if (alreadyBoostedMonsterInThisWaveById[id] == false)
        {
            timesMonsterAppearedById[lane, id]++;
        }
        int increase_value = timesMonsterAppearedById[lane, id] * 4;
        
        //Boost monster HP.
        monster.GetComponent<PawnCharacter>().maxHealth += increase_value;
        monster.GetComponent<PawnCharacter>().health += increase_value;
        
        
        alreadyBoostedMonsterInThisWaveById[id] = true;
    }

    IEnumerator SpawnLane4(int[] combination_)
    {
        float timer = 0;

        //spawns king
        if (waveNumberLane4 == kingWaveLane4)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane4.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(4,0);
            
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
            kingWaveLane4 = -1;
        }

        bool[] alreadyBoostedMonsterInThisWaveById = new bool[timesMonsterAppearedById.Length];
        for (int i = 0; i < alreadyBoostedMonsterInThisWaveById.Length; i++) { alreadyBoostedMonsterInThisWaveById[i] = false; }
        while (spawningMonsterLane4 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane4] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane4.position, Quaternion.Euler(new Vector3(0,180,0)));
                monster.GetComponent<PawnController>().SetupWaypoints(4,0);
                BoostMonsterHP(ref monster, ref alreadyBoostedMonsterInThisWaveById, 3);

                

                monsterBatch.Add(monster);
                minimap.UpdateMonsterBatch();

                monster.transform.parent = transform;
                spawningMonsterLane4++;
                timer = spawnTimer;

                //wave progression
                waveProgression = GetTotalSpawningMonsters() / (float)waveMonsters;
                waveCounterUI.GetComponent<Image>().fillAmount = waveProgression;
                //hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();

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
        if (GameController.gameState == GameState.Preparation)
        {
            waveCounterUI.GetComponent<Image>().fillAmount = Mathf.Lerp(waveCounterUI.GetComponent<Image>().fillAmount, 0f, 0.2f);
            if (!isWaving)
            {
                //here we randomly start the next king wave.
                if (!isStartingKing)
                {
                    int kingWave = Random.Range(0, 1);
                    if(kingWave > 0)
                    {
                        int kinglane = Random.Range(1, 4);
                        isStartingKing = true;
                        
                    }
                }
                if (!isStartingBomber)
                {
                    int bomberWave = Random.Range(0, 1);

                    if (bomberWave >0)
                    {
                        int bomberLane = Random.Range(0, 1);
                        isStartingBomber = true;
                    }
                }


                CreateWave();
                gainSecondChanceCounter++;
            }
        }
        else if (GameController.gameState == GameState.BeginWave)
        {
            if (isWaving)
            {
                saved_money = PlayerStats.Money;
               // Debug.Log("SAVED " + saved_money + " MONEY !");
                StopAllCoroutines();
                GameController.ChangeGameState(GameState.Waving);
                doSaveAll();
            }
        }


        else if(GameController.gameState == GameState.Waving)
        {

            if (!startedSpawn) {
                
                if (maxLanes == 1)
                {
                    StartCoroutine("SpawnLane1", combinationLane1);

                }
                else if (maxLanes == 2)
                {
                    StartCoroutine("SpawnLane1", combinationLane1);

                    StartCoroutine("SpawnLane2", combinationLane2);

                }
                else if (maxLanes == 3)
                {
                    StartCoroutine("SpawnLane1", combinationLane1);

                    StartCoroutine("SpawnLane2", combinationLane2);

                    StartCoroutine("SpawnLane3", combinationLane3);
                }
                else
                {
                    StartCoroutine("SpawnLane1", combinationLane1);

                    StartCoroutine("SpawnLane2", combinationLane2);

                    StartCoroutine("SpawnLane3", combinationLane3);

                    StartCoroutine("SpawnLane4", combinationLane4);
                }
            }
            startedSpawn = true;
        }
        else if(GameController.gameState == GameState.Action)
        {
            //hud.transform.Find("Wave").transform.Find("Progress").GetComponent<Text>().text = "100%";
            if(transform.childCount == 0)
                GameController.ChangeGameState(GameState.EndWave);
        }
        else if(GameController.gameState == GameState.GameOver)
        {
            StopAllCoroutines();
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

    public void RetryWave()
    {
        startedSpawn = false;
        finishedSpawns = 0;
        isWaving = false;

        spawningMonsterLane1 = 0;
        spawningMonsterLane2 = 0;
        spawningMonsterLane3 = 0;
        spawningMonsterLane4 = 0;

        waveNumber = Mathf.Clamp(waveNumber - 1, 0, waveNumber);
        waveNumberLane1 = Mathf.Clamp(waveNumberLane1 - 1, 0, waveNumberLane1);
        waveNumberLane2 = Mathf.Clamp(waveNumberLane2 - 1, 0, waveNumberLane2);
        waveNumberLane3 = Mathf.Clamp(waveNumberLane3 - 1, 0, waveNumberLane3);
        waveNumberLane4 = Mathf.Clamp(waveNumberLane4 - 1, 0, waveNumberLane4);

        if (waveNumberLane1 % (int)interval == 0)
        {
            currentMS1--;
        }
        if (waveNumberLane2 % (int)interval == 0)
        {
            currentMS2--;
        }
        if (waveNumberLane3 % (int)interval == 0)
        {
            currentMS3--;
        }
        if (waveNumberLane4 % (int)interval == 0)
        {
            currentMS4--;
        }

       
        GameController.ChangeGameState(GameState.Preparation);
    }

    //waves set up
    void CreateWave()
    {
        //reseting monsters indexes;

        Debug.Log("CREATING WAVE...");
        startedSpawn = false;
        finishedSpawns = 0;
        spawningMonsterLane1 = 0;
        spawningMonsterLane2 = 0;
        spawningMonsterLane3 = 0;
        spawningMonsterLane4 = 0;

        waveNumber++;

        Debug.Log("Gold: "+ PlayerStats.totalMoneyAccumulated + " at wave " + waveNumber);

        if (waveNumber < 10)
            maxLanes = 1;
        else if (waveNumber >= 10 && waveNumber < 20)
        {
            openWave(1);
            maxLanes = 2;
            GameController.UnlockLane(2);
        }

        else if (waveNumber >= 20 && waveNumber < 30)
        {
            openWave(2);
            maxLanes = 3;
            GameController.UnlockLane(4);
        }
        else {
            openWave(3);
            maxLanes = 4;
            GameController.UnlockLane(3);
        }

        waveMonsters = 0;

        if(maxLanes == 1)
        {
            currentMS1 = Mathf.Clamp(currentMS1, 0, (totalCombinations - 1));

            waveLane1 = new Wave(waveNumberLane1 / interval, currentMS1, 10);

            waveNumberLane1++;
            

            if (waveNumberLane1 % (int) interval == 0)
                currentMS1 = Mathf.Clamp((currentMS1+1) , 0,(totalCombinations-1));

            //waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            if (isStartingBomber && bomberWaveLane1!= -1)
            {
                combinationLane1 = new int[waveNumberLane1 * 2];
                int[] wC = waveLane1.GetCombinaton();
                int[] wB = waveLane1.BomberWave(waveNumberLane1);
                for (int i = 0; i < wC.Length; i++)
                {
                    combinationLane1[i] = wC[i];
                }

                for (int i = 0; i < wB.Length; i++)
                {
                    combinationLane1[i] = wB[i];
                }

                bomberWaveLane1 = -1;

            }
            else
            {
                combinationLane1 = waveLane1.GetCombinaton();
            }
            
            waveMonsters += combinationLane1.Length;
            if (waveSpawnerUIs["Lane1"])
            {
                waveSpawnerUIs["Lane1"].gameObject.SetActive(true);
                waveSpawnerUIs["Lane1"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
            }
        }
        else if(maxLanes == 2)
        {

            currentMS1 = Mathf.Clamp(currentMS1, 0, (totalCombinations - 1));
            currentMS2 = Mathf.Clamp(currentMS2, 0, (totalCombinations - 1));

            if (waveNumberLane1 % (int)interval == 0)
            {
                currentMS1 = Mathf.Clamp((currentMS1 + 1), 0, (totalCombinations -1)); 

            }

            waveLane1 = new Wave(waveNumberLane1 % (int)interval / interval, currentMS1, 10);

            waveLane2 = new Wave(waveNumberLane2 % (int)interval / interval, currentMS2, 10);

            waveNumberLane1++;
            waveNumberLane2++;


            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length;
            if (waveSpawnerUIs["Lane2A"])
                waveSpawnerUIs["Lane2A"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
            if (waveSpawnerUIs["Lane2B"])
                waveSpawnerUIs["Lane2B"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
        }
        else if(maxLanes == 3)
        {
            currentMS1 = Mathf.Clamp(currentMS1, 0, (totalCombinations - 1));
            currentMS2 = Mathf.Clamp(currentMS2, 0, (totalCombinations - 1));
            currentMS3 = Mathf.Clamp(currentMS3, 0, (totalCombinations - 1));

            if (waveNumberLane1 % (int)interval == 0)
            {
                currentMS1 = Mathf.Clamp((currentMS1 + 1), 0, (totalCombinations -1));
            }
            if (waveNumberLane2 % (int)interval == 0)
            {
                currentMS2 = Mathf.Clamp((currentMS2 + 1), 0, (totalCombinations-1));
            }

            waveLane1 = new Wave(waveNumberLane1 % (int)interval / interval, currentMS1, 10);
            waveLane2 = new Wave(waveNumberLane2 % (int)interval / interval, currentMS2, 10);
            waveLane3 = new Wave(waveNumberLane3 % (int)interval / interval, currentMS3, 10);

            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;



            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length;
            if (waveSpawnerUIs["Lane3A"])
                waveSpawnerUIs["Lane3A"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
            if (waveSpawnerUIs["Lane3B"])
                waveSpawnerUIs["Lane3B"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
        }
        else if( maxLanes == 4)
        {
            
            currentMS1 = Mathf.Clamp(currentMS1, 0, (totalCombinations - 1));
            currentMS2 = Mathf.Clamp(currentMS2, 0, (totalCombinations - 1));
            currentMS3 = Mathf.Clamp(currentMS3, 0, (totalCombinations - 1));
            currentMS4 = Mathf.Clamp(currentMS4, 0, (totalCombinations - 1));
            
           

            if (waveNumberLane1 % (int)interval == 0)
            {

                currentMS1 = Mathf.Clamp((currentMS1 + 1), 0, (totalCombinations - 1));
            }
            if (waveNumberLane2 % (int)interval == 0)
            {
                
                currentMS2 = Mathf.Clamp((currentMS2 + 1), 0, (totalCombinations-1));
            }
            if (waveNumberLane3 % (int)interval == 0)
            {
                
                currentMS3 = Mathf.Clamp((currentMS3 + 1), 0, (totalCombinations-1));
            }


            waveLane4 = new Wave(waveNumberLane4 % (int)interval / interval, currentMS4, 10);
            waveNumberLane4++;

            if (waveNumberLane4 % (int)interval == 0)
            {
                
                currentMS4 = Mathf.Clamp((currentMS4 + 1), 0, (totalCombinations-1));
            }

            waveLane1 = new Wave(waveNumberLane1 % (int)interval / interval, currentMS1, 10);
            waveLane2 = new Wave(waveNumberLane2 % (int)interval / interval, currentMS2, 10);
            waveLane3 = new Wave(waveNumberLane3 % (int)interval / interval, currentMS3, 10);
          

            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;
            

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();
            combinationLane4 = waveLane4.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length + combinationLane4.Length;
            if (waveSpawnerUIs["Lane4"])
                waveSpawnerUIs["Lane4"].gameObject.GetComponent<WaveSpawnerUIController>().showUI();
            
        }
        
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterText").GetComponent<Text>().text = "" + (waveNumber);
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterTextShadow").GetComponent<Text>().text = "" + (waveNumber);
        PlayerStats.MineCapacity = (int)(waveNumber / 5);
        //FillMonstersType();
        monsterBatch.Clear();
        minimap.ClearMonsterBatch();
        isWaving = true;
    }

    public void openWave(int waveNumber) {
        if (!openLanes[waveNumber])
        {
            openLanes[waveNumber] = true;

            if (waveWarningBoardController){
                switch (waveNumber)
                {
                    case 0:
                        break;
                    case 1:
                        waveWarningBoardController.setWarningText("Monsters are coming \n through the Snowlands!");
                        waveWarningBoardController.openWarningBoard();
                        break;
                    case 2:
                        waveWarningBoardController.setWarningText("Monsters are coming \n through the Volcanoes!");
                        waveWarningBoardController.openWarningBoard();
                break;
                case 3:
                        waveWarningBoardController.setWarningText("Monsters are coming \n through the Desert!");
                        waveWarningBoardController.openWarningBoard();
                        break;
                }
            }
        }
    }

    //void SpawnMonsters()
    //{
    //       if(timer <= 0)
    //       {
    //           int monsterIndex = combination[spawningMonster] - 1;
    //           GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocation.position, Quaternion.identity);
    //           monsterBatch.Add(monster);
    //           Minimap.UpdateMonsterBatch();

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
    public void doDestroyAll()
    {
        GameObject upgrade_wheel = GameObject.Find("UpgradeWheel");
        BuildableController bc;
        if (upgrade_wheel != null)
        {
            UpgradeWheelController uwc = upgrade_wheel.GetComponent<UpgradeWheelController>();
            uwc.gameObject.SetActive(false);
            uwc.isActive = false;
            uwc.clearButtons();
        }
        //Debug.Log("Gonna destroy all !");
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            bc = gridMouse.ListOfGameObjects[i].GetComponent<BuildableController>();

            int x = bc.getUnitBlueprint().stored_x;
            int z = bc.getUnitBlueprint().stored_z;


            
            
            //Debug.Log("JUST DESTROYED THESE COORDINATES: "+x+", "+z);
            //if it's a mining camp
            if (bc.getUnitBlueprint().name == Shop.instance.miningCamp.name)
            {
                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property("Normal");
                gridMouse.propertiesMatrix[x+1, z+1] = new PropertyScript.Property("Normal");
                gridMouse.propertiesMatrix[x+1, z] = new PropertyScript.Property("Normal");
                gridMouse.propertiesMatrix[x, z+1] = new PropertyScript.Property("Normal");
                PlayerStats.MinesConstructed--;

                gridMouse.previewMatrix[x, z] = false;
                gridMouse.previewMatrix[x+1, z+1] = false;
                gridMouse.previewMatrix[x+1, z] = false;
                gridMouse.previewMatrix[x, z+1] = false;
            }
            else //if it's not
            {
                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property("Normal");
                gridMouse.previewMatrix[x, z] = false;
            }
            Destroy(gridMouse.ListOfGameObjects[i]);
        }
        gridMouse.ListOfGameObjects.Clear();
        //Debug.Log("SIZE: " + gridMouse.ListOfGameObjects.Count);
        //---------------------------------------------------------
        //destroy all monsters
        for (int i = 0; i < monsterBatch.Count; i++)
        {
            Destroy(monsterBatch[i]);
        }
        monsterBatch.Clear();

        //destroy gold buttons
        foreach (Canvas c in GameObject.FindObjectsOfType<Canvas>())
        {
            if (c.CompareTag("HUD"))
            {
                hud_canvas = c;
                break;
            }
        }
        if (hud_canvas)
        {
            Transform goldMinesButtonHolder = hud_canvas.transform.Find("GoldMinesButtonHolder");
            foreach (Transform child in goldMinesButtonHolder)
            {
                Destroy(child.gameObject);
            }
        }

    }
    public void doSaveAll()
    {
        //Debug.Log("Gonna save all !");
        listOfStates = new List<PropertyScript.StructureState>();
        BuildableController bc;
        TowerController tc;
        TowerSlowController tSc;
        TeslaCoilController tTc;
        if (PlayerStats.DebugModeON) { FileOperations.openStreamWriter(); }
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            if (gridMouse.ListOfGameObjects[i] == null)
            {
                //Debug.Log("(doSaveAll) IT'S NULL !");
            }
            //Debug.Log("(doSaveAll()) COUNT = " + gridMouse.ListOfGameObjects.Count);


            //Get information from the Buildable
            bc = gridMouse.ListOfGameObjects[i].GetComponent<BuildableController>();
            //--------------------------------------------------------------------------
            //Get information from the TowerController
            tc = gridMouse.ListOfGameObjects[i].GetComponent<TowerController>();
            //--------------------------------------------------------------------------
            //Get information from the TowerSlowController
            tSc = gridMouse.ListOfGameObjects[i].GetComponent<TowerSlowController>();
            //--------------------------------------------------------------------------
            //Get information from the TowerSlowController
            tTc = gridMouse.ListOfGameObjects[i].GetComponent<TeslaCoilController>();


            PropertyScript.StructureState state;
            if (tc != null) // If it has a TowerController (AKA: is a tower).
            {
                state =
                    new PropertyScript.StructureState(state.structureName = tc.getUnitBlueprint().name,
                                                      gridMouse.ListOfGameObjects[i].transform,
                                                        bc.Health, tc.fireRateLVL, tc.attackPowerLVL);

                //Debug.Log("Just Saved FR, AP = " + tc.fireRateLVL + ", " + tc.attackPowerLVL);
                listOfStates.Add(state);
                if (PlayerStats.DebugModeON) { FileOperations.writeState(state); }
            }
            else if (tSc != null)
            {
                state =
                    new PropertyScript.StructureState(state.structureName = tSc.getUnitBlueprint().name,
                                                      gridMouse.ListOfGameObjects[i].transform,
                                                        bc.Health, tSc.fireRateLVL, tSc.attackPowerLVL);
                listOfStates.Add(state);
                if (PlayerStats.DebugModeON) { FileOperations.writeState(state); }
            }
            else if (tTc != null)
            {
                state =
                    new PropertyScript.StructureState(state.structureName = tTc.getUnitBlueprint().name,
                                                      gridMouse.ListOfGameObjects[i].transform,
                                                        bc.Health, tTc.fireRateLVL, tTc.attackPowerLVL);
                listOfStates.Add(state);
                if (PlayerStats.DebugModeON) { FileOperations.writeState(state); }
            }
            else if (bc != null) // Soldier Camp
            {
                state =
                    new PropertyScript.StructureState(state.structureName = bc.getUnitBlueprint().name,
                                                      gridMouse.ListOfGameObjects[i].transform);
                //Debug.Log("ADDED " + bc.getUnitBlueprint().name + " TO THE LIST OF STATES");
                listOfStates.Add(state);
                if (PlayerStats.DebugModeON) { FileOperations.writeState(state); }
            }
            
           // Debug.Log("Added " + gridMouse.ListOfGameObjects[i].transform.position + ".");
        }
        if (PlayerStats.DebugModeON) { FileOperations.closeStreamWriter(); }
    }
    public void doLoadAll()
    {
        if (PlayerStats.DebugModeON)
        {
            listOfStates = FileOperations.loadState();
            FileOperations.closeReader();
        }
        //Debug.Log("CALLING DESTROY ALL");
        doDestroyAll();

        //Debug.Log("Gonna load all " + listOfStates.Count + " !");
        for (int i = 0; i < listOfStates.Count; i++)
        {
            if (listOfStates[i].structureName == Shop.instance.towerLevel1.name)
            {
                shop.SelectStandardUnit(true);
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, true);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.previewMatrix[x, z] = true;

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
               // Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == Shop.instance.towerLevel2.name)
            {
                shop.SelectTower2Unit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, true);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.previewMatrix[x, z] = true;

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
               // Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == Shop.instance.towerSlow.name)
            {
                shop.SelectIceTowerUnit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, true);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.previewMatrix[x, z] = true;

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerSlowController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
               // Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == Shop.instance.towerTesla.name)
            {
                shop.SelectFireTowerUnit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, true);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.previewMatrix[x, z] = true;

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TeslaCoilController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
               // Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == Shop.instance.towerLevel3.name)
            {
                shop.SelectTower3Unit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, true);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.previewMatrix[x, z] = true;

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
                //Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == Shop.instance.miningCamp.name)
            {
                shop.SelectSecondaryUnit(true);
                Vector3 newPosition = new Vector3(listOfStates[i].position.x - 0.5f, listOfStates[i].position.y, listOfStates[i].position.z - 0.5f);
                int added_index = gridMouse.buildUnitAndAddItToTheList(newPosition, listOfStates[i].rotation,true);
                Vector2 gridSize = gridMouse.getGridSize();

                //int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                //int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                int x = Mathf.FloorToInt(newPosition.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(newPosition.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                gridMouse.previewMatrix[x, z] = true;
                gridMouse.previewMatrix[x + 1, z + 1] = true;
                gridMouse.previewMatrix[x + 1, z] = true;
                gridMouse.previewMatrix[x, z + 1] = true;

               // Debug.Log("LOADED " + listOfStates[i].position + ".");
            }
            else
            {
               // Debug.Log("DID NOT LOAD");
            }

        }
        //this line is REALLY important; the unitToBuild must be deselected or it can
        //happen that you are able to build a unit without money in the retried wave.
        buildManager.DeselectUnitToBuild();
        
        //load previously saved money
        PlayerStats.SetMoney(saved_money);

        //Restart wave
        GameController gc = GameObject.Find("GameMode").GetComponent<GameController>();
        gc.game_over = false;
        RetryWave();
        gainSecondChanceCounter = 0;

        CastleHealth.castleDestructionAnimator.enabled = false;

        GameObject castleObject = GameObject.FindWithTag("Castle");
        Destroy(castleObject);

        GameObject gameMap = GameObject.Find("Game Map");
        //GameObject newCastle = Instantiate(castlePrefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0f, 0f, -180f)));
        GameObject newCastle = Instantiate(castlePrefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0f, 0f, -180f)));
        //newCastle.transform.localScale = castleObject.transform.localScale;
        newCastle.transform.parent = gameMap.transform;
        newCastle.transform.localScale = new Vector3(1f, 1f, 1f);
        newCastle.transform.RotateAround(Vector3.zero, Vector3.right, -90f);


        CastleHealth castleHealth = newCastle.GetComponent<CastleHealth>();
        castleHealth.enabled = true;

        

        castleHealth.health = 5000;
        castleHealth.UpdateHealthBarGfx(5000);

        //clear minimap
        Minimap minimap = GameObject.Find("Minimap").GetComponent<Minimap>();
        minimap.ClearMonsterBatch();


        //DESTROY HEALTH BARS
        GameObject healthBarsObject = GameObject.Find("HealthBars");
        foreach (Transform child in healthBarsObject.transform)
        {
            Destroy(child.gameObject);
        }


        


    }


    



}

