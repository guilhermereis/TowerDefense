using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour {

    public float health = 5000f;

    private List<PawnCharacter> enemies;
    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
	// Use this for initialization
	void Start () {
        countdown = 0;
        enemies = new List<PawnCharacter>();
	}
	
    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {
		
        if(countdown <= 0 && canBeDamaged)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                health -= enemies[i].attack;
                if (health <= 0)
                {
                    GameController.gc.ChangeGameState(GameController.GameState.GameOver);
                    gameObject.SetActive(false);
                    break;
                }

            }
            countdown = 1 / damageRate;
        }

        countdown -= Time.deltaTime;

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
