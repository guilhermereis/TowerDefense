using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour {

    public float health;

    private List<PawnCharacter> enemies;
    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
	// Use this for initialization
	void Start () {
        countdown = 0;
        enemies = new List<PawnCharacter>();
        health = 5000.0f;

    }
	
    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {
		

	}

    public void ApplyDamage(float damage)
    {
        health -= damage;
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
