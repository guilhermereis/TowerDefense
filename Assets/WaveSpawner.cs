using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    
    public List<GameObject> monsterBatch;
    public GameObject[] monstersPrefab;

    public int[] monstersType;

    public int totalMonsters = 3;

    private Wave currentWave;
    public int waveNumber = 1;
    public float waveProgression = 0;

    public Transform spawnLocation;
    bool isWaving = false;
   
    float spawnTimer = 2f;
    float timer = 0;
    public int[] combination;
    public int spawningMonster = 0;

    public Canvas hud;

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

    private void Update()
    {
        if(GameController.gameState == GameState.BeginWave)
        {
            if (!isWaving)
            {
                CreateWave();
            }
            else
            {              //Debug.Log(waveProgression);
                SpawnMonsters();
            }


        }
        else if(GameController.gameState == GameState.Action)
        {
            //hud.transform.Find("Wave").transform.Find("Progress").GetComponent<Text>().text = "100%";
            if(transform.childCount == 0)
                GameController.ChangeGameState(GameState.EndWave);
        }

        
        

    }

    public void FillMonstersType()
    {
        monstersType = new int[totalMonsters];
        for (int i = 0; i < combination.Length; i++)
        {
            monstersType[combination[i]-1]++;
           
        }
    }

    void CreateWave()
    {
        spawningMonster = 0;
        currentWave = new Wave(waveNumber * 2, waveNumber);
        combination = currentWave.GetCombinaton();
        waveNumber++;
        FillMonstersType();
        monsterBatch.Clear();
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

            //wave progression
            waveProgression = (float)spawningMonster / (float)combination.Length;
            hud.transform.Find("WaveUI").transform.Find("Progress").GetComponent<Text>().text = (waveProgression * 100.0f).ToString() + "%";// waveProgression.ToString();


        }
        if (spawningMonster >= combination.Length)
        {
            isWaving = false;
            GameController.ChangeGameState(GameState.Action);
        }

        timer -= Time.deltaTime;

    }
}
