﻿using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop instance;

    public Button standardUnitButton;
    public Button secondaryUnitButton;

    public UnitBlueprint standardUnit;
    public UnitBlueprint missileLauncher;
    public UnitBlueprint towerLevel2;
    public UnitBlueprint towerLevel3;
    public UnitBlueprint towerSlow;
    public UnitBlueprint towerTesla;

    public int upgradeT1As1price = 100;
    public int upgradeT1As2price = 200;
    public int upgradeT1As3price = 300;
    public int upgradeT1Ad1price = 200;
    public int upgradeT1Ad2price = 300;
    public int upgradeT1Ad3price = 400;

    public int upgradeT2As1price = 200;
    public int upgradeT2As2price = 400;
    public int upgradeT2As3price = 600;
    public int upgradeT2Ad1price = 300;
    public int upgradeT2Ad2price = 600;
    public int upgradeT2Ad3price = 900;

    public int upgradeT3As1price = 500;
    public int upgradeT3As2price = 1000;
    public int upgradeT3As3price = 1500;
    public int upgradeT3Ad1price = 600;
    public int upgradeT3Ad2price = 1200;
    public int upgradeT3Ad3price = 1800;

    BuildManager buildManager;

    public Color CoinEnabledColor;
    public Color CoinTextEnabledColor;

    public Color CoinDisabledColor;
    public Color CoinTextDisabledColor;

    private bool canBuildPrimary = true;
    private bool canBuildSecondary = true;
    private bool updateGui = true;

    public enum UpgradeType { AttackPower, FireRate };
    public int GetUpgradePrice(int towerLVL, UpgradeType upgradeType, int upgradeLVL)
    {
        int return_value = 0;
        if (towerLVL == 1)
        {
            if (upgradeType == UpgradeType.AttackPower)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT1Ad1price; break; }
                    case 2: { return_value = upgradeT1Ad2price; break; }
                    case 3: { return_value = upgradeT1Ad3price; break; }

                }
            }
            else if (upgradeType == UpgradeType.FireRate)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT1As1price; break; }
                    case 2: { return_value = upgradeT1As2price; break; }
                    case 3: { return_value = upgradeT1As3price; break; }

                }
            }
        }
        else if (towerLVL == 2)
        {
            if (upgradeType == UpgradeType.AttackPower)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT2Ad1price; break; }
                    case 2: { return_value = upgradeT2Ad2price; break; }
                    case 3: { return_value = upgradeT2Ad3price; break; }

                }
            }
            else if (upgradeType == UpgradeType.FireRate)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT2As1price; break; }
                    case 2: { return_value = upgradeT2As2price; break; }
                    case 3: { return_value = upgradeT2As3price; break; }

                }
            }
        }
        else if (towerLVL == 3)
        {
            if (upgradeType == UpgradeType.AttackPower)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT3Ad1price; break; }
                    case 2: { return_value = upgradeT3Ad2price; break; }
                    case 3: { return_value = upgradeT3Ad3price; break; }

                }
            }
            else if (upgradeType == UpgradeType.FireRate)
            {
                switch (upgradeLVL)
                {
                    case 1: { return_value = upgradeT3As1price; break; }
                    case 2: { return_value = upgradeT3As2price; break; }
                    case 3: { return_value = upgradeT3As3price; break; }

                }
            }
        }
        return return_value;
    }

    void Awake()
    {
        if (instance != null) //if instance has been set before 
        {
            Debug.LogError("More than one Shop in scene !");
            return;
        }
        instance = this;
    }

    void Start()
    {
        buildManager = BuildManager.instance;
        initializeUIValues();
    }

    private void Update()
    {
        if (PlayerStats.Money < standardUnit.cost)
        {
            standardUnitButton.interactable = false;
            standardUnitButton.transform.Find("Coin").GetComponent<Image>().color = CoinDisabledColor;
            standardUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().color = CoinTextDisabledColor;
            canBuildPrimary = false;
        } else {
            standardUnitButton.interactable = true;
            standardUnitButton.transform.Find("Coin").GetComponent<Image>().color = CoinEnabledColor;
            standardUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().color = CoinTextEnabledColor;
            canBuildPrimary = true;
        }

        if (PlayerStats.Money < missileLauncher.cost)
        {
            secondaryUnitButton.interactable = false;
            secondaryUnitButton.transform.Find("Coin").GetComponent<Image>().color = CoinDisabledColor;
            secondaryUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().color = CoinTextDisabledColor;
            canBuildSecondary = false;
        } else {
            secondaryUnitButton.interactable = true;
            secondaryUnitButton.transform.Find("Coin").GetComponent<Image>().color = CoinEnabledColor;
            secondaryUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().color = CoinTextEnabledColor;
            canBuildSecondary = true;
        }
    }

    public void SelectStandardUnit() {
        if (canBuildPrimary) {
            Debug.Log("Standard Turret Selected");
            buildManager.SelectUnitToBuild(standardUnit);
        }
    }

    public void SelectSecondaryUnit() {
        if (canBuildSecondary) {
            Debug.Log("Secondary Turret Selected");
            buildManager.SelectUnitToBuild(missileLauncher);
        }
    }

    public void initializeUIValues() {
        standardUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().text = "" + standardUnit.cost;
        standardUnitButton.transform.Find("Coin").transform.Find("PriceShadow").GetComponent<Text>().text = "" + standardUnit.cost;
        secondaryUnitButton.transform.Find("Coin").transform.Find("Price").GetComponent<Text>().text = "" + missileLauncher.cost;
        secondaryUnitButton.transform.Find("Coin").transform.Find("PriceShadow").GetComponent<Text>().text = "" + missileLauncher.cost;
    }
}
