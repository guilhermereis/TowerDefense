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
    public GameObject upgradeWheel;
    private UpgradeWheelController upgradeWheelController;

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
        if(bottomBar)
            bottomBarBehaviour = (BottomInfoBarBehaviour)bottomBar.GetComponentInChildren<BottomInfoBarBehaviour>();
        if (upgradeWheel)
            upgradeWheelController = upgradeWheel.GetComponent<UpgradeWheelController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //toggle UI
            shopObject.SetActive(!shopObject.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            PlayerStats.AddMoney(100);
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

    public void BuildUnitOn(ref List<GameObject> tempList,int index, Vector3 position, bool upgraded)
    {
        if (upgraded == false)
        {
            if (PlayerStats.Money < unitToBuild.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }
            PlayerStats.AddMoney(-1 * unitToBuild.cost);
        }

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
    public void BuildUnitOn(ref List<GameObject> tempList, int index, Vector3 position, Quaternion rotation, bool upgraded = false)
    {
        if (upgraded == false)
        {
            if (PlayerStats.Money < unitToBuild.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }
            PlayerStats.AddMoney(-1 * unitToBuild.cost);
        }
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
        Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
    }

    public void OnUnitUpgrade() {
        showBottomBar();
        showUpgradeWheel();
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
        showBottomBar();
        showUpgradeWheel();
    }
    public void HideOptions()
    {
        hideUpgradeWheel();
        hideBottomBar();
        if (selectedGameObject){
            MeshRenderer renderer = selectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>();
            if (renderer)
                renderer.enabled = false;
        }
        
    }

    public void hideBottomBar() {
        bottomBarBehaviour.setSelectionState(0);
    }

    public void showBottomBar() {
        bottomBarBehaviour.setSelectedUnit(selectedGameObject);
        bottomBarBehaviour.setSelectionState(1);
    }

    public void showUpgradeWheel() {
        if (upgradeWheelController)
        {
            upgradeWheelController.tower = selectedUnit;
            TowerController towerController = selectedGameObject.GetComponent<TowerController>();
            if (towerController)
            {
                switch (towerController.name)
                {
                    case "PrefabArcherTower1(Clone)":
                        upgradeWheelController.setTowerLvl(0);
                        break;
                    case "PrefabArcherTower2(Clone)":
                        upgradeWheelController.setTowerLvl(1);
                        break;
                    case "PrefabArcherTower3(Clone)":
                        upgradeWheelController.setTowerLvl(2);
                        upgradeWheelController.setSpecialization(1);
                        break;
                    case "PrefabArcherTower2Slow(Clone)":
                        upgradeWheelController.setTowerLvl(1);
                        upgradeWheelController.setSpecialization(0);
                        break;
                    case "PrefabArcherTower2Tesla(Clone)":
                        upgradeWheelController.setTowerLvl(1);
                        upgradeWheelController.setSpecialization(2);
                        break;
                }
                upgradeWheelController.setAttackDamage((int)towerController.getAttackPowerLVL());
                upgradeWheelController.setAttackSpeedLvl((int)towerController.getFireRateLVL());
                upgradeWheelController.isActive = true;
                upgradeWheel.SetActive(true);
                upgradeWheelController.openWheel();
            }
        }
    }

    public void hideUpgradeWheel() {
        if (upgradeWheelController)
        {
            upgradeWheelController.closeWheel();
        }
    }

    public void OnUpgradeWheelClosed() {
        upgradeWheel.SetActive(false);
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


    public void SellSelectedBuilding() {
        UnitBlueprint SelectedUnit = getSelectedUnit();
        if (SelectedUnit != null)
        {
            PlayerStats.AddMoney(SelectedUnit.sell_cost);
            Debug.Log("Sold for " + SelectedUnit.sell_cost + ". Current Money: " + PlayerStats.Money);
            string name = getSelectedGameObject().name;
            BuildableController buildable =
                getSelectedGameObject().GetComponent<BuildableController>();
            //gridMouse.ListOfGameObjects.RemoveAt(buildable.getArrayListPosition());
            coolRemoveAt(buildable.getArrayListPosition());
            //------------------------------------------------------------------------
            //before Destroying the object, remove it's entry from the matrix
            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 SelectedPosition = getSelectedGameObject().transform.position;
            int x;
            int z;
            if (SelectedUnit == Shop.instance.standardUnit)
            {
                //tower
                x = Mathf.FloorToInt(SelectedPosition.x + gridSize.x / 2);
                z = Mathf.FloorToInt(SelectedPosition.z + gridSize.y / 2);
            }
            else
            {
                //soldier camp
                //have to subtract 0.5 from each axis
                //because we want the center of the bottom-right tile
                //not the center of the four tiles
                //(which the game object's transform points to)
                x = Mathf.FloorToInt(SelectedPosition.x - 0.5f + gridSize.x / 2);
                z = Mathf.FloorToInt(SelectedPosition.z - 0.5f + gridSize.y / 2);
            }

            Vector3 position = gridMouse.CoordToPosition(x, z);
            gridMouse.propertiesMatrix[x, z].unit = null;
            gridMouse.previewMatrix[x, z] = false;
            if (SelectedUnit == Shop.instance.missileLauncher)
            {
                gridMouse.propertiesMatrix[x + 1, z + 1].unit = null;
                gridMouse.propertiesMatrix[x, z + 1].unit = null;
                gridMouse.propertiesMatrix[x + 1, z].unit = null;

                gridMouse.previewMatrix[x + 1, z + 1] = false;
                gridMouse.previewMatrix[x, z + 1] = false;
                gridMouse.previewMatrix[x + 1, z] = false;
            }
            //------------------------------------------------------------------------

            Destroy(getSelectedGameObject());
            //Debug.Log("AQUI: "+gridMouse.propertiesMatrix[x, y].builtGameObject);
            HideOptions();
            Debug.Log("Vendeu " + name + " na posição " + x + ", " + z);
        }
    }

    public void coolRemoveAt(int position)
    {
        GameObject lastObject = gridMouse.ListOfGameObjects[gridMouse.ListOfGameObjects.Count - 1];
        lastObject.GetComponent<BuildableController>().setArrayListPosition(position);
        gridMouse.ListOfGameObjects[position] = lastObject;
        gridMouse.ListOfGameObjects.RemoveAt(gridMouse.ListOfGameObjects.Count - 1);

    }

    public void UpgradeSelectedBuild()
    {
        Debug.Log("Going to upgrade " + getSelectedUnit().name + "!");
        if (getSelectedUnit() != null)
        {
            if (getSelectedUnit().name == "Tower")
            {
                if (PlayerStats.Money - Shop.instance.standardUnit.upgrade_cost >= 0)
                {
                    PlayerStats.AddMoney(-1 * Shop.instance.standardUnit.upgrade_cost);
                    SelectUnitToBuild(Shop.instance.towerLevel2);
                    BuildTheNextLevelStructure();
                    //buildManager.DeselectUnitToBuild();
                    //buildManager.DeselectSelectedUnit();                
                    //buildManager.HideOptions();
                    OnUnitUpgrade();
                    Debug.Log("Upgraded unit: " + "Tower");
                }
                else
                {
                    Debug.Log("You don't have enough money to upgrade this unit.");
                }
            }
            else if (getSelectedUnit().name == "Tower2")
            {
                if (PlayerStats.Money - Shop.instance.towerLevel2.upgrade_cost >= 0)
                {
                    PlayerStats.AddMoney(-1 * Shop.instance.towerLevel2.upgrade_cost);
                    SelectUnitToBuild(Shop.instance.towerLevel3);
                    BuildTheNextLevelStructure();
                    //buildManager.DeselectUnitToBuild();
                    //buildManager.DeselectSelectedUnit();
                    //buildManager.HideOptions();
                    OnUnitUpgrade();
                    Debug.Log("Upgraded unit: " + "Tower2");
                }
                else
                {
                    Debug.Log("You don't have enough money to upgrade this unit.");
                }
            }
            else
            {
                Debug.Log("Unit can't be upgraded any further !");
            }

        }
    }

    protected void BuildTheNextLevelStructure()
    {

        if (getUnitToBuild() != null)
        {

            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 SelectedPosition = getSelectedGameObject().transform.position;
            int x = Mathf.FloorToInt(SelectedPosition.x + gridSize.x / 2);
            int z = Mathf.FloorToInt(SelectedPosition.z + gridSize.y / 2);
            Vector3 position = gridMouse.CoordToPosition(x, z);
            //destroys the current object
            Destroy(gridMouse.propertiesMatrix[x, z].builtGameObject);
            int added_index = gridMouse.buildUnitAndAddItToTheList(position, true);
            gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

            //select upgraded unit
            PropertyScript.Property propertyInQuestion = gridMouse.propertiesMatrix[x, z];
                       
            gridMouse.SelectPosition(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
            Debug.Log("Selecionou a posição: " + x + ", " + z);

            GameObject obj = getSelectedGameObject();
            

            TowerSlowController tSlowc = obj.GetComponent<TowerSlowController>();
            if (tSlowc != null)
            {
                tSlowc.SetFireRateAndAttackPower();
                Debug.Log("THIS UNIT'S SLOW AMOUNT: " + tSlowc.slowAmount);
            }
            TowerController tc = obj.GetComponent<TowerController>();
            if (tc != null)
            {
                tc.SetFireRateAndAttackPower();
                Debug.Log("THIS UNIT'S FIRERATE AND ATTACKPOWER: " + tc.getFireRate() + ", " + tc.getAttackPower());
            }
            
        }

    }

    public void UpgradeSlow()
    {
        Debug.Log("Going to upgrade " + getSelectedUnit().name + "!");
        if (getSelectedUnit() != null)
        {
            if (PlayerStats.Money - Shop.instance.towerSlow.upgrade_cost >= 0)
            {
                PlayerStats.AddMoney(-1 * Shop.instance.towerSlow.upgrade_cost);
                
                SelectUnitToBuild(Shop.instance.towerSlow);
                BuildTheNextLevelStructure();
                //DeselectUnitToBuild();
                //DeselectSelectedUnit();
                //HideOptions();
                OnUnitUpgrade();
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
        Debug.Log("Going to upgrade " + getSelectedUnit().name + "!");
        if (getSelectedUnit() != null)
        {
            if (PlayerStats.Money - Shop.instance.towerTesla.upgrade_cost >= 0)
            {
                PlayerStats.AddMoney(-1 * Shop.instance.towerTesla.upgrade_cost);
                SelectUnitToBuild(Shop.instance.towerTesla);
                BuildTheNextLevelStructure();
                //DeselectUnitToBuild();
                //DeselectSelectedUnit();
                //HideOptions();
                OnUnitUpgrade();
                Debug.Log("Upgraded unit: " + "Tower");
            }
            else
            {
                Debug.Log("You don't have enough money to upgrade this unit.");
            }

        }
    }

    public void upgradeAP1()
    {
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getAttackPowerLVL() < 1)
        {
            int price = 0;
            switch (getTowerLvlByName()) {
                case 0:
                    price = Shop.instance.upgradeT1Ad1price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2Ad1price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3Ad1price;
                    break;
            }
            PlayerStats.AddMoney(-1 * price);
            tower.setAttackPowerLVL(1);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }

    public void upgradeAP2()
    {
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getAttackPowerLVL() < 2)
        {
            int price = 0;
            switch (getTowerLvlByName())
            {
                case 0:
                    price = Shop.instance.upgradeT1Ad2price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2Ad2price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3Ad2price;
                    break;

            }
            PlayerStats.AddMoney(-1 * price);
            tower.setAttackPowerLVL(2);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }
    public void upgradeAP3()
    {
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getAttackPowerLVL() < 3)
        {
            int price = 0;
            switch (getTowerLvlByName())
            {
                case 0:
                    price = Shop.instance.upgradeT1Ad3price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2Ad3price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3Ad3price;
                    break;
            }
            PlayerStats.AddMoney(-1 * price);
            tower.setAttackPowerLVL(3);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();

        }
    }

    public void upgradeFR1()
    {
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
   
        if (tower.getFireRateLVL() < 1)
        {
            int price = 0;
            switch (getTowerLvlByName())
            {
                case 0:
                    price = Shop.instance.upgradeT1As1price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2As1price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3As1price;
                    break;
            }
            PlayerStats.AddMoney(-1 * price);
            tower.setFireRateLVL(1);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }
    public void upgradeFR2()
    {
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 2)
        {
            int price = 0;
            switch (getTowerLvlByName())
            {
                case 0:
                    price = Shop.instance.upgradeT1As2price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2As2price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3As2price;
                    break;
            }
            PlayerStats.AddMoney(-1 * price);
            tower.setFireRateLVL(2);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }
    public void upgradeFR3()
    {
        
        TowerController tower = getSelectedGameObject().GetComponent<TowerController>();
        if (tower.getFireRateLVL() < 3)
        {
            int price = 0;
            switch (getTowerLvlByName())
            {
                case 0:
                    price = Shop.instance.upgradeT1As3price;
                    break;
                case 1:
                    price = Shop.instance.upgradeT2As3price;
                    break;
                case 2:
                    price = Shop.instance.upgradeT3As3price;
                    break;
            }
            PlayerStats.AddMoney(-1 * price);
            tower.setFireRateLVL(3);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }

    public int getTowerLvlByName() {
        TowerController towerController = selectedGameObject.GetComponent<TowerController>();
        int returnInt = 0;
        switch (towerController.name)
        {
            case "PrefabArcherTower1(Clone)":
                returnInt = 0;
                break;
            case "PrefabArcherTower2(Clone)":
                returnInt = 1;
                break;
            case "PrefabArcherTower3(Clone)":
                returnInt = 2;
                break;
            case "PrefabArcherTower2Slow(Clone)":
                returnInt = 3;
                break;
            case "PrefabArcherTower2Tesla(Clone)":
                returnInt = 4;
                break;
        }

        return returnInt;
    }
}
