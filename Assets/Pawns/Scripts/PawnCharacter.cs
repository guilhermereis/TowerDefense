using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PawnCharacter : MonoBehaviour {

	public float maxHealth;
    public float health;
    public float defense;
    public float attack;
    public float attackRate;
    public bool isDead;
    public bool isSlow;

    public bool exploded;

	public PawnHealthBarGUI healthBar;
    public AudioSource painSoundPrefab;

    public GameObject coinEffectPrefab;
        


    private void Awake()
	{
		health = maxHealth;
	}

	private void Start()
	{
       
        healthBar = (PawnHealthBarGUI)GetComponent<PawnHealthBarGUI>();
        
		
	}

	public virtual void Die()
	{
		isDead = true;

        

        //Instantiate(coinEffectPrefab, transform.position, Quaternion.identity);

        if (gameObject.tag.Equals("Enemy"))
        {
            if(!exploded)
                PlayerStats.AddMoney(50);

            SoundToPlay.PlayAtLocation(painSoundPrefab, transform.position, Quaternion.identity);
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

    public virtual bool Damage(float _damage, out bool hit)
    {
        hit = false;
        if (!isDead)
        {
            float realDamage = _damage - defense;
            if (realDamage <= 0)
            {
                realDamage = (int) 0.10f * _damage;
                hit = true;

            }
            else
                hit = true;

            

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
                Die();
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
