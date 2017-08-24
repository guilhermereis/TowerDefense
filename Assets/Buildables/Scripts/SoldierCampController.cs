using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCampController : BuildableController {

	[HideInInspector]
    public delegate void EnemyOutOfReachDelegate(GameObject target);
    delegate void SetEnemyDelegate(GameObject target);

	[HideInInspector]
	public List<GameObject> enemies;
	[HideInInspector]
	public List<SimpleSoldierController> soldiersController;

	public GameObject simpleSoldierPrefab;

	[HideInInspector]
	private EnemyOutOfReachDelegate enemyOutOfReach;
	SetEnemyDelegate setEnemy;

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
		float distance = 1.5f;
		Vector3 spawnLocation = spawnPoint.position;
		for (int i = 0; i < soldiersCount; i++)
		{
			spawnLocation += spawnPoint.transform.forward * distance;

			GameObject soldier = Instantiate(simpleSoldierPrefab, spawnLocation, spawnPoint.rotation);
			soldier.transform.parent = gameObject.transform;


			SimpleSoldierController ssc = (SimpleSoldierController)soldier.GetComponent<SimpleSoldierController>();
			//binding enemyOutOfReachDelegate
			soldiersController.Add(ssc);

		}
		soldiersCount = soldiersController.Count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateSoldiers()
	{
		//Instantiate<SimpleSoldierController>(simpleSoldier,transform.po)
		//soldiersController
	}


    //when an enemy get in range;
    private void OnTriggerEnter(Collider other)
    {
		if (!other.isTrigger)
		{
			if(other.gameObject.tag == "Enemy" && !other.isTrigger)
			{
				enemies.Add(other.gameObject);
				//bug.Log(enemies.LastIndexOf(other.gameObject));
				if (enemies.Count > 1 && nextEnemy < soldiersCount -1)
				{
					soldiersController[nextEnemy].SetTarget(other.gameObject);
					nextEnemy++;
				
				}
				else
				{
					SetSoldierTarget(other.gameObject);
				}
			}
		}

    }

	public void UpdateEnemies(GameObject _target)
	{
		if (enemies.Remove(_target))
		{
			_target.GetComponent<PawnCharacter>().OnDying();
			if (enemies.Count > 0)
				SetSoldierTarget(enemies[0]);
		}
	}

    private void OnTriggerExit(Collider other)
    {
		//todo delegate to who has this target stop to attack

		if (!other.isTrigger)
		{
			if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<PawnCharacter>().isDying)
			{
				
				enemies.Remove(other.gameObject);
				if (enemies.Count > 0)
				{
					for (int i = 0; i < soldiersCount; i++)
					{
						if(soldiersController[i].target == other.gameObject)
						{
							soldiersController[i].target = null;
							Debug.Log(other.gameObject.name);
							//nextEnemy++;
							//break;
						}
					}
					SetSoldierTarget(enemies[0]);
				}
				else
					EnemyOutOfReach(other.gameObject);
			}
			else if(other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<PawnCharacter>().isDying)
			{
				Debug.Log("i am dying");
			}
			else if(other.gameObject.tag == "Enemy")
			{
				Debug.Log("nothing is happening");
			}
		}


    }

    private void SetSoldierTarget(GameObject _target)
    {
        for (int i = 0; i < soldiersController.Count; i++)
        {
            if(soldiersController[i].target == null)
            {
                soldiersController[i].SetTarget(_target);
                //Debug.Log(_target.name);
               
            }
        }
    }
}
/*
 * entra o primeiro, pega todos e setam para o primeiro
 * entrar o segundo, pega 1 dos soldados e seta para o segundo e assim por diante
 * 2 mataram 1, ai redistribuir.
*/