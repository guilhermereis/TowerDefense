using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;
    public GameObject castlePrefab;
    private Minimap minimap;
    private int saved_money = 0;
    public int[] monstersType;

    public int totalMonsters = 6;

    int waveMonsters = 0;

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

    bool startedSpawn = false;
  
    int finishedSpawns = 0;
    public int maxLanes = 1;

    GridMouse gridMouse;
    BuildManager buildManager;
    List<PropertyScript.StructureState> listOfStates;
    Shop shop;

    private void Start()
    {
        minimap = GameObject.Find("Minimap").GetComponent<Minimap>();
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
            monster.GetComponent<PawnController>().SetupWaypoints(1,0);
            monsterBatch.Add(monster);
            monster.transform.parent = transform;
            minimap.UpdateMonsterBatch();
        }

        while (spawningMonsterLane1 < combination_.Length)
        {

            if(timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane1] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane1.position, Quaternion.identity);
                monster.GetComponent<PawnController>().SetupWaypoints(1,0);
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
        if (waveNumberLane2 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane2A.position, Quaternion.Euler(new Vector3(0, 90, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(2,0);
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
        }
        while (spawningMonsterLane2 < combination_.Length)
        {

            if (timer <= 0)
            {
                spawnLocationLane2 = Mathf.Round(Random.Range(0, 1)) > 0 ? spawnLocationLane2A : spawnLocationLane2B;
                int waypoint = spawnLocationLane2 == spawnLocationLane2A ? 0 : 1;

                int monsterIndex = combination_[spawningMonsterLane2] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane2.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                monster.GetComponent<PawnController>().SetupWaypoints(2,waypoint);
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
        if (waveNumberLane3 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane3.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(3,0);
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
        }
        while (spawningMonsterLane3 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane3] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane3.position, Quaternion.Euler(new Vector3(0, -90, 0)));
                monster.GetComponent<PawnController>().SetupWaypoints(3,0);
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

    IEnumerator SpawnLane4(int[] combination_)
    {
        float timer = 0;

        //spawns king
        if (waveNumberLane4 % 10 == 0)
        {
            GameObject monster = Instantiate(King1, spawnLocationLane4.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            monster.GetComponent<PawnController>().SetupWaypoints(4,0);
            monsterBatch.Add(monster);
            minimap.UpdateMonsterBatch();
            monster.transform.parent = transform;
        }

        while (spawningMonsterLane4 < combination_.Length)
        {

            if (timer <= 0)
            {
                int monsterIndex = combination_[spawningMonsterLane4] - 1;
                GameObject monster = Instantiate(monstersPrefab[monsterIndex], spawnLocationLane4.position, Quaternion.Euler(new Vector3(0,180,0)));
                monster.GetComponent<PawnController>().SetupWaypoints(4,0);
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
        if(GameController.gameState == GameState.BeginWave)
        {
            if (!isWaving)
            {
                CreateWave();
            }
            else
            {
                //Debug.Log(waveProgression);
                StopAllCoroutines();
                GameController.ChangeGameState(GameState.Waving);
                
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
        spawningMonsterLane1 = 0;
        spawningMonsterLane2 = 0;
        spawningMonsterLane3 = 0;
        spawningMonsterLane4 = 0;
        GameController.ChangeGameState(GameState.Waving);
    }

    //waves set up
    void CreateWave()
    {
        doSaveAll();
        Debug.Log("INITIATING WAVE... SAVED ALL OBJECTS");


        //reseting monsters indexes;
        startedSpawn = false;
        finishedSpawns = 0;
        spawningMonsterLane1 = 0;
        spawningMonsterLane2 = 0;
        spawningMonsterLane3 = 0;
        spawningMonsterLane4 = 0;

        waveNumber++;

        if(waveNumber < 10)
            maxLanes = 1;
        else if (waveNumber >= 10 && waveNumber < 20)
            maxLanes = 2;
        else if (waveNumber >= 20 && waveNumber < 30)
            maxLanes = 3;
        else
            maxLanes = 4;



        waveMonsters = 0;

        if(maxLanes == 1)
        {
            waveNumberLane1++;
            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            combinationLane1 = waveLane1.GetCombinaton();
            waveMonsters += combinationLane1.Length;
        }
        else if(maxLanes == 2)
        {
            waveNumberLane1++;
            waveNumberLane2++;

            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length;

        }
        else if(maxLanes == 3)
        {
            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;

            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);
            waveLane3 = new Wave(waveNumberLane3 * 2, waveNumberLane3);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length;

        }
        else if( maxLanes == 4)
        {
            waveNumberLane1++;
            waveNumberLane2++;
            waveNumberLane3++;
            waveNumberLane4++;

            waveLane1 = new Wave(waveNumberLane1 * 2, waveNumberLane1);
            waveLane2 = new Wave(waveNumberLane2 * 2, waveNumberLane2);
            waveLane3 = new Wave(waveNumberLane3 * 2, waveNumberLane3);
            waveLane4 = new Wave(waveNumberLane4 * 2, waveNumberLane4);

            combinationLane1 = waveLane1.GetCombinaton();
            combinationLane2 = waveLane2.GetCombinaton();
            combinationLane3 = waveLane3.GetCombinaton();
            combinationLane4 = waveLane4.GetCombinaton();

            waveMonsters += combinationLane1.Length + combinationLane2.Length + combinationLane3.Length + combinationLane4.Length;

        }
        
        
        
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterText").GetComponent<Text>().text = "" + (waveNumber);
        hud.transform.Find("Player Info").transform.Find("Wave Counter").transform.Find("WaveCounterTextShadow").GetComponent<Text>().text = "" + (waveNumber);
        //FillMonstersType();
        monsterBatch.Clear();
        minimap.ClearMonsterBatch();
        isWaving = true;
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
        if (upgrade_wheel != null)
        {
            UpgradeWheelController uwc = upgrade_wheel.GetComponent<UpgradeWheelController>();
            uwc.gameObject.SetActive(false);
            uwc.isActive = false;
            uwc.clearButtons();
        }
        Debug.Log("Gonna destroy all !");
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            Destroy(gridMouse.ListOfGameObjects[i]);
            //gridMouse.ListOfGameObjects.RemoveAt(i);
        }
        gridMouse.ListOfGameObjects.Clear();
        Debug.Log("SIZEEEE: " + gridMouse.ListOfGameObjects.Count);
        //---------------------------------------------------------
        //destroy all monsters
        for (int i = 0; i < monsterBatch.Count; i++)
        {
            Destroy(monsterBatch[i]);
        }
        monsterBatch.Clear();
    }
    public void doSaveAll()
    {
        Debug.Log("Gonna save all !");
        saved_money = PlayerStats.Money;

        listOfStates = new List<PropertyScript.StructureState>();
        BuildableController bc;
        TowerController tc;
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            if (gridMouse.ListOfGameObjects[i] == null)
            {
                Debug.Log("IIIIIIIIITS NULL !!!!");
            }
            Debug.Log("LALALALA COUNT = " + gridMouse.ListOfGameObjects.Count);


            //Get information from the Buildable
            bc = gridMouse.ListOfGameObjects[i].GetComponent<BuildableController>();
            //--------------------------------------------------------------------------
            //Get information from the TowerController
            tc = gridMouse.ListOfGameObjects[i].GetComponent<TowerController>();
            //--------------------------------------------------------------------------
            PropertyScript.StructureState state =
                new PropertyScript.StructureState(state.structureName = gridMouse.ListOfGameObjects[i].name,
                                                  gridMouse.ListOfGameObjects[i].transform,
                                                    bc.Health, tc.fireRateLVL, tc.attackPowerLVL);
            Debug.Log("Just Saved FR, AP = " + tc.fireRateLVL + ", " + tc.attackPowerLVL);
            listOfStates.Add(state);
            Debug.Log("Added " + gridMouse.ListOfGameObjects[i].transform.position + ".");
        }
    }
    public void doLoadAll()
    {
        Debug.Log("CALLING DESTROY ALL");
        doDestroyAll();

        Debug.Log("Gonna load all " + listOfStates.Count + " !");
        for (int i = 0; i < listOfStates.Count; i++)
        {
            if (listOfStates[i].structureName == "PrefabArcherTower1(Clone)")
            {
                shop.SelectStandardUnit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, false);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
                Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == "PrefabArcherTower2(Clone)")
            {
                shop.SelectTower2Unit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, false);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
                Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == "PrefabArcherTower3(Clone)")
            {
                shop.SelectTower3Unit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position, false);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL, listOfStates[i].attackPowerLVL);
                Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL + ", " + listOfStates[i].attackPowerLVL);
                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == "PrefabCamp(Clone)")
            {
                shop.SelectSecondaryUnit();
                Vector3 newPosition = new Vector3(listOfStates[i].position.x - 0.5f, listOfStates[i].position.y, listOfStates[i].position.z - 0.5f);
                int added_index = gridMouse.buildUnitAndAddItToTheList(newPosition, listOfStates[i].rotation);
                Vector2 gridSize = gridMouse.getGridSize();

                //int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                //int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                int x = Mathf.FloorToInt(newPosition.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(newPosition.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else
            {
                Debug.Log("DID NOT LOAD");
            }

        }

        //load previously saved money
        PlayerStats.Money = saved_money;

        //Restart wave
        GameController gc = GameObject.Find("GameMode").GetComponent<GameController>();
        gc.game_over = false;
        RetryWave();

        //Hide Try Again Window
        DebugOptions debugOptions = GameObject.Find("DebugOptions").GetComponent<DebugOptions>();
        debugOptions.Hide();

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

