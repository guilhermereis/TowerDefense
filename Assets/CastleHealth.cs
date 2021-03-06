﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour {

    public float health;
    public float maxHealth;
    public bool wasDamaged;
    public static Animator castleDestructionAnimator;
    private List<PawnCharacter> enemies;
    private Animator castleUIFeedbackAnimator;
    private TooltipController tooltipController;
    public GameObject castleUnderAttackSound;

    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
    private WaveSpawner ws;
    public GameObject repairButton;
    public GameObject repairSound;

    public int repairCostMultiplier = 1;
    public float repairAmmountPercentual = 0.2f;

    public Color buttonDisabledColor;
    public Color buttonNoMoneyColor;
    public Color buttonEnabledColor;
    private float underAttackCounter;
    public float underAttackCounterLimit = 2f;
    private bool isUnderAttack = false;
    private bool isDestroyed = false;

    Canvas HUD;
    Image healthBar;

    AudioSource implosionSound;

    ParticleSystem demolitionEffect;
    // Use this for initialization

    void Awake()
    {
        Canvas[] allCanvas = GameObject.FindObjectsOfType<Canvas>();
        foreach (Canvas c in allCanvas)
        {
            if (c.CompareTag("HUD"))
            {
                HUD = c;
                break;
            }
        }

        if (!repairButton) {
            repairButton = HUD.transform.Find("Castle Info").transform.Find("RepairButton").gameObject;
            repairButton.GetComponent<Button>().onClick.AddListener(Repair);
        }

        underAttackCounter = 0f;
        healthBar = HUD.transform.Find("Castle Info").transform.Find("BG").transform.Find("Filled").GetComponent<Image>();
        castleUIFeedbackAnimator = HUD.transform.Find("Castle Info").GetComponent<Animator>();
        tooltipController = repairButton.GetComponent<TooltipController>();
    }

	void Start () {
        isUnderAttack = false;
        isDestroyed = false;
    countdown = 0;

        enemies = new List<PawnCharacter>();

        maxHealth = 5000;
        UpdateHealthBarGfx(maxHealth,false);

        health = maxHealth;

        castleDestructionAnimator = GetComponent<Animator>();

        demolitionEffect = GetComponentInChildren<ParticleSystem>();

        implosionSound = GetComponent<AudioSource>();

        ws = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
    }

    public float CalculateCost(float waveNumber)
    {
        float y = 8 * waveNumber;
        return y;
    }

    public void Repair()
    {
        if (!TopRightMenu.isGamePaused) {
            int cost = Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
            if (PlayerStats.Money >= cost)
            {
                if (health < maxHealth)
                {
                    int spent = PlayerStats.AddMoney(-1 * cost);
                    GameController.MoneyCollected(spent, false);
                    GameController.Repair();
                    health = Mathf.Clamp(health + repairAmmountPercentual * maxHealth,0f,maxHealth);
                    UpdateHealthBarGfx(health);
                    SoundToPlay.PlaySfx(repairSound,0.2f);
                }
                else
                {
                    GameController.Repair();
                }
            }
            else
            {
                Debug.Log("You don't have enough money to upgrade the Castle !");
            }
        }
    }

    void setRepairCostText(WaveSpawner ws) {
        string baseString = "REPAIR "+ repairAmmountPercentual *100 + "% OF YOUR CASTLE'S HEALTH\nTHE PRICE INCREASES WITH TIME\nCURRENT COST: ";
        tooltipController.tooltipText = baseString + Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
    }


    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {

        if (isUnderAttack) {
            underAttackCounter += Time.deltaTime;
        }

        if (underAttackCounter >= underAttackCounterLimit)
        {
            underAttackCounter = 0f;
            isUnderAttack = false;
        }

        if (castleDestructionAnimator != null)
        {
            if (castleDestructionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !castleDestructionAnimator.IsInTransition(0))
            {
                //Destroy(gameObject,3);
            }
        }

        if (repairButton) {

            int cost = Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
            if (PlayerStats.Money < cost)
            {
                repairButton.GetComponent<Button>().interactable = false;
                repairButton.GetComponent<Image>().color = buttonNoMoneyColor;
            }
            else
            {
                if (health == maxHealth)
                {
                    repairButton.GetComponent<Button>().interactable = false;
                    repairButton.GetComponent<Image>().color = buttonDisabledColor;
                }
                else
                {
                    repairButton.GetComponent<Button>().interactable = true;
                    repairButton.GetComponent<Image>().color = buttonEnabledColor;
                }
            }

        }
        setRepairCostText(ws);
    }


    public void UpdateHealthBarGfx(float value, bool animate = true)
    {
        if(animate)
            castleUIFeedbackAnimator.SetTrigger("UnderAttack");
        healthBar.fillAmount = value/maxHealth;
        
    }

    public void ApplyDamage(float damage)
    {
        wasDamaged = true;
        health -= damage;
        UpdateHealthBarGfx(health);

        if (!isDestroyed) {
            if (!isUnderAttack)
            {
                isUnderAttack = true;
                SoundToPlay.PlaySfx(castleUnderAttackSound);
            }

            if (health <= 0)
            {
                CastleDestructionEvent();
                GameController.ChangeGameState(GameState.GameOver);
                isDestroyed = true;
                //gameObject.SetActive(false);

            }
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
