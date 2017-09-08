﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public GameObject optionsObject;
    public GameObject shopObject;
    private UnitBlueprint unitToBuild;
    private UnitBlueprint selectedUnit;
    private Vector2 selectedPosition;
    private GameObject selectedGameObject;
    private GridMouse gridMouse;

    void Awake()
    {
        if (instance != null) //if instance has been set before 
        {
            Debug.LogError("More than one BuildManager in scene !");
            return;
        }
        instance = this;
    }
    // Use this for initialization
    void Start () {
        gridMouse = GridMouse.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //toggle UI
            shopObject.SetActive(!shopObject.activeSelf);
        }
    }

    public UnitBlueprint getSelectedUnit()
    {
        return selectedUnit;
    }
    public void DeselectSelectedUnit()
    {
        selectedUnit = null;
    }

    public Vector2 getSelectedPosition()
    {
        return selectedPosition;
    }

    public UnitBlueprint getUnitToBuild()
    {
        return unitToBuild;
    }

    public bool CanBuild { get { return unitToBuild != null; } }


    public GameObject BuildPreviewOn(GameObject temporaryInstance,Vector3 position)
    {
        if (temporaryInstance != null && unitToBuild != null)
        {
            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 newPosition;
            if (unitToBuild == Shop.instance.missileLauncher)
                newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
            else
                newPosition = position;
            temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, newPosition, unitToBuild.prefab.transform.rotation);
            //temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
            MonoBehaviour[] list = temporaryInstance.GetComponents<MonoBehaviour>();
            for (int i = 0; i < list.Length; i++)
            {
                Destroy(list[i]);
            }
            temporaryInstance.layer = LayerMask.NameToLayer("Ignore Raycast");
            temporaryInstance.GetComponent<Renderer>().material.color = Color.green;
        }
        return temporaryInstance;
    }

    public void BuildUnitOn(ref List<GameObject> tempList,int index, Vector3 position)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.AddMoney(-1* unitToBuild.cost);

        Vector3 newPosition;
        if (unitToBuild == Shop.instance.missileLauncher)
            newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
        else
            newPosition = position;
        tempList[index] = Instantiate(unitToBuild.prefab, newPosition, Quaternion.Euler(gridMouse.getPreviewRotation()));
        //tempList[index] = Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
        tempList[index].GetComponent<BuildableController>().setArrayListPosition(index);
        tempList[index].GetComponent<BuildableController>().setUnitBlueprint(getUnitToBuild());
        //Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }


    //public void SelectBuilding(UnitBlueprint unit, Vector2 position)
    public void SelectBuilding(UnitBlueprint unit, GameObject gameObject)
    {
        unitToBuild = null;
        //if (selectedUnit == unit)
        selectedUnit = unit;
        selectedGameObject = gameObject;
        selectedPosition = gameObject.transform.position;        
    }
    public void SelectBuilding(int indexOfSelectedObject)
    {
        unitToBuild = null;

        GridMouse gridMouse = GridMouse.instance;
        BuildableController buildable =
                gridMouse.ListOfGameObjects[indexOfSelectedObject].GetComponent<BuildableController>();
        
        //if (selectedUnit == unit)
        selectedUnit = buildable.getUnitBlueprint();
        selectedGameObject = gridMouse.ListOfGameObjects[indexOfSelectedObject];
        //selectedPosition = position;
    }
    public GameObject getSelectedGameObject() {
        return selectedGameObject;
    }
    public void ShowOptions()
    {
        optionsObject.SetActive(true);
        GameObject.Find("ButtonUpgradeText").GetComponent<Text>().text = "Upgrade $" + selectedUnit.upgrade_cost;
        GameObject.Find("ButtonSellText").GetComponent<Text>().text = "Sell $" + selectedUnit.sell_cost;
    }
    public void HideOptions()
    {
        optionsObject.SetActive(false);
    }

    public void SelectStructure(Structure structure)
    {
        unitToBuild = null;

    }
    /*
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
        }
        selectedNode = node;
        unitToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    */
    public void SelectUnitToBuild(UnitBlueprint unit)
    {
        unitToBuild = unit;
        //DeselectNode();
    }
    public void DeselectUnitToBuild()
    {
        unitToBuild = null;
    }

}
