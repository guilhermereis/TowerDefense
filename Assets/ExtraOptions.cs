using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraOptions : MonoBehaviour {
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void upgradeAP1()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getAttackPowerLVL() < 1)
        {
            tower.setAttackPowerLVL(1);
            tower.SetFireRateAndAttackPower();
        }
    }
    public void upgradeAP2()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();        
        if (tower.getAttackPowerLVL() < 2)
        {
            tower.setAttackPowerLVL(2);
            tower.SetFireRateAndAttackPower();
        }
    }
    public void upgradeAP3()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();        
        if (tower.getAttackPowerLVL() < 3)
        {
            tower.setAttackPowerLVL(3);
            tower.SetFireRateAndAttackPower();
        }
    }
    //----------------------------------------------------------
    public void upgradeFR1()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 1)
        {
            tower.setFireRateLVL(1);
            tower.SetFireRateAndAttackPower();
        }
    }
    public void upgradeFR2()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 2)
        {
            tower.setFireRateLVL(2);
            tower.SetFireRateAndAttackPower();
        }
    }
    public void upgradeFR3()
    {
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 3)
        {
            tower.setFireRateLVL(3);
            tower.SetFireRateAndAttackPower();
        }
    }
}
