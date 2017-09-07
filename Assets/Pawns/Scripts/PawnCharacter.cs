using UnityEngine;
using UnityEngine.UI;

public class PawnCharacter : MonoBehaviour {

	public float maxHealth;
    public float health;
    public float defense;
    public float attack;
    public float attackRate;
    public bool isDying;
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

	public virtual void OnDying()
	{
		isDying = true;
        PlayerStats.AddMoney(10);
     
        Instantiate(painSoundPrefab, transform.position, Quaternion.identity);

        //Instantiate(coinEffectPrefab, transform.position, Quaternion.identity);



        Destroy(gameObject);
	}

    public virtual bool Damage(float _damage)
    {

        float realDamage = _damage - defense;
		if (realDamage < 0)
			realDamage = 0;

        health -= realDamage;

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, maxHealth);
           
        }
		//Debug.Log(health);
		if (health <= 0)
        {
			return true;
            //Destroy(gameObject);
            //return;
        }

		return false;
    }
}
