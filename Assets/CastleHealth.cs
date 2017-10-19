using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour {

    public float health;
    public float maxHealth;
    public static Animator castleDestructionAnimator;
    private List<PawnCharacter> enemies;
    private Animator castleUIFeedbackAnimator;
    private TooltipController tooltipController;

    public float damageRate = 0.2f;
    public float countdown;
    private bool canBeDamaged;
    private WaveSpawner ws;
    public GameObject repairButton;

    public int repairCostMultiplier = 30;
    public float repairAmmountPercentual = 0.2f;

    public Color buttonDisabledColor;
    public Color buttonEnabledColor;

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

        healthBar = HUD.transform.Find("Castle Info").transform.Find("BG").transform.Find("Filled").GetComponent<Image>();
        castleUIFeedbackAnimator = HUD.transform.Find("Castle Info").GetComponent<Animator>();
        tooltipController = repairButton.GetComponent<TooltipController>();
    }

	void Start () {
        countdown = 0;

        enemies = new List<PawnCharacter>();

        maxHealth = 5000.0f;

        health = maxHealth;

        castleDestructionAnimator = GetComponent<Animator>();

        demolitionEffect = GetComponentInChildren<ParticleSystem>();

        implosionSound = GetComponent<AudioSource>();

        ws = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
    }

    public float CalculateCost(float waveNumber)
    {
        float y = Mathf.Log(5 * waveNumber + 1, 10) * 2.55f;
        return y;
    }

    public void Repair()
    {
        int cost = Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
        if (PlayerStats.Money >= cost)
        {
            if (health < maxHealth)
            {
                PlayerStats.AddMoney(-1 * cost);
                health += repairAmmountPercentual * maxHealth;
                UpdateHealthBarGfx(health);
            }
            else
            {
                Debug.Log("Castle is at full health !");
            }
        }
        else {
            Debug.Log("You don't have enough money to upgrade the Castle !");
        }
    }

    void setRepairCostText(WaveSpawner ws) {
        string baseString = "PAY TO REPAIR "+ repairAmmountPercentual *100 + "% OF YOUR CASTLE'S HEALTH\nTHE PRICE INCREASES WITH THE PROGRESSION OF THE GAME\nCURRENT COST: ";
        tooltipController.tooltipText = baseString + Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
    }


    //Change this later to actually the monsters hit with theirs fire rate
	// Update is called once per frame
	void Update () {

        if (castleDestructionAnimator != null)
        {
            if (castleDestructionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !castleDestructionAnimator.IsInTransition(0))
            {
                //Destroy(gameObject,3);
            }
        }

        if (repairButton) {
            if (health == maxHealth)
            {
                repairButton.GetComponent<Button>().interactable = false;
            }
            else {
                repairButton.GetComponent<Button>().interactable = true;
            }

            int cost = Mathf.RoundToInt(repairCostMultiplier * CalculateCost(ws.waveNumber));
            if (PlayerStats.Money < cost)
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
        setRepairCostText(ws);
    }


    public void UpdateHealthBarGfx(float value)
    {
        healthBar.fillAmount = value/maxHealth;
        castleUIFeedbackAnimator.SetTrigger("UnderAttack");
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
