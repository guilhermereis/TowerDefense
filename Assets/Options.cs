using UnityEngine;

public class Options : MonoBehaviour {

    protected BuildManager buildManager;
    protected GridMouse gridMouse;
    protected Shop shop;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;
        gridMouse = GridMouse.instance;
        shop = Shop.instance;
    }

    public void Upgrade() {
        Debug.Log("Going to upgrade "+ buildManager.getSelectedUnit().name+"!");
        if (buildManager.getSelectedUnit() != null) {
            if (buildManager.getSelectedUnit().name == "Tower")
            {
                if (PlayerStats.Money - Shop.instance.standardUnit.upgrade_cost >= 0)
                {
                    PlayerStats.AddMoney(-1 * Shop.instance.standardUnit.upgrade_cost);
                    buildManager.SelectUnitToBuild(shop.towerLevel2);
                    BuildTheNextLevelStructure();
                    //buildManager.DeselectUnitToBuild();
                    //buildManager.DeselectSelectedUnit();                
                    //buildManager.HideOptions();
                    buildManager.OnUnitUpgrade();
                    Debug.Log("Upgraded unit: " + "Tower");
                }
                else
                {
                    Debug.Log("You don't have enough money to upgrade this unit.");
                }
            }
            else if (buildManager.getSelectedUnit().name == "Tower2")
            {
                if (PlayerStats.Money - Shop.instance.towerLevel2.upgrade_cost >= 0)
                {
                    PlayerStats.AddMoney(-1 * Shop.instance.towerLevel2.upgrade_cost);
                    buildManager.SelectUnitToBuild(shop.towerLevel3);
                    BuildTheNextLevelStructure();
                    //buildManager.DeselectUnitToBuild();
                    //buildManager.DeselectSelectedUnit();
                    //buildManager.HideOptions();
                    buildManager.OnUnitUpgrade();
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
    protected void BuildTheNextLevelStructure() {
        
        if (buildManager.getUnitToBuild() != null)
        {

            Vector2 gridSize = gridMouse.getGridSize();
            Vector3 SelectedPosition = buildManager.getSelectedGameObject().transform.position;
            int x = Mathf.FloorToInt(SelectedPosition.x + gridSize.x / 2);
            int z = Mathf.FloorToInt(SelectedPosition.z + gridSize.y / 2);
            Vector3 position = gridMouse.CoordToPosition(x, z);
            //destroys the current object
            Destroy(gridMouse.propertiesMatrix[x, z].builtGameObject);
            int added_index = gridMouse.buildUnitAndAddItToTheList(position, true);
            gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

            //select upgraded unit
            PropertyScript.Property propertyInQuestion = gridMouse.propertiesMatrix[x, z];
            gridMouse.SelectPosition(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
            Debug.Log("Selecionou a posição: " + x + ", " + z);
        }
        
    }
	
}
