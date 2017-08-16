using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public UnitBlueprint standardUnitPrefab;
    public UnitBlueprint secondaryUnitPrefab;
    public NodeUI nodeUI;
    public StructureUI structureUI;

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

    private UnitBlueprint unitToBuild;
    private Node selectedNode;

    public UnitBlueprint getUnitToBuild()
    {
        return unitToBuild;
    }

    public bool CanBuild { get { return unitToBuild != null; } }

    public void BuildPreviewOn(Node node)
    {
        GameObject preview = (GameObject)Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.preview = preview;
        
    }

    public void BuildUnitOn(Node node)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= unitToBuild.cost;


        GameObject unit = (GameObject) Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.SetUnit(unit);
        

        Debug.Log("Unit built ! Money left: " +PlayerStats.Money);
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
        DeselectNode();

    }

}
