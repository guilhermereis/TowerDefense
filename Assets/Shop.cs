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

    private bool canBuildPrimary = true;
    private bool canBuildSecondary = true;

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
    }

    private void Update()
    {
        if (PlayerStats.Money < standardUnit.cost)
        {
            standardUnitButton.interactable = false;
            canBuildPrimary = false;
        }else {
            standardUnitButton.interactable = true;
            canBuildPrimary = true;
        }

        if (PlayerStats.Money < missileLauncher.cost)
        {
            secondaryUnitButton.interactable = false;
            canBuildSecondary = false;
        }else {
            secondaryUnitButton.interactable = true;
            canBuildSecondary = true;
        }
    }

    public void SelectStandardUnit(){
        if (canBuildPrimary){
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
}
