using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampController : MonoBehaviour {

    delegate void EnemyOutOfReachDelegate(GameObject target);
    delegate void SetEnemyDelegate(GameObject target);

    public List<GameObject> enemies;
    public SimpleSoldierController[] soldiersController;
    EnemyOutOfReachDelegate enemyOutOfReach;
    SetEnemyDelegate setEnemy;

    private int soldiersCount = 3;
    
    // Use this for initialization
	void Start () {
        soldiersController = new SimpleSoldierController[soldiersCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            SimpleSoldierController ssc = (SimpleSoldierController)transform.GetChild(i).GetComponent<SimpleSoldierController>();
            enemyOutOfReach += ssc.ForgetTarget;
            soldiersController[i] = ssc;
            //setEnemy += ssc.SetTarget; 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when an enemy get in range;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
            SetSoldierTarget(other.gameObject);
        }

    }

    private void SetSoldierTarget(GameObject _target)
    {
        for (int i = 0; i < soldiersController.Length; i++)
        {
            if(soldiersController[i].target == null)
            {
                soldiersController[i].SetTarget(_target);
                Debug.Log(_target.name);
                break;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //todo delegate to who has this target stop to attack
        if(other.gameObject.tag == "Enemy")
        {
            enemyOutOfReach(other.gameObject);
        }

    }
}
