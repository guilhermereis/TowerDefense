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
		Destroy(gameObject);
	}

    public virtual bool Damage(float _damage)
    {

        health -= _damage;
		healthBar.UpdateHealthBar(health, maxHealth);
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
