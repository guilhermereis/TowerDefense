﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public GameObject optionsObject;
    public GameObject shopObject;
    private UnitBlueprint unitToBuild;
    private UnitBlueprint selectedUnit;
    private Vector2 selectedPosition;

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

    public Vector2 getSelectedPosition()
    {
        return selectedPosition;
    }

    public UnitBlueprint getUnitToBuild()
    {
        return unitToBuild;
    }

    public bool CanBuild { get { return unitToBuild != null; } }


    public void BuildPreviewOn(ref GameObject temporaryInstance,Vector3 position)
    {
        if (temporaryInstance != null && unitToBuild != null)
        {
            temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
            MonoBehaviour[] list = temporaryInstance.GetComponents<MonoBehaviour>();
            for (int i = 0; i < list.Length; i++)
            {
                Destroy(list[i]);
            }
            temporaryInstance.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    public void BuildUnitOn(ref GameObject temp, Vector3 position)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= unitToBuild.cost;
        
        temp = Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
        Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }


    public void SelectBuilding(UnitBlueprint unit,Vector2 position)
    {
        unitToBuild = null;
        //if (selectedUnit == unit)
        selectedUnit = unit;
        selectedPosition = position;
    }
    public void ShowOptions()
    {
        optionsObject.SetActive(true);
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
