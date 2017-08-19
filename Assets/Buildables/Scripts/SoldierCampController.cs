using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCampController : BuildableController {

    delegate void EnemyOutOfReachDelegate(GameObject target);
    delegate void SetEnemyDelegate(GameObject target);

    public List<GameObject> enemies;
    public List<SimpleSoldierController> soldiersController;

	public GameObject simpleSoldier;

	EnemyOutOfReachDelegate enemyOutOfReach;
    SetEnemyDelegate setEnemy;

	//default number of spawned soldiers
    private int soldiersCount = 3;

	private int nextEnemy = 0;
	

    // Use this for initialization
	void Start () {
		Health = 50;
		IsUpgradable = true;
		Defense = 2;
		soldiersController = new List<SimpleSoldierController>();
        for (int i = 0; i < transform.childCount; i++)
        {
            SimpleSoldierController ssc = (SimpleSoldierController)transform.GetChild(i).GetComponent<SimpleSoldierController>();
			//binding enemyOutOfReachDelegate
			enemyOutOfReach += ssc.ForgetTarget;
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
						if(soldiersController[nextEnemy].target == other.gameObject)
						{
							soldiersController[nextEnemy].target = null;
							Debug.Log(other.gameObject.name);
							break;
						}
					}
					SetSoldierTarget(enemies[0]);
				}
				else
					enemyOutOfReach(other.gameObject);
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