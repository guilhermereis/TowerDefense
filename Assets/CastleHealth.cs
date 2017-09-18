using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour {

    public float health;
    public float maxHealth;
    Animator castleDestructionAnimator;
    private List<PawnCharacter> enemies;
    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
    Canvas HUD;
    Image healthBar;

    AudioSource implosionSound;

    ParticleSystem demolitionEffect;
	// Use this for initialization
	void Start () {
        countdown = 0;

        enemies = new List<PawnCharacter>();

        maxHealth = 5000.0f;

        health = maxHealth;

        castleDestructionAnimator = GetComponent<Animator>();

        demolitionEffect = GetComponentInChildren<ParticleSystem>();

        implosionSound = GetComponent<AudioSource>();

        Canvas[] allCanvas = GameObject.FindObjectsOfType<Canvas>();
        foreach(Canvas c in allCanvas)
        {
            if (c.CompareTag("HUD"))
            {
                HUD = c;
                break;
            }

        }
        healthBar = HUD.transform.Find("Castle Info").transform.Find("BG").transform.Find("Filled").GetComponent<Image>();
    }
	
    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {

        if (castleDestructionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !castleDestructionAnimator.IsInTransition(0))
        {
            Destroy(gameObject,3);
        }

    }


    void UpdateHealthBarGfx(float value)
    {
        healthBar.fillAmount = value/maxHealth;
        
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        UpdateHealthBarGfx(health);
        if(health <=0)
        {
            CastleDestructionEvent();
            GameController.ChangeGameState(GameState.GameOver);
            
            //gameObject.SetActive(false);
            
        }

    }
    //here we play the castle destruction animatio, play the explosion effects and play the implosion sound
    void CastleDestructionEvent()
    {
        //castleDestructionAnimator.SetBool("destroyed", true);
        castleDestructionAnimator.enabled = true;
        demolitionEffect.Play(true);
        implosionSound.Play();

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
