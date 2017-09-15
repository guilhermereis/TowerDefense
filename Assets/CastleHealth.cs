using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour {

    public float health;
    public float maxHealth;
    private List<PawnCharacter> enemies;
    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
    Canvas HUD;
    Image healthBar;
	// Use this for initialization
	void Start () {
        countdown = 0;
        enemies = new List<PawnCharacter>();
        maxHealth = 5000.0f;
        health = maxHealth;
        Canvas[] allCanvas = GameObject.FindObjectsOfType<Canvas>();
        foreach(Canvas c in allCanvas)
        {
            if (c.CompareTag("HUD"))
            {
                HUD = c;
                break;
            }

        }
        healthBar = HUD.transform.Find("CastleFeedBack").transform.Find("HpBg").transform.Find("FillBar").GetComponent<Image>();
    }
	
    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {
		

	}


    void UpdateHealthBarGfx(float value)
    {
        healthBar.fillAmount = value/maxHealth;
        //Debug.Log(value);
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        UpdateHealthBarGfx(health);
        if(health <=0)
        {
            GameController.ChangeGameState(GameState.GameOver);
            gameObject.SetActive(false);
            
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            canBeDamaged = true;
            enemies.Add(other.gameObject.GetComponent<PawnCharacter>());
        }
    }
}
