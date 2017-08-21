using UnityEngine;

public class Shop : MonoBehaviour {

    public UnitBlueprint standardUnit;
    public UnitBlueprint missileLauncher;
    BuildManager buildManager;

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
