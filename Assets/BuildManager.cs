using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public GameObject optionsObject;
    public GameObject optionsSlowObject;
    public GameObject extraOptionsObject;
    public GameObject shopObject;
    public GameObject sphere;
    private UnitBlueprint unitToBuild;
    private UnitBlueprint selectedUnit;
    private Vector2 selectedPosition;
    private GameObject selectedGameObject;
    private GameObject LastSelectedGameObject;
    private GridMouse gridMouse;
    public GameObject bottomBar;
    private BottomInfoBarBehaviour bottomBarBehaviour;

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
        bottomBarBehaviour = (BottomInfoBarBehaviour)bottomBar.GetComponentInChildren<BottomInfoBarBehaviour>();
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
            {
                newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
            }
            else
            {
                newPosition = position;
            }
            temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, newPosition, unitToBuild.prefab.transform.rotation);
            //temporaryInstance.transform.Find("Sphere").gameObject.SetActive(true);
            temporaryInstance.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = true;

            //sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //temporaryInstance.transform.parent = sphere.transform;

            //temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
            MonoBehaviour[] list = temporaryInstance.GetComponents<MonoBehaviour>();
            for (int i = 0; i < list.Length; i++)
            {
                Destroy(list[i]);
            }
            temporaryInstance.layer = LayerMask.NameToLayer("Ignore Raycast");
            //temporaryInstance.transform.Find("Sphere").gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            foreach (Transform child in temporaryInstance.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); 
            }
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
        tempList[index].transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
        if(tempList[index].GetComponent<TowerController>() != null)
        {
            tempList[index].GetComponent<TowerController>().BuildEffect();
        }
        //Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }
    public void BuildUnitOn(ref List<GameObject> tempList, int index, Vector3 position, Quaternion rotation)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.AddMoney(-1 * unitToBuild.cost);

        Vector3 newPosition;
        if (unitToBuild == Shop.instance.missileLauncher)
            newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
        else
            newPosition = position;
        tempList[index] = Instantiate(unitToBuild.prefab, newPosition, rotation);
        //tempList[index] = Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
        tempList[index].GetComponent<BuildableController>().setArrayListPosition(index);
        tempList[index].GetComponent<BuildableController>().setUnitBlueprint(getUnitToBuild());
        tempList[index].transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }

    //public void SelectBuilding(UnitBlueprint unit, Vector2 position)
    public void SelectBuilding(UnitBlueprint unit, GameObject gameObject)
    {
        unitToBuild = null;
        if (LastSelectedGameObject != null)
            LastSelectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
        //if (selectedUnit == unit)
        selectedUnit = unit;
        selectedGameObject = gameObject;
        selectedPosition = gameObject.transform.position;
        selectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = true;
        LastSelectedGameObject = selectedGameObject;
    }
    public void SelectBuilding(int indexOfSelectedObject)
    {
        unitToBuild = null;
        if (LastSelectedGameObject != null)
            LastSelectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;

        GridMouse gridMouse = GridMouse.instance;
        BuildableController buildable =
                gridMouse.ListOfGameObjects[indexOfSelectedObject].GetComponent<BuildableController>();
        
        //if (selectedUnit == unit)
        selectedUnit = buildable.getUnitBlueprint();
        selectedGameObject = gridMouse.ListOfGameObjects[indexOfSelectedObject];
        selectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = true;
        //selectedPosition = position;
        LastSelectedGameObject = selectedGameObject;
    }
    public GameObject getSelectedGameObject() {
        return selectedGameObject;
    }
    public void ShowOptions()
    {
        extraOptionsObject.SetActive(true);
        if (selectedGameObject.name == "PrefabArcherTower2(Clone)")
        {
            optionsSlowObject.SetActive(true);
        }
        else
        {
            optionsObject.SetActive(true);
        } 
        GameObject.Find("ButtonUpgradeText").GetComponent<Text>().text = "Upgrade $" + selectedUnit.upgrade_cost;
        GameObject.Find("ButtonSellText").GetComponent<Text>().text = "Sell $" + selectedUnit.sell_cost;
    }
    public void HideOptions()
    {
        extraOptionsObject.SetActive(false);
        optionsObject.SetActive(false);
        optionsSlowObject.SetActive(false);
        selectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
        
    }

    public void hideBottomBar() {
        bottomBarBehaviour.setSelectionState(0);
    }

    public void showBottomBar() {
        bottomBarBehaviour.setSelectionState(1);
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
