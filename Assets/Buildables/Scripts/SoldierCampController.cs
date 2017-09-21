using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCampController : BuildableController {

	[HideInInspector]
    public delegate void EnemyOutOfReachDelegate(GameObject target);
    delegate void SetEnemyDelegate(GameObject target);

	//[HideInInspector]
	public List<GameObject> enemies;
	[HideInInspector]
	public List<SimpleSoldierController> soldiersController;

	public GameObject simpleSoldierPrefab;

	[HideInInspector]
	private EnemyOutOfReachDelegate enemyOutOfReach;
	SetEnemyDelegate setEnemy;



    public float spawnCountdown = 0;
    //spawns soldiers each 2 seconds. 
    public float spawnRate = 0.5f;
	public Transform[] spawnPoints;


	//default number of spawned soldiers
    public int soldiersCount = 3;

	private int nextEnemy = 0;

	public EnemyOutOfReachDelegate EnemyOutOfReach
	{
		get
		{
			return enemyOutOfReach;
		}

		set
		{
			enemyOutOfReach = value;
		}
	}

    public int soldiersSpawned = 0;



	// Use this for initialization
	void Start () {
		Health = 50;
		IsUpgradable = true;
		Defense = 2;
		soldiersController = new List<SimpleSoldierController>();
        //spawnPoints = new Transform[3];
	}
	
	// Update is called once per frame
	void Update () {

        
        //spawns soldiers if there are enemies and is not above the soldiers limit
        if(spawnCountdown <=0 && (enemies.Count > 0) && soldiersController.Count < soldiersCount )
        {
            InstantiateSoldiers();
            spawnCountdown = 1 / spawnRate;
        }


        spawnCountdown -= Time.deltaTime;
	}

	public void InstantiateSoldiers()
	{

        //soldiersController
        Vector3 spawnLocation = spawnPoints[soldiersSpawned].position;
        GameObject soldier = Instantiate(simpleSoldierPrefab, spawnLocation, spawnPoints[soldiersSpawned].rotation);
        soldier.transform.parent = gameObject.transform;
        SimpleSoldierController ssc = (SimpleSoldierController)soldier.GetComponent<SimpleSoldierController>();

        soldiersController.Add(ssc);
        ssc.SetTarget(enemies[0]);

        ssc.deadSoldier += removeDeadSoldier;
        soldiersSpawned++;
    }


    public void removeDeadSoldier(SimpleSoldierController deadSoldier)
    {
        soldiersController.Remove(deadSoldier);
        soldiersSpawned--;
    }

    //when an enemy get in range;
    public override void OnTriggerEnter(Collider other)
    {
		if (other.GetType() == typeof(CapsuleCollider) && other.gameObject.CompareTag("Enemy"))
		{
			enemies.Add(other.gameObject);
            other.gameObject.GetComponent<PawnController>().deadPawn += RemoveDeadEnemy;
            UpdateEnemies();
		}

    }


    public void RemoveDeadEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
        UpdateEnemies();
    }

	public void UpdateEnemies()
	{
        if (enemies.Count > 0)
        {
		    foreach(SimpleSoldierController ssc in soldiersController)
            {

                if(ssc.target == null || ssc.target.GetComponent<PawnCharacter>().isDead)
                {
                   int enemyIndex = (int)Mathf.Round(Random.Range(0, enemies.Count - 1));
                   ssc.target = enemies[enemyIndex];
                   ssc.ChangeState(PawnController.PawnState.FindTarget);
                   
                }

            }

        }
	}

    public override void OnTriggerExit(Collider other)
    {
		//todo delegate to who has this target stop to attack

		if (other.GetType() == typeof(CapsuleCollider) && other.gameObject.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
			EnemyOutOfReach(other.gameObject);
            UpdateEnemies();
			
			
		}


    }

    
}
/*
 * entra o primeiro, pega todos e setam para o primeiro
 * entrar o segundo, pega 1 dos soldados e seta para o segundo e assim por diante
 * 2 mataram 1, ai redistribuir.
*/