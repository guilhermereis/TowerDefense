using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum DamageType { Blood, Fire, Ice, Explosion }

public class PawnCharacter : MonoBehaviour {

	public float maxHealth;
    public float health;
    public float defense;
    public float attack;
    public float attackRate;
    public bool isDead;
    public bool isSlow;
    private bool debug;
    public bool exploded;

	public PawnHealthBarGUI healthBar;
    public GameObject painSoundPrefab;

    public GameObject coinEffectPrefab;
        


    private void Awake()
	{
		health = maxHealth;
	}

	private void Start()
	{
       
        healthBar = (PawnHealthBarGUI)GetComponent<PawnHealthBarGUI>();
        
		
	}

	public virtual void Die(DamageType _damageType)
	{
		isDead = true;
        PawnType myType = GetComponent<PawnController>().type;
        int myId = GetComponent<PawnController>().monster_id;
        GameController.AddMonsterKilled(myType, _damageType);

        //Instantiate(coinEffectPrefab, transform.position, Quaternion.identity);
        int goldAmmount = 10;
        if (gameObject.tag.Equals("Enemy"))
        {
            if (!exploded)
            {
                switch (myId) {
                    case 4:
                        goldAmmount = 200;
                        break;
                    case 5:
                        goldAmmount = 12;
                        break;
                    case 6:
                        goldAmmount = 13;
                        break;
                    case 7:
                        goldAmmount = 14;
                        break;
                    case 8:
                        goldAmmount = 400;
                        break;
                    case 9:
                        goldAmmount = 20;
                        break;
                    case 10:
                        goldAmmount = 23;
                        break;

                }
                PlayerStats.AddMoney(goldAmmount);
                GameController.MoneyCollected(goldAmmount, false);
            }

            SoundToPlay.PlayAtLocation(painSoundPrefab, transform.position, Quaternion.identity);
            //SoundToPlay.PlaySfx(painSoundPrefab);
            //Instantiate(painSoundPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<PawnController>().ChangeState(PawnController.PawnState.Dead);


            if (gameObject.GetComponent<PawnController>().deadPawn != null)
            {
                gameObject.GetComponent<PawnController>().deadPawn(gameObject);
            }
        }
        else
        {
            gameObject.GetComponent<PawnController>().ChangeState(PawnController.PawnState.Dead);
            //gameObject.GetComponent<SimpleSoldierController>().enabled = false;
            //gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject);
            return;
        }
        
        
        //Destroy(gameObject);
    }

    public virtual bool Damage(float _damage, out bool hit,DamageType _damageType)
    {
        debug = false;
        hit = false;
        float realDamage = 0;
        if (!isDead)
        {
            if(_damageType == DamageType.Explosion)
            {
                hit = true;
                realDamage = _damage;

            }
            else
            {
                realDamage = _damage - defense;
                if (realDamage <= 0)
                {
                    realDamage = (int) (0.10f * _damage);
                    hit = true;
                }
                else
                    hit = true;

            }

            if(debug)
                realDamage = (int)0.20f * _damage;

            health -= realDamage;

            //start coroutine that makes pawn stop for a moment
            //gameObject.GetComponent<PawnController>().StopCoroutine("HitStop");
            //gameObject.GetComponent<PawnController>().StartCoroutine("HitStop");
            //s
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(health, maxHealth);
           
            }
		    //Debug.Log(health);
		    if (health <= 0)
            {
                Die(_damageType);
			    return true;
                //Destroy(gameObject);
                //return;
            }

        }

		return false;
    }

    //Called when Death animation has ended
    public virtual void OnDeathAnimationEnd() {

    }

    //thread responsible for slow state.
    IEnumerator SlowTime(float _slowAmount)
    {

        float slowAmount = _slowAmount;
        isSlow = true;
        //Debug.Log("material lenght " + gameObject.GetComponentsInChildren<Renderer>().Length);

        while (slowAmount >= 0)
        {
            if (isDead)
                break;

            slowAmount -= Time.deltaTime;

            yield return null;
        }

        isSlow = false;
    }
}
