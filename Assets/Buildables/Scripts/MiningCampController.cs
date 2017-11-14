using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningCampController : BuildableController {
    /* defining variables*/
    public delegate void FullDelegate();
    public FullDelegate full;
    //default values
    public int currentGold = 0;
    public int maxCapacity = 50;
    public bool isFull;
    public int goldByWave = 50;
    public Canvas canvas;
    public GameObject isFullButtonUI;
    public GameObject fullButton;
    public GameObject moneyCollectedAudio;
    public int storageLevel;
    public int miningRateLevel;
    public int mineLevel;
    public bool isMaxLevel;

    private string UITooltipBaseText = "MINE'S READY!\nCLICK TO COLLECT\n";

    protected override void Awake()
    {
        base.Awake();
    }
    // Use this for initialization
    public override int GetSellCostWithInterest()
    {
        return unitBlueprint.withInterest_sellcost;
    }

    void Start () {

       
        unitBlueprint.withInterest_sellcost = getUnitBlueprint().sell_cost;

        storageLevel = 1;
        miningRateLevel = 1;
        mineLevel = 1;

       GameController.endWaveDelegate += AddGold;
       foreach(Canvas c in GameObject.FindObjectsOfType<Canvas>())
       {
            if (c.CompareTag("HUD"))
            {
                canvas = c;
                break;
            }
       }
        fullButton = Instantiate(isFullButtonUI);
        fullButton.SetActive(false);
        fullButton.transform.SetParent(canvas.transform.Find("GoldMinesButtonHolder").transform,false);

        fullButton.GetComponent<Button>().onClick.AddListener(Withdrawl);
    }

    private void OnDestroy()
    {
        GameController.endWaveDelegate -= AddGold;
        full = null;
    }

    //add gold until reach maxcapacity
    private void AddGold()
    {
     
        currentGold = Mathf.Clamp(currentGold + goldByWave, 0, maxCapacity);
        if (currentGold == maxCapacity && !isFull)
        {
            isFull = true;
            fullButton.SetActive(true);
            string tooltipText = UITooltipBaseText + currentGold + " GOLD";
            fullButton.GetComponent<TooltipController>().tooltipText = tooltipText;
        }
      
    }
    IEnumerator GoldTextAnimation()
    {
        float timer = 1.5f;
        Text goldText = fullButton.GetComponentInChildren<Text>();
        Vector3 originalPosition= goldText.transform.position;
        goldText.enabled = true;
        while (timer > 0)
        {
            Vector3 dir = 20.0f * Vector3.up * Time.deltaTime;
            goldText.transform.Translate(dir, Space.World);
            timer -= Time.deltaTime;

            yield return null;
        }
        goldText.enabled = false;
        goldText.transform.position = originalPosition;
        fullButton.GetComponent<TooltipController>().hideTooltip(null);
        fullButton.SetActive(false);
        fullButton.GetComponent<Button>().enabled = true;
        fullButton.GetComponent<Image>().enabled = true;

    }
    //reset isfull state and remove all the money inside
    public void Withdrawl()
    {
        if (!TopRightMenu.isGamePaused)
        {
            int goldToReturn = currentGold;
            currentGold = 0;
            isFull = false;
            fullButton.GetComponent<Button>().enabled = false;
            fullButton.GetComponent<Image>().enabled = false;
            StopCoroutine(GoldTextAnimation());
            StartCoroutine(GoldTextAnimation());
            //fullButton.GetComponent<TooltipController>().hideTooltip(null);
            //fullButton.SetActive(false);
            SoundToPlay.PlaySfx(moneyCollectedAudio);
            int added = PlayerStats.AddMoney(goldToReturn);
            GameController.MoneyCollected(added, true);
        }
    }
   
    private void UpgradeMaxGold(int multiplier)
    {
        storageLevel++;
        maxCapacity = maxCapacity * multiplier;
    }

    private void UpgradeGoldByWave(int multplier)
    {
        miningRateLevel++;
        goldByWave = goldByWave * multplier;
    }
    
    private void Update()
    {
        fullButton.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0.5f, 0f) + transform.position);
        float cameraZoom = Mathf.Clamp((1f - Camera.main.orthographicSize / 11), 0.5f, 1f);//Magic Numbers to get a good scale from the camera zoom
        fullButton.transform.localScale = new Vector3(2*cameraZoom, 2*cameraZoom, 2*cameraZoom);
    }

    public void Upgrade()
    {
        if (!isMaxLevel)
        {
            if (isFull)
                Withdrawl();

            mineLevel++;
            
            UpgradeGoldByWave(2);
            UpgradeMaxGold(4);

            if(mineLevel == 3)
                isMaxLevel = true;
            
        }

        

    }

   
}
