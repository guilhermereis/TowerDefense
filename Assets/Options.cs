using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    BuildManager buildManager;
    GridMouse gridMouse;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;
        gridMouse = GridMouse.instance;
    }

    public void Sell() {
        UnitBlueprint SelectedUnit = buildManager.getSelectedUnit();
        Vector2 SelectedPosition = buildManager.getSelectedPosition();
        PlayerStats.Money += SelectedUnit.sell_cost;
        Debug.Log("Sold for " + SelectedUnit.sell_cost + ". Current Money: " + PlayerStats.Money);
        int x = Mathf.FloorToInt(SelectedPosition.x);
        int y = Mathf.FloorToInt(SelectedPosition.y);
        Destroy(gridMouse.propertiesMatrix[x,y].builtGameObject);
        //Debug.Log("AQUI: "+gridMouse.propertiesMatrix[x, y].builtGameObject);
        Debug.Log("Vendeu a posição: "+x+", "+y);
    }
    public void Upgrade() {

    }

	
}
