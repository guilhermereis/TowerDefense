using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWheelController : MonoBehaviour {

    public UnitBlueprint tower;
    public GameObject upgradeWheel;
    private Animator anim;
    private int attackSpeedLvl = 0;
    private int attackDmgLvl = 0;
    private int towerLvl = 0;
    private int specialization = 0;

    //Sprites
    public Sprite as1Sprite;
    public Sprite as2Sprite;
    public Sprite as3Sprite;

    public Sprite ad1Sprite;
    public Sprite ad2Sprite;
    public Sprite ad3Sprite;

    public void clearButtons() {
        upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerTesla").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerArcher").gameObject.SetActive(false);
    }

    public void setTowerLvl(int newLvl) {
        clearButtons();
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerLevel").transform.Find("Cost").GetComponent<Text>().text = "" + Shop.instance.standardUnit.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerLevel").transform.Find("CostShadow").GetComponent<Text>().text = "" + Shop.instance.standardUnit.upgrade_cost;
                break;
            case 1:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerTesla").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerArcher").gameObject.SetActive(true);

                upgradeWheel.transform.Find("UpgradeTowerTesla").transform.Find("Cost").GetComponent<Text>().text = "" + Shop.instance.towerTesla.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerTesla").transform.Find("CostShadow").GetComponent<Text>().text = "" + Shop.instance.towerTesla.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerIcer").transform.Find("Cost").GetComponent<Text>().text = "" + Shop.instance.towerSlow.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerIcer").transform.Find("CostShadow").GetComponent<Text>().text = "" + Shop.instance.towerSlow.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerArcher").transform.Find("Cost").GetComponent<Text>().text = "" + Shop.instance.towerLevel2.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerArcher").transform.Find("CostShadow").GetComponent<Text>().text = "" + Shop.instance.towerLevel2.upgrade_cost;

                break;
            case 2:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(true);
                break;
        }
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + tower.sell_cost;
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + tower.sell_cost;
        this.towerLvl = newLvl;
    }

    public void setSpecialization(int newSpec) {
        switch (newSpec)
        {
            case 0: //Icer Tower
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(false);
                break;
            case 1: //Archer Tower 3
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(false);
                break;

            case 2: //Tesla Tower
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(false);
                break;
        }

        specialization = newSpec;
        upgradeWheel.transform.Find("UpgradeTowerTesla").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerArcher").gameObject.SetActive(false);
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + tower.sell_cost;
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + tower.sell_cost;
    }

    public void setAttackSpeedLvl(int newLvl) {
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as1Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = true;
                break;
            case 1:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as2Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = true;
                break;
            case 2:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as3Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = false;
                break;
        }

        this.attackSpeedLvl = newLvl;

        switch (towerLvl) {
            case 0:
                switch (attackSpeedLvl) {
                    case 0:
                        setASUpgradeCostText(Shop.instance.upgradeT1As1price);
                        break;
                    case 1:
                        setASUpgradeCostText(Shop.instance.upgradeT1As2price);
                        break;
                    case 2:
                        setASUpgradeCostText(Shop.instance.upgradeT1As3price);
                        break;
                }
                break;
            case 1:
                switch (attackSpeedLvl)
                {
                    case 0:
                        setASUpgradeCostText(Shop.instance.upgradeT2As1price);
                        break;
                    case 1:
                        setASUpgradeCostText(Shop.instance.upgradeT2As2price);
                        break;
                    case 2:
                        setASUpgradeCostText(Shop.instance.upgradeT2As3price);
                        break;
                }
                break;
            case 2:
                switch (attackSpeedLvl)
                {
                    case 0:
                        setASUpgradeCostText(Shop.instance.upgradeT3As1price);
                        break;
                    case 1:
                        setASUpgradeCostText(Shop.instance.upgradeT3As2price);
                        break;
                    case 2:
                        setASUpgradeCostText(Shop.instance.upgradeT3As3price);
                        break;
                }
                break;
        }
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + tower.sell_cost;
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + tower.sell_cost;
    }

    public void setAttackDamage(int newLvl)
    {
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad1Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = true;
                break;
            case 1:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad2Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = true;
                break;
            case 2:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad3Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = false;
                break;
        }

        this.attackDmgLvl = newLvl;
        switch (towerLvl)
        {
            case 0:
                switch (attackDmgLvl)
                {
                    case 0:
                        setADUpgradeCostText(Shop.instance.upgradeT1Ad1price);
                        break;
                    case 1:
                        setADUpgradeCostText(Shop.instance.upgradeT1Ad2price);
                        break;
                    case 2:
                        setADUpgradeCostText(Shop.instance.upgradeT1Ad3price);
                        break;
                }
                break;
            case 1:
                switch (attackDmgLvl)
                {
                    case 0:
                        setADUpgradeCostText(Shop.instance.upgradeT2Ad1price);
                        break;
                    case 1:
                        setADUpgradeCostText(Shop.instance.upgradeT2Ad2price);
                        break;
                    case 2:
                        setADUpgradeCostText(Shop.instance.upgradeT2Ad3price);
                        break;
                }
                break;
            case 2:
                switch (attackSpeedLvl)
                {
                    case 0:
                        setADUpgradeCostText(Shop.instance.upgradeT3Ad1price);
                        break;
                    case 1:
                        setADUpgradeCostText(Shop.instance.upgradeT3Ad2price);
                        break;
                    case 2:
                        setADUpgradeCostText(Shop.instance.upgradeT3Ad3price);
                        break;
                }
                break;
        }
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + tower.sell_cost;
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + tower.sell_cost;
    }

    public void setASUpgradeCostText(int price) {
        upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").GetComponent<Text>().text = "" + price;
        upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").GetComponent<Text>().text = "" + price;
    }

    public void setADUpgradeCostText(int price) {
        upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").GetComponent<Text>().text = "" + price;
        upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").GetComponent<Text>().text = "" + price;
    }

    // Use this for initialization
    void Start () {
        anim = upgradeWheel.GetComponent<Animator>();
        gameObject.SetActive(false);
        clearButtons();
	}

    public void SellBuildingOnClick() {
        BuildManager.instance.SellSelectedBuilding();
    }

    public void UpgradeLvlOnClick() {
        BuildManager.instance.UpgradeSelectedBuild();
    }

    public void UpgradeAttributeOnClick(int att) {
        switch (att) {
            case 0:
                switch (attackSpeedLvl) {
                    case 0:
                        BuildManager.instance.upgradeFR1();
                        break;
                    case 1:
                        BuildManager.instance.upgradeFR2();
                        break;
                    case 2:
                        BuildManager.instance.upgradeFR3();
                        break;
                }
                break;
            case 1:
                switch (attackDmgLvl)
                {
                    case 0:
                        BuildManager.instance.upgradeAP1();
                        break;
                    case 1:
                        BuildManager.instance.upgradeAP2();
                        break;
                    case 2:
                        BuildManager.instance.upgradeAP3();
                        break;
                }
                break;
        }
    }

    public void SetSpecializationOnClick(int spec) {
        switch (spec)
        {
            case 0:
                BuildManager.instance.UpgradeSlow();
                break;
            case 1:
                BuildManager.instance.UpgradeSelectedBuild();
                break;
            case 2:
                BuildManager.instance.UpgradeTesla();
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (anim)
        {
            anim.SetInteger("AttackSpeedLvl", attackSpeedLvl);
            anim.SetInteger("AttackDmgLvl", attackDmgLvl);
            anim.SetInteger("TowerLvl", towerLvl);
            anim.SetInteger("Specialization", specialization);
        }
    }
}
