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
            Vector2 SelectedPosition = buildManager.getSelectedPosition();
            PlayerStats.Money += SelectedUnit.sell_cost;
            Debug.Log("Sold for " + SelectedUnit.sell_cost + ". Current Money: " + PlayerStats.Money);
            int x = Mathf.FloorToInt(SelectedPosition.x);
            int y = Mathf.FloorToInt(SelectedPosition.y);
            Destroy(gridMouse.propertiesMatrix[x, y].builtGameObject);
            //Debug.Log("AQUI: "+gridMouse.propertiesMatrix[x, y].builtGameObject);
            Debug.Log("Vendeu a posição: " + x + ", " + y);
        }
    }
    public void Upgrade() {
        if (buildManager.getSelectedUnit() != null) {
            if (buildManager.getSelectedUnit().name == "Tower") {
                buildManager.SelectUnitToBuild(shop.towerLevel2);
                BuildTheNextLevelStructure();
                buildManager.DeselectUnitToBuild();
                Debug.Log("Upgraded unit: " + "Tower");
            }

        }
    }
    void BuildTheNextLevelStructure() {
        if (buildManager.getUnitToBuild() != null)
        {
            Vector2 SelectedPosition = buildManager.getSelectedPosition();
            int x = Mathf.FloorToInt(SelectedPosition.x);
            int y = Mathf.FloorToInt(SelectedPosition.y);
            //destroys the current object
            Destroy(gridMouse.propertiesMatrix[x, y].builtGameObject);
            Vector3 position3D = gridMouse.CoordToPosition(x, y);
            gridMouse.matrixOfGameObjects[x, y] = new GameObject();
            //builds the upgraded version
            buildManager.BuildUnitOn(ref gridMouse.matrixOfGameObjects[x, y], position3D);
            gridMouse.propertiesMatrix[x, y] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.matrixOfGameObjects[x, y], "Obstacle");
            //Debug.Log("Construiu na posição " + x + ", " + z);
        }
    }
	
}
