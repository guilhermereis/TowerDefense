using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningCampController : BuildableController {
    /* defining variables*/
    public int currentGold = 0;
    public int maxCapacity = 500;
    public bool isFull;
    public int goldByWave = 50;

    protected override void Awake()
    {
        base.Awake();
        
    }
    // Use this for initialization
    void Start () {
		
	}
	//add gold until reach maxcapacity
    public int AddGold()
    {
        currentGold = Mathf.Clamp(goldByWave, 0, maxCapacity);
        return currentGold;
    }
    //reset isfull state and remove all the money inside
    public int Withdrawl()
    {
        int goldToReturn = currentGold;
        currentGold = 0;
        isFull = false;
        return goldToReturn;
    }
   
    public void UpgradeMaxGold()
    {
        maxCapacity += 50;
    }

    public void UpgradeGoldByWave()
    {
        goldByWave += 50;
    }

	// Update is called once per frame
	void Update () {

        if(GameController.gameState == GameState.EndWave && !isFull)
        {
            if(AddGold() == maxCapacity)
                isFull = true;
        }
	}
}
