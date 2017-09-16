using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSlow : Options {

    public void UpgradeSlow()
    {
        Debug.Log("Going to upgrade " + buildManager.getSelectedUnit().name + "!");
        if (buildManager.getSelectedUnit() != null)
        {
            if (PlayerStats.Money - Shop.instance.towerSlow.upgrade_cost >= 0)
            {
                PlayerStats.AddMoney(-1 * Shop.instance.towerSlow.upgrade_cost);
                buildManager.SelectUnitToBuild(shop.towerSlow);
                BuildTheNextLevelStructure();
                buildManager.DeselectUnitToBuild();
                buildManager.DeselectSelectedUnit();
                buildManager.HideOptions();
                Debug.Log("Upgraded unit: " + "Tower");
            }
            else
            {
                Debug.Log("You don't have enough money to upgrade this unit.");
            }
            
        }
    }
    public void UpgradeTesla()
    {
        Debug.Log("Going to upgrade " + buildManager.getSelectedUnit().name + "!");
        if (buildManager.getSelectedUnit() != null)
        {
            if (PlayerStats.Money - Shop.instance.towerTesla.upgrade_cost >= 0)
            {
                PlayerStats.AddMoney(-1 * Shop.instance.towerTesla.upgrade_cost);
                buildManager.SelectUnitToBuild(shop.towerTesla);
                BuildTheNextLevelStructure();
                buildManager.DeselectUnitToBuild();
                buildManager.DeselectSelectedUnit();
                buildManager.HideOptions();
                Debug.Log("Upgraded unit: " + "Tower");
            }
            else
            {
                Debug.Log("You don't have enough money to upgrade this unit.");
            }

        }
    }
}
