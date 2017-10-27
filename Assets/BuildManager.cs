using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (bottomBar)
            bottomBarBehaviour = (BottomInfoBarBehaviour)bottomBar.GetComponentInChildren<BottomInfoBarBehaviour>();
        if (upgradeWheel)
            upgradeWheelController = upgradeWheel.GetComponent<UpgradeWheelController>();

        GameObject slider = GameObject.Find("Slider");
        if (slider)
            Debug.Log("VOLUME: "+slider.GetComponent<Slider>().value);
        GameObject audioOpt = GameObject.FindWithTag("AudioOptions");
        Destroy(audioOpt);
    }	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
        */
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
        if (temporaryInstance.name == "PreviewGameObject")
        {
            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 newPosition;
            if (unitToBuild == Shop.instance.miningCamp)
            {
                newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
                Destroy(temporaryInstance);
                temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, newPosition, unitToBuild.prefab.transform.rotation);
            }
            else
            {
                newPosition = position;
                Destroy(temporaryInstance);
                temporaryInstance = (GameObject)Instantiate(unitToBuild.prefab, newPosition, unitToBuild.prefab.transform.rotation);
                temporaryInstance.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
                temporaryInstance.transform.Find("GroundLine").gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

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
        }
        else if (temporaryInstance != null && unitToBuild != null)
        {
            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 newPosition;
            if (unitToBuild == Shop.instance.miningCamp)
            {
                newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
            }
            else
            {
                newPosition = position;
            }
            temporaryInstance.transform.position = newPosition;
            
            temporaryInstance.GetComponent<Renderer>().material.color = Color.green;
            
        }
        
        return temporaryInstance;
    }

    public void BuildUnitOn(ref List<GameObject> tempList,int index, Vector3 position, bool upgraded)
    {
        Destroy(GameObject.Find("PreviewGameObject"));
        //Debug.Log("Gonna build !!!");
        if (upgraded == false)
        {
            if (PlayerStats.Money < unitToBuild.cost)
            {
                //Debug.Log("Not enough money to build that!");
                return;
            }
            int spent = PlayerStats.AddMoney(-1 * unitToBuild.cost);
            GameController.MoneySpent(spent);
        }

        Vector3 newPosition;
        if (unitToBuild == Shop.instance.miningCamp)
            newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
        else
            newPosition = position;
        tempList[index] = Instantiate(unitToBuild.prefab, newPosition, Quaternion.Euler(gridMouse.getPreviewRotation()));
        //tempList[index] = Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
        tempList[index].GetComponent<BuildableController>().setArrayListPosition(index);

        UnitBlueprint newUnitBlueprint = new UnitBlueprint();
        newUnitBlueprint.name = getUnitToBuild().name;
        newUnitBlueprint.prefab = getUnitToBuild().prefab;
        newUnitBlueprint.cost = getUnitToBuild().cost;
        newUnitBlueprint.sell_cost = getUnitToBuild().sell_cost;
        newUnitBlueprint.withInterest_sellcost = getUnitToBuild().withInterest_sellcost;
        newUnitBlueprint.upgrade_cost = getUnitToBuild().upgrade_cost;
        newUnitBlueprint.position = position;

        if (newUnitBlueprint.name == Shop.instance.miningCamp.name)
        {
            newUnitBlueprint.stored_x = Mathf.FloorToInt(position.x -0.5f + gridMouse.getGridSize().x / 2);
            newUnitBlueprint.stored_z = Mathf.FloorToInt(position.z -0.5f + gridMouse.getGridSize().y / 2);
        }
        else
        {
            newUnitBlueprint.stored_x = Mathf.FloorToInt(position.x + gridMouse.getGridSize().x / 2);
            newUnitBlueprint.stored_z = Mathf.FloorToInt(position.z + gridMouse.getGridSize().y / 2);
        } 
        

        tempList[index].GetComponent<BuildableController>().setUnitBlueprintAndType(newUnitBlueprint);
        
        Transform sphere = tempList[index].transform.Find("Sphere");
        Transform groundLine = tempList[index].transform.Find("GroundLine");
        
        if (sphere){
            sphere.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if (groundLine) {
            groundLine.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if(tempList[index].GetComponent<BuildableController>() != null)
        {
            tempList[index].GetComponent<BuildableController>().BuildEffect();
        }
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            Debug.Log(i+": "+gridMouse.ListOfGameObjects[i].name);
        }
        Destroy(GameObject.Find("UnitGameObject"));

    }
    public void BuildUnitOn(ref List<GameObject> tempList, int index, Vector3 position, Quaternion rotation, bool upgraded = false)
    {
        Destroy(GameObject.Find("PreviewGameObject"));
        if (upgraded == false)
        {
            if (PlayerStats.Money < unitToBuild.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }
            int spent = PlayerStats.AddMoney(-1 * unitToBuild.cost);
            GameController.MoneySpent(spent);
        }
        Vector3 newPosition;
        if (unitToBuild == Shop.instance.miningCamp)
            newPosition = new Vector3(position.x + 0.5f, position.y, position.z + 0.5f);
        else
            newPosition = position;
        tempList[index] = Instantiate(unitToBuild.prefab, newPosition, rotation);
        //tempList[index] = Instantiate(unitToBuild.prefab, position, unitToBuild.prefab.transform.rotation);
        tempList[index].GetComponent<BuildableController>().setArrayListPosition(index);


        UnitBlueprint newUnitBlueprint = new UnitBlueprint();
        newUnitBlueprint.name = getUnitToBuild().name;
        newUnitBlueprint.prefab = getUnitToBuild().prefab;
        newUnitBlueprint.cost = getUnitToBuild().cost;
        newUnitBlueprint.sell_cost = getUnitToBuild().sell_cost;
        newUnitBlueprint.withInterest_sellcost = getUnitToBuild().withInterest_sellcost;
        newUnitBlueprint.upgrade_cost = getUnitToBuild().upgrade_cost;
        newUnitBlueprint.position = position;
                
        if (newUnitBlueprint.name == Shop.instance.miningCamp.name)
        {
            newUnitBlueprint.stored_x = Mathf.FloorToInt(position.x - 0.5f + gridMouse.getGridSize().x / 2);
            newUnitBlueprint.stored_z = Mathf.FloorToInt(position.z - 0.5f + gridMouse.getGridSize().y / 2);
        }
        else
        {
            newUnitBlueprint.stored_x = Mathf.FloorToInt(position.x + gridMouse.getGridSize().x / 2);
            newUnitBlueprint.stored_z = Mathf.FloorToInt(position.z + gridMouse.getGridSize().y / 2);
        }

        tempList[index].GetComponent<BuildableController>().setUnitBlueprintAndType(newUnitBlueprint);
        //GameController.AddBuiltTower(tempList[index].GetComponent<BuildableController>().buildType);
        tempList[index].GetComponent<BuildableController>().BuildEffect();

        //if its not a mining camp
        if (unitToBuild != Shop.instance.miningCamp) {
            tempList[index].transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
            tempList[index].transform.Find("GroundLine").gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        //for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        //{
        //    Debug.Log("EIS A LISTA");
        //    Debug.Log(i + ": " + gridMouse.ListOfGameObjects[i].name);
        //}
        //Debug.Log("Unit built ! Money left: " + PlayerStats.Money);
        Destroy(GameObject.Find("UnitGameObject"));
    }

    public void OnUnitUpgrade() {
        showBottomBar();
        showUpgradeWheel();
    }

    //public void SelectBuilding(UnitBlueprint unit, Vector2 position)
    public void SelectBuilding(UnitBlueprint unit, GameObject gameObject)
    {
        if (!TopRightMenu.isGamePaused)
        {
            unitToBuild = null;
            if (LastSelectedGameObject != null)
            {
                Transform LastSphere = LastSelectedGameObject.transform.Find("Sphere");
                Transform LastGroundLine = LastSelectedGameObject.transform.Find("GroundLine");

                if (LastSphere)
                    LastSphere.gameObject.GetComponent<MeshRenderer>().enabled = false;

                if (LastGroundLine)
                    LastGroundLine.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            //if (selectedUnit == unit)
            selectedUnit = unit;
            selectedGameObject = gameObject;
            selectedPosition = gameObject.transform.position;

            Transform Sphere = selectedGameObject.transform.Find("Sphere");
            Transform GroundLine = selectedGameObject.transform.Find("GroundLine");
            if (Sphere)
                Sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (GroundLine)
                GroundLine.gameObject.GetComponent<MeshRenderer>().enabled = true;

            LastSelectedGameObject = selectedGameObject;
        }
    }
    public void SelectBuilding(int indexOfSelectedObject)
    {
        if (!TopRightMenu.isGamePaused)
        {
            unitToBuild = null;
            if (LastSelectedGameObject != null)
            {
                LastSelectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = false;
                LastSelectedGameObject.transform.Find("GroundLine").gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            GridMouse gridMouse = GridMouse.instance;
            BuildableController buildable =
                    gridMouse.ListOfGameObjects[indexOfSelectedObject].GetComponent<BuildableController>();

            //if (selectedUnit == unit)
            selectedUnit = buildable.getUnitBlueprint();
            selectedGameObject = gridMouse.ListOfGameObjects[indexOfSelectedObject];
            selectedGameObject.transform.Find("Sphere").gameObject.GetComponent<MeshRenderer>().enabled = true;
            selectedGameObject.transform.Find("GroundLine").gameObject.GetComponent<MeshRenderer>().enabled = true;

            //selectedPosition = position;
            LastSelectedGameObject = selectedGameObject;
        }
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
            Transform Sphere = selectedGameObject.transform.Find("Sphere");
            Transform GroundLine = selectedGameObject.transform.Find("GroundLine");

            if (Sphere)
            {
                MeshRenderer renderer = Sphere.gameObject.GetComponent<MeshRenderer>();
                if (renderer)
                    renderer.enabled = false;
            }
            if (GroundLine)
            {
                MeshRenderer renderer = GroundLine.gameObject.GetComponent<MeshRenderer>();
                if (renderer)
                    renderer.enabled = false;
            }
        }
        
    }

    public void hideBottomBar() {
        bottomBarBehaviour.setSelectionState(0);
    }

    public void showBottomBar() {
        bottomBarBehaviour.setSelectedUnit(selectedGameObject);
        bottomBarBehaviour.setSelectionState(1);
    }

    public void SetCampFull()
    {
        if (upgradeWheelController)
        {

        }
    }

    public void showUpgradeWheel() {
        if (!TopRightMenu.isGamePaused)
        {
            if (upgradeWheelController)
            {
                upgradeWheelController.tower = selectedGameObject;
                BuildableController buildingController = selectedGameObject.GetComponent<BuildableController>();
                TowerController towerController = selectedGameObject.GetComponent<TowerController>();
                string name = buildingController.getUnitBlueprint().name;

                if (buildingController)
                {


                    if (name == Shop.instance.towerLevel1.name)
                    {
                        upgradeWheelController.setTowerLvl(0);
                    }
                    else if (name == Shop.instance.towerLevel2.name)
                    {
                        upgradeWheelController.setTowerLvl(1);
                    }
                    else if (name == Shop.instance.towerLevel3.name)
                    {
                        upgradeWheelController.setTowerLvl(2);
                        upgradeWheelController.setSpecialization(1);
                    }
                    else if (name == Shop.instance.towerSlow.name)
                    {
                        upgradeWheelController.setTowerLvl(1);
                        upgradeWheelController.setSpecialization(0);
                    }
                    else if (name == Shop.instance.towerTesla.name)
                    {
                        upgradeWheelController.setTowerLvl(1);
                        upgradeWheelController.setSpecialization(2);
                    }

                    if (name == Shop.instance.miningCamp.name)
                    {
                        upgradeWheelController.setMineSellPrice();
                    }
                    else
                    {
                        upgradeWheelController.setAttackDamage((int)((TowerController)buildingController).getAttackPowerLVL());
                        upgradeWheelController.setAttackSpeedLvl((int)((TowerController)buildingController).getFireRateLVL());
                    }

                    upgradeWheelController.isActive = true;
                    upgradeWheel.SetActive(true);
                    upgradeWheelController.openWheel();
                }
                else
                {

                    upgradeWheel.SetActive(true);
                    upgradeWheel.transform.Find("UpgradeAttackSpeed").gameObject.SetActive(false);
                    upgradeWheel.transform.Find("UpgradeAttackDamage").gameObject.SetActive(false);
                    upgradeWheel.transform.Find("UpgradeTowerIcer").gameObject.SetActive(false);

                    upgradeWheelController.isActive = true;
                    upgradeWheelController.openWheel();
                }
            }
        }
    }

    public void hideUpgradeWheel() {
        if (upgradeWheelController)
        {
            upgradeWheelController.closeWheel();
        }
    }
    public void forceHideUpgradeWheel()
    {
        if (upgradeWheelController)
        {
            upgradeWheelController.onWheelClosed();
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
        //UnitBlueprint SelectedUnit = getSelectedUnit();
        BuildableController bc = getSelectedGameObject().GetComponent<BuildableController>();

        if (bc.getUnitBlueprint() != null)
        {
            int added = PlayerStats.AddMoney(bc.GetSellCostWithInterest());
            GameController.MoneyCollected(added,false);

            Debug.Log("Sold for " + bc.GetSellCostWithInterest() + ". Current Money: " + PlayerStats.Money);
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
            if (bc.getUnitBlueprint() == Shop.instance.towerLevel1
             || bc.getUnitBlueprint() == Shop.instance.towerLevel2
             || bc.getUnitBlueprint() == Shop.instance.towerLevel3
             || bc.getUnitBlueprint() == Shop.instance.towerTesla
             || bc.getUnitBlueprint() == Shop.instance.towerSlow)
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
                MiningCampController mc =  getSelectedGameObject().GetComponent<MiningCampController>();
                if(mc)
                    Destroy(mc.fullButton);


                x = Mathf.FloorToInt(SelectedPosition.x - 0.5f + gridSize.x / 2);
                z = Mathf.FloorToInt(SelectedPosition.z - 0.5f + gridSize.y / 2);

            }

            Vector3 position = gridMouse.CoordToPosition(x, z);
            gridMouse.propertiesMatrix[x, z].unit = null;
            gridMouse.propertiesMatrix[x, z].type = "Normal";
            gridMouse.previewMatrix[x, z] = false;
            if (bc.getUnitBlueprint() == Shop.instance.miningCamp)
            {
                gridMouse.propertiesMatrix[x + 1, z + 1].unit = null;
                gridMouse.propertiesMatrix[x, z + 1].unit = null;
                gridMouse.propertiesMatrix[x + 1, z].unit = null;

                gridMouse.propertiesMatrix[x + 1, z + 1].type = "Normal";
                gridMouse.propertiesMatrix[x, z + 1].type = "Normal";
                gridMouse.propertiesMatrix[x + 1, z].type = "Normal";


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
                if (PlayerStats.Money - Shop.instance.towerLevel1.upgrade_cost >= 0)
                {
                    int spent = PlayerStats.AddMoney(-1 * Shop.instance.towerLevel1.upgrade_cost);
                    GameController.MoneySpent(spent);

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
                    int spent = PlayerStats.AddMoney(-1 * Shop.instance.towerLevel2.upgrade_cost);
                    GameController.MoneySpent(spent);

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

            //remove the previous version of the unit from the list
            BuildableController bc = gridMouse.propertiesMatrix[x, z].builtGameObject.GetComponent<BuildableController>();
            coolRemoveAt(bc.getArrayListPosition());

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
                int spent = PlayerStats.AddMoney(-1 * Shop.instance.towerSlow.upgrade_cost);
                GameController.MoneySpent(spent);
                
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
                int spent = PlayerStats.AddMoney(-1 * Shop.instance.towerTesla.upgrade_cost);
                GameController.MoneySpent(spent);

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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);

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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);

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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);
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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);

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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);

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
            int spent = PlayerStats.AddMoney(-1 * price);
            GameController.MoneySpent(spent);

            tower.setFireRateLVL(3);
            tower.SetFireRateAndAttackPower();
            OnUnitUpgrade();
        }
    }

    public int getTowerLvlByName() {
        TowerController towerController = selectedGameObject.GetComponent<TowerController>();
        string name = towerController.getUnitBlueprint().name;
        //Debug.Log("CHECK " + name + ", " + Shop.instance.towerLevel1.name);// + ", " + towerController.getUnitBlueprint().name);
        int returnInt = 0;

        if (name == Shop.instance.towerLevel1.name)
        {
            returnInt = 0;
        }
        else if (name == Shop.instance.towerLevel2.name)
        {
            returnInt = 1;
        }
        else if (name == Shop.instance.towerLevel3.name)
        {
            returnInt = 2;
        }
        else if (name == Shop.instance.towerSlow.name)
        {
            returnInt = 3;
        }
        else if (name == Shop.instance.towerTesla.name)
        {
            returnInt = 4;
        }
        

        return returnInt;
    }
}
