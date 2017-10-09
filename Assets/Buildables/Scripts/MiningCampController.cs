using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningCampController : BuildableController {
    /* defining variables*/
    public delegate void FullDelegate();
    public FullDelegate full;
    //default values
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
       GameController.endWaveDelegate += AddGold;
    }
	//add gold until reach maxcapacity
    public void AddGold()
    {
     
        currentGold = Mathf.Clamp(currentGold + goldByWave, 0, maxCapacity);
        if (currentGold == maxCapacity && !isFull)
            isFull = true;
      
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

	
}
