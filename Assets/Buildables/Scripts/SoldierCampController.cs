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
	public Transform spawnPoint;


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





	// Use this for initialization
	void Start () {
		Health = 50;
		IsUpgradable = true;
		Defense = 2;
		soldiersController = new List<SimpleSoldierController>();
		
	}
	
	// Update is called once per frame
	void Update () {

        int soldiers = transform.childCount;
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
        Vector3 spawnLocation = spawnPoint.position;
        GameObject soldier = Instantiate(simpleSoldierPrefab, spawnLocation, spawnPoint.rotation);
        soldier.transform.parent = gameObject.transform;
        SimpleSoldierController ssc = (SimpleSoldierController)soldier.GetComponent<SimpleSoldierController>();
       
        soldiersController.Add(ssc);
        ssc.SetTarget(enemies[0]);
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
                if (ssc.target == null || ssc.target.GetComponent<PawnCharacter>().isDead)
                {
                    ssc.target = enemies[0];
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