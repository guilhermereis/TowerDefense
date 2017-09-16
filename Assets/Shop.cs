using UnityEngine;

public class Shop : MonoBehaviour {

    public static Shop instance;
    public UnitBlueprint standardUnit;
    public UnitBlueprint missileLauncher;
    public UnitBlueprint towerLevel2;
    public UnitBlueprint towerLevel3;
    public UnitBlueprint towerSlow;
    public UnitBlueprint towerTesla;
    BuildManager buildManager;

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

    public void SelectStandardUnit()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectUnitToBuild(standardUnit);
    }

    public void SelectSecondaryUnit()
    {
        Debug.Log("Secondary Turret Selected");
        buildManager.SelectUnitToBuild(missileLauncher);
    }

}
