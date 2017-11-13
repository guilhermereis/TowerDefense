using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWheelController : MonoBehaviour {

    public GameObject tower;
    private BuildableController buildable;
    public GameObject upgradeWheel;
    public GameObject sellSound;
    public GameObject openSound;
    
    private Animator anim;
    private int attackSpeedLvl = 0;
    private int attackDmgLvl = 0;
    private int towerLvl = 0;
    private int specialization = 0;

    public Color NotEnoughGoldTextColor;
    public Color NotEnoughGoldCoinColor;
    public Color NotEnoughGoldButtonColor;

    private Color DefaultCoinColor;
    private Color DefaultTextColor;
    private Color DefaultButtonColor;

    //Sprites
    public Sprite as1Sprite;
    public Sprite as2Sprite;
    public Sprite as3Sprite;
    public Sprite asMaxSprite;

    public Sprite ad1Sprite;
    public Sprite ad2Sprite;
    public Sprite ad3Sprite;
    public Sprite adMaxSprite;

    public bool isActive = true;
    public bool upgradeButtonsEnabled = true;
    public bool isMine = false;
    // Use this for initialization

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
        isActive = false;
        isMine = false;
        clearButtons();
        DefaultCoinColor = upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Coin").GetComponent<Image>().color;
        DefaultTextColor = upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").GetComponent<Text>().color;
        DefaultButtonColor = upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().color;
    }

    public void clearButtons() {
        upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerTesla").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeTowerArcher").gameObject.SetActive(false);
        upgradeWheel.transform.Find("UpgradeCampLevel").gameObject.SetActive(false);
    }

    public void setMineSellPrice()
    {
        buildable = tower.GetComponent<BuildableController>();
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
        Debug.Log("5 SET SELL COST TO " + buildable.GetSellCostWithInterest());
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
    }
    public void setTowerLvl(int newLvl) {
        clearButtons();
        this.towerLvl = newLvl;
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeTowerLevel").transform.Find("Cost").GetComponent<Text>().text = "" + Shop.instance.towerLevel1.upgrade_cost;
                upgradeWheel.transform.Find("UpgradeTowerLevel").transform.Find("CostShadow").GetComponent<Text>().text = "" + Shop.instance.towerLevel1.upgrade_cost;
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
        buildable = tower.GetComponent<BuildableController>();
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
        Debug.Log("1 SET SELL COST TO " + buildable.GetSellCostWithInterest());
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
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

        buildable = tower.GetComponent<BuildableController>();
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
        Debug.Log("2 SET SELL COST TO " + buildable.GetSellCostWithInterest());
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
    }

    public void setAttackSpeedLvl(int newLvl) {
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as1Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 1:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as2Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 2:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = as3Sprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 3:
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Image>().overrideSprite = asMaxSprite;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").Find("Stripe").GetComponent<Image>().enabled = false;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").GetComponent<Button>().interactable = false;
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Coin").gameObject.SetActive(false);
                break;
        }

        this.attackSpeedLvl = newLvl;

        switch (towerLvl)
        {
            case 0:
                switch (attackSpeedLvl)
                {
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
        buildable = tower.GetComponent<BuildableController>();
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
        Debug.Log("3 SET SELL COST TO " + buildable.GetSellCostWithInterest());
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
    }

    public void setAttackDamage(int newLvl)
    {
        switch (newLvl)
        {
            case 0:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad1Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 1:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad2Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 2:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = ad3Sprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").Find("Stripe").GetComponent<Image>().enabled = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = true;
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").gameObject.SetActive(true);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Coin").gameObject.SetActive(true);
                break;
            case 3:
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Image>().overrideSprite = adMaxSprite;
                upgradeWheel.transform.Find("UpgradeAttackDamage").Find("Stripe").GetComponent<Image>().enabled = false;
                upgradeWheel.transform.Find("UpgradeAttackDamage").GetComponent<Button>().interactable = false;
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").gameObject.SetActive(false);
                upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Coin").gameObject.SetActive(false);
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
                switch (attackDmgLvl)
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
        buildable = tower.GetComponent<BuildableController>();
        upgradeWheel.transform.Find("SellTower").transform.Find("Cost").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
        Debug.Log("4 SET SELL COST TO " + buildable.GetSellCostWithInterest());
        upgradeWheel.transform.Find("SellTower").transform.Find("CostShadow").GetComponent<Text>().text = "" + buildable.GetSellCostWithInterest();
    }

    public void setASUpgradeCostText(int price) {
        upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("Cost").GetComponent<Text>().text = "" + price;
        upgradeWheel.transform.Find("UpgradeAttackSpeed").transform.Find("CostShadow").GetComponent<Text>().text = "" + price;
    }

    public void setADUpgradeCostText(int price) {
        upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("Cost").GetComponent<Text>().text = "" + price;
        upgradeWheel.transform.Find("UpgradeAttackDamage").transform.Find("CostShadow").GetComponent<Text>().text = "" + price;
    }

    public void SellBuildingOnClick() {
        if (upgradeButtonsEnabled)
        {
            isActive = false;
            SoundToPlay.PlaySfx(sellSound);
            BuildManager.instance.SellSelectedBuilding();
        }
    }

    public void GetMoneyOnClick()
    {
        if (upgradeButtonsEnabled)
        {
            isActive = false;

        }
    }

    public void UpgradeLvlOnClick() {
        if (upgradeButtonsEnabled)
        {
            BuildManager.instance.UpgradeSelectedBuild();
            Debug.Log("CLICKED ONE TIME");
        }
    }

    public void UpgradeAttributeOnClick(int att) {
        if (upgradeButtonsEnabled) {
            switch (att)
            {
                case 0:
                    switch (attackSpeedLvl)
                    {
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
    }

    public void SetSpecializationOnClick(int spec) {
        if (upgradeButtonsEnabled) {
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
    }

    // Update is called once per frame
    void Update () {

        if (anim)
        {
            if (!isMine)
            {
                anim.SetInteger("AttackSpeedLvl", attackSpeedLvl);
                anim.SetInteger("AttackDmgLvl", attackDmgLvl);
                anim.SetInteger("TowerLvl", towerLvl);
                anim.SetInteger("Specialization", specialization);
            }
            else {
                anim.SetInteger("Specialization", 2);
            }
            
        }
        if (BuildManager.instance.getSelectedGameObject()) {
            if (isMine)
            {
                transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f) + BuildManager.instance.getSelectedGameObject().transform.position);
            }
            else {
                transform.position = Camera.main.WorldToScreenPoint(new Vector3(0f, 0.5f, 0f) + BuildManager.instance.getSelectedGameObject().transform.position);
            }
            
        }

        gameObject.SetActive(isActive);

        //Check if have enough money for updates

        GameObject upgradeTesla = upgradeWheel.transform.Find("UpgradeTowerTesla").gameObject;
        GameObject upgradeArcher = upgradeWheel.transform.Find("UpgradeTowerArcher").gameObject;
        GameObject upgradeIcer = upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject;
        GameObject upgradeTower = upgradeWheel.transform.Find("UpgradeTowerLevel").gameObject;
        GameObject upgradeAS = upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject;
        GameObject upgradeAD = upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject;

        CheckForEnoughMoney(upgradeTesla);
        CheckForEnoughMoney(upgradeArcher);
        CheckForEnoughMoney(upgradeIcer);
        CheckForEnoughMoney(upgradeTower);
        CheckForEnoughMoney(upgradeAS);
        CheckForEnoughMoney(upgradeAD);

        if (anim.gameObject.activeSelf) {
            if (anim.GetBool("DoneClosing"))
            {
                onWheelClosed();
                anim.SetBool("DoneClosing", false);
            }
        }
    }

    public void CheckForEnoughMoney(GameObject upgradeButton) {
        int money = PlayerStats.Money;
        if (money < int.Parse(upgradeButton.transform.Find("Cost").GetComponent<Text>().text))
        {
            upgradeButton.transform.Find("Cost").GetComponent<Text>().color = NotEnoughGoldTextColor;
            upgradeButton.transform.Find("Coin").GetComponent<Image>().color = NotEnoughGoldCoinColor;
            upgradeButton.GetComponent<Image>().color = NotEnoughGoldButtonColor;
            upgradeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            upgradeButton.transform.Find("Cost").GetComponent<Text>().color = DefaultTextColor;
            upgradeButton.transform.Find("Coin").GetComponent<Image>().color = DefaultCoinColor;
            upgradeButton.GetComponent<Image>().color = DefaultButtonColor;
            upgradeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void openWheel() {
        anim.SetBool("Open", true);
        isActive = true;
        SoundToPlay.PlaySfx(openSound);
    }

    public void onWheelOpened() {
        enableButtons();
    }

    public void closeWheel() {
        if(anim.gameObject.activeSelf)
            anim.SetBool("Open", false);
    }

    public void onWheelClosed() {
        isActive = false;
        disableButtons();
    }

    public void enableButtons() {
        upgradeButtonsEnabled = true;
    }
    public void disableButtons()
    {
        upgradeButtonsEnabled = false;
    }
}
