using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    BuildManager buildManager;
    GridMouse gridMouse;
    Shop shop;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;
        gridMouse = GridMouse.instance;
        shop = Shop.instance;
    }

    public void Sell() {

        UnitBlueprint SelectedUnit = buildManager.getSelectedUnit();
        if (SelectedUnit != null)
        {
            PlayerStats.Money += SelectedUnit.sell_cost;
            Debug.Log("Sold for " + SelectedUnit.sell_cost + ". Current Money: " + PlayerStats.Money);
            string name = buildManager.getSelectedGameObject().name;
            BuildableController buildable = 
                buildManager.getSelectedGameObject().GetComponent<BuildableController>();
            gridMouse.ListOfGameObjects.RemoveAt(buildable.getArrayListPosition());
            Destroy(buildManager.getSelectedGameObject());
            //Debug.Log("AQUI: "+gridMouse.propertiesMatrix[x, y].builtGameObject);
            Debug.Log("Vendeu "+name);
        }
    }
    public void Upgrade() {
        if (buildManager.getSelectedUnit() != null) {
            if (buildManager.getSelectedUnit().name == "Tower")
            {
                buildManager.SelectUnitToBuild(shop.towerLevel2);
                BuildTheNextLevelStructure();
                buildManager.DeselectUnitToBuild();
                Debug.Log("Upgraded unit: " + "Tower");
            }
            else
            {
                Debug.Log("Unit can't be upgraded any further !");
            }

        }
    }
    void BuildTheNextLevelStructure() {
        
        if (buildManager.getUnitToBuild() != null)
        {
            Vector2 SelectedPosition = buildManager.getSelectedPosition();
            int x = Mathf.FloorToInt(SelectedPosition.x);
            int y = Mathf.FloorToInt(SelectedPosition.y);
            Vector3 position = gridMouse.CoordToPosition(x, y);
            //destroys the current object
            Destroy(gridMouse.propertiesMatrix[x, y].builtGameObject);
            int added_index = gridMouse.buildUnitAndAddItToTheList(position);
            //gridMouse.propertiesMatrix[x, y] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
        }
        
    }
	
}
