﻿using UnityEngine;

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

    public void coolRemoveAt(int position)
    {
        GameObject lastObject = gridMouse.ListOfGameObjects[gridMouse.ListOfGameObjects.Count - 1];
        lastObject.GetComponent<BuildableController>().setArrayListPosition(position);
        gridMouse.ListOfGameObjects[position] = lastObject;
        gridMouse.ListOfGameObjects.RemoveAt(gridMouse.ListOfGameObjects.Count - 1);
       
    }
    public void Sell() {

        UnitBlueprint SelectedUnit = buildManager.getSelectedUnit();
        if (SelectedUnit != null)
        {
            PlayerStats.AddMoney(SelectedUnit.sell_cost);
            Debug.Log("Sold for " + SelectedUnit.sell_cost + ". Current Money: " + PlayerStats.Money);
            string name = buildManager.getSelectedGameObject().name;
            BuildableController buildable = 
                buildManager.getSelectedGameObject().GetComponent<BuildableController>();
            //gridMouse.ListOfGameObjects.RemoveAt(buildable.getArrayListPosition());
            coolRemoveAt(buildable.getArrayListPosition());
            //------------------------------------------------------------------------
            //before Destroying the object, remove it's entry from the matrix
            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 SelectedPosition = buildManager.getSelectedGameObject().transform.position;
            int x = Mathf.FloorToInt(SelectedPosition.x -0.5f + gridSize.x / 2);
            int z = Mathf.FloorToInt(SelectedPosition.z -0.5f + gridSize.y / 2);
            Vector3 position = gridMouse.CoordToPosition(x, z);
            gridMouse.propertiesMatrix[x, z].unit = null;
            if (SelectedUnit == Shop.instance.missileLauncher)
            {
                gridMouse.propertiesMatrix[x + 1, z + 1].unit = null;
                gridMouse.propertiesMatrix[x, z + 1].unit = null;
                gridMouse.propertiesMatrix[x + 1, z].unit = null;
            }
            //------------------------------------------------------------------------

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
                buildManager.DeselectSelectedUnit();
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

            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 SelectedPosition = buildManager.getSelectedGameObject().transform.position;
            int x = Mathf.FloorToInt(SelectedPosition.x + gridSize.x / 2);
            int z = Mathf.FloorToInt(SelectedPosition.z + gridSize.y / 2);
            Vector3 position = gridMouse.CoordToPosition(x, z);
            //destroys the current object
            Destroy(gridMouse.propertiesMatrix[x, z].builtGameObject);
            int added_index = gridMouse.buildUnitAndAddItToTheList(position);
            //gridMouse.propertiesMatrix[x, y] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
        }
        
    }
	
}
