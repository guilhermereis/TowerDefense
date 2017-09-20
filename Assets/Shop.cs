using UnityEngine;
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
    BuildManager buildManager;

    public Color CoinEnabledColor;
    public Color CoinTextEnabledColor;

    public Color CoinDisabledColor;
    public Color CoinTextDisabledColor;

    private bool canBuildPrimary = true;
    private bool canBuildSecondary = true;
    private bool updateGui = true;

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
