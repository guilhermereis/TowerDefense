﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraOptions : MonoBehaviour {
    BuildManager buildManager;
    private int upgradeAP1price = 0;
    private int upgradeAP2price = 0;
    private int upgradeAP3price = 0;

    private int upgradeFR1price = 0;
    private int upgradeFR2price = 0;
    private int upgradeFR3price = 0;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    private void SetPrices()
    {
        string SelectedGameObjectName = buildManager.getSelectedGameObject().name;
        Debug.Log("SELECTED ++++++++ : " + SelectedGameObjectName);
        if (SelectedGameObjectName == "PrefabArcherTower1(Clone)")
        {
            upgradeFR1price = 100;
            upgradeFR2price = 200;
            upgradeFR3price = 300;

            upgradeAP1price = 200;
            upgradeAP2price = 300;
            upgradeAP3price = 400;            
        }
        else if (SelectedGameObjectName == "PrefabArcherTower2(Clone)")
        {
            upgradeFR1price = 200;
            upgradeFR2price = 400;
            upgradeFR3price = 600;

            upgradeAP1price = 300;
            upgradeAP2price = 600;
            upgradeAP3price = 900;
        }
        else if (SelectedGameObjectName == "PrefabArcherTower3(Clone)")
        {
            upgradeFR1price = 500;
            upgradeFR2price = 1000;
            upgradeFR3price = 1500;

            upgradeAP1price = 600;
            upgradeAP2price = 1200;
            upgradeAP3price = 1800;
        }
    }

    public void upgradeAP1()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getAttackPowerLVL() < 1)
        {
            if (PlayerStats.Money - upgradeAP1price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeAP1price);
                tower.setAttackPowerLVL(1);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
        }
    }
    public void upgradeAP2()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();        
        if (tower.getAttackPowerLVL() < 2)
        {
            if (PlayerStats.Money - upgradeAP2price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeAP2price);
                tower.setAttackPowerLVL(2);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
        }
    }
    public void upgradeAP3()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();        
        if (tower.getAttackPowerLVL() < 3)
        {
            if (PlayerStats.Money - upgradeAP3price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeAP3price);
                tower.setAttackPowerLVL(3);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
            
        }
    }
    //----------------------------------------------------------
    public void upgradeFR1()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 1)
        {
            if (PlayerStats.Money - upgradeFR1price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeFR1price);
                tower.setFireRateLVL(1);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
        }
    }
    public void upgradeFR2()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 2)
        {
            if (PlayerStats.Money - upgradeFR2price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeFR2price);
                tower.setFireRateLVL(2);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
        }
    }
    public void upgradeFR3()
    {
        SetPrices();
        TowerController tower = buildManager.getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 3)
        {
            if (PlayerStats.Money - upgradeFR3price >= 0)
            {
                PlayerStats.AddMoney(-1 * upgradeFR3price);
                tower.setFireRateLVL(3);
                tower.SetFireRateAndAttackPower();
                buildManager.OnUnitUpgrade();
            }
            else
            {
                Debug.Log("You don't have enough money to buy this upgrade.");
            }
        }
    }
}
