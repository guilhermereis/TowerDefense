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

    protected override void Awake()
    {
        base.Awake();
       
    }
    // Use this for initialization
    void Start () {
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
        fullButton.transform.SetParent(canvas.transform,false);
        
        fullButton.GetComponent<Button>().onClick.AddListener(Withdrawl);


    }
	//add gold until reach maxcapacity
    public void AddGold()
    {
     
        currentGold = Mathf.Clamp(currentGold + goldByWave, 0, maxCapacity);
        if (currentGold == maxCapacity && !isFull)
        {
            isFull = true;
            fullButton.SetActive(true);

        }
      
    }
    //reset isfull state and remove all the money inside
    public void Withdrawl()
    {
        int goldToReturn = currentGold;
        currentGold = 0;
        isFull = false;
        fullButton.SetActive(false);
        PlayerStats.AddMoney(goldToReturn);
    }
   
    public void UpgradeMaxGold()
    {
        maxCapacity += 50;
    }

    public void UpgradeGoldByWave()
    {
        goldByWave += 50;
    }
    
    private void Update()
    {
        fullButton.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0.5f, 0f) + transform.position);
    }

}
