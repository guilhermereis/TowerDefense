using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public NodeUI nodeUI;
    public StructureUI structureUI;
    private UnitBlueprint unitToBuild;
    private Node selectedNode;
    private UnitBlueprint selectedUnit;
    private Vector3 selectedSquare;
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
        if (temporaryInstance !=null && unitToBuild != null)
            temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, position, Quaternion.identity);        
    }
    public void BuildPreviewOn(Node node)
    {
        GameObject preview = (GameObject)Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.preview = preview;
        
    }

    public void BuildUnitOn(ref GameObject temp, Vector3 position)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= unitToBuild.cost;
        
        temp = Instantiate(unitToBuild.prefab, position, Quaternion.identity);
        Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }


    public void SelectBuilding(UnitBlueprint unit,Vector2 position)
    {
        unitToBuild = null;
        selectedUnit = unit;
        selectedPosition = position;
    }


    public void SelectStructure(Structure structure)
    {
        unitToBuild = null;


        structureUI.SetTarget(structure);
    }

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
    public void SelectUnitToBuild(UnitBlueprint unit)
    {
        unitToBuild = unit;
        //DeselectNode();
    }

}
