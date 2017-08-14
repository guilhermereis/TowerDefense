using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardUnit()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.setUnitToBuild(buildManager.standardUnitPrefab);
    }

    public void PurchaseSecondaryUnit()
    {
        Debug.Log("Secondary Turret Purchased");
        buildManager.setUnitToBuild(buildManager.secondaryUnitPrefab);
    }

}
