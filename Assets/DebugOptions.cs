using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour
{


    GridMouse gridMouse;
    BuildManager buildManager;
    List<PropertyScript.StructureState> listOfStates;
    Shop shop;
    
    void Start()
    {
        gridMouse = GridMouse.instance;
        buildManager = BuildManager.instance;
        shop = Shop.instance;
        listOfStates = new List<PropertyScript.StructureState>();
    }

    public void doDestroyAll()
    {
        UpgradeWheelController uwc = 
            GameObject.Find("UpgradeWheel").GetComponent<UpgradeWheelController>();
        uwc.gameObject.SetActive(false);
        uwc.isActive = false;
        uwc.clearButtons();
        Debug.Log("Gonna destroy all !");
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            Destroy(gridMouse.ListOfGameObjects[i]);
            //gridMouse.ListOfGameObjects.RemoveAt(i);
        }
        gridMouse.ListOfGameObjects.Clear();
        Debug.Log("SIZEEEE: " + gridMouse.ListOfGameObjects.Count);

    }
    public void doSaveAll()
    {
        Debug.Log("Gonna save all !");
        listOfStates = new List<PropertyScript.StructureState>();
        BuildableController bc;
        TowerController tc;
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            //Get information from the Buildable
            bc = gridMouse.ListOfGameObjects[i].GetComponent<BuildableController>();
            //--------------------------------------------------------------------------
            //Get information from the TowerController
            tc = gridMouse.ListOfGameObjects[i].GetComponent<TowerController>();
            //--------------------------------------------------------------------------
            PropertyScript.StructureState state =
                new PropertyScript.StructureState(state.structureName = gridMouse.ListOfGameObjects[i].name,
                                                  gridMouse.ListOfGameObjects[i].transform,
                                                    bc.Health,tc.fireRateLVL,tc.attackPowerLVL);
            Debug.Log("Just Saved FR, AP = " + tc.fireRateLVL + ", " + tc.attackPowerLVL);
            listOfStates.Add(state);
            Debug.Log("Added " + gridMouse.ListOfGameObjects[i].transform.position + ".");
        }
    }
    public void doLoadAll()
    {
        Debug.Log("Gonna load all " + listOfStates.Count + " !");
        for (int i = 0; i < listOfStates.Count; i++)
        {
            if (listOfStates[i].structureName == "PrefabArcherTower1(Clone)")
            {
                shop.SelectStandardUnit();
                int added_index = gridMouse.buildUnitAndAddItToTheList(listOfStates[i].position,false);
                Vector2 gridSize = gridMouse.getGridSize();
                int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x,z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                //Set Fire Rate and Attack Power from saved state
                gridMouse.ListOfGameObjects[added_index]
                    .GetComponent<TowerController>()
                        .SetFireRateAndAttackPowerByLVL(listOfStates[i].fireRateLVL,listOfStates[i].attackPowerLVL);
                Debug.Log("Just Loaded FR, AP = " + listOfStates[i].fireRateLVL+", "+listOfStates[i].attackPowerLVL);
                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else if (listOfStates[i].structureName == "PrefabCamp(Clone)")
            {
                shop.SelectSecondaryUnit();
                Vector3 newPosition = new Vector3(listOfStates[i].position.x - 0.5f, listOfStates[i].position.y, listOfStates[i].position.z - 0.5f);
                int added_index = gridMouse.buildUnitAndAddItToTheList(newPosition, listOfStates[i].rotation);
                Vector2 gridSize = gridMouse.getGridSize();

                //int x = Mathf.FloorToInt(listOfStates[i].position.x + gridSize.x / 2);
                //int z = Mathf.FloorToInt(listOfStates[i].position.z + gridSize.y / 2);

                int x = Mathf.FloorToInt(newPosition.x + gridSize.x / 2);
                int z = Mathf.FloorToInt(newPosition.z + gridSize.y / 2);

                gridMouse.propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x + 1, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");
                gridMouse.propertiesMatrix[x, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gridMouse.ListOfGameObjects, added_index, "Obstacle");

                Debug.Log("LOOOOOOOOOOOOOADED " + listOfStates[i].position + ".");
            }
            else
            {
                Debug.Log("DID NOT LOAD");
            }

        }
    }
}
