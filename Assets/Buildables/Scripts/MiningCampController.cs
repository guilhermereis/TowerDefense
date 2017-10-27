﻿using System.Collections;
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
        full = null;
    }

    //add gold until reach maxcapacity
    public void AddGold()
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
    //reset isfull state and remove all the money inside
    public void Withdrawl()
    {
        int goldToReturn = currentGold;
        currentGold = 0;
        isFull = false;
        fullButton.GetComponent<TooltipController>().hideTooltip(null);
        fullButton.SetActive(false);
        SoundToPlay.PlaySfx(moneyCollectedAudio);
        int added = PlayerStats.AddMoney(goldToReturn);
        GameController.MoneyCollected(added, true);
    }
   
    public void UpgradeMaxGold()
    {
        storageLevel++;
        maxCapacity += 50;
    }

    public void UpgradeGoldByWave()
    {
        miningRateLevel++;
        goldByWave += 50;
    }
    
    private void Update()
    {
        fullButton.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0.5f, 0f) + transform.position);
        float cameraZoom = Mathf.Clamp((1f - Camera.main.orthographicSize / 11), 0.5f, 1f);//Magic Numbers to get a good scale from the camera zoom
        fullButton.transform.localScale = new Vector3(2*cameraZoom, 2*cameraZoom, 2*cameraZoom);
    }

}
