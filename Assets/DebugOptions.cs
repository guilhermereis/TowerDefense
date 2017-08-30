using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour {

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

        Debug.Log("Gonna destroy all !");
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            Destroy(gridMouse.ListOfGameObjects[i]);
        }
        
    }
    public void doSaveAll()
    {
        Debug.Log("Gonna save all !");
        listOfStates = new List<PropertyScript.StructureState>();
        BuildableController bc;
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            PropertyScript.StructureState state = new PropertyScript.StructureState();
            bc = gridMouse.ListOfGameObjects[i].GetComponent<BuildableController>();
            state.currentHealth = bc.Health;
            state.transform = gridMouse.ListOfGameObjects[i].transform;
            state.structureName = gridMouse.ListOfGameObjects[i].name;
            listOfStates.Add(state);
            Debug.Log("Added " + gridMouse.ListOfGameObjects[i].transform.position + ".");
        }
    }
    public void doLoadAll()
    {
        Debug.Log("Gonna load all !");
        for (int i = 0; i < listOfStates.Count; i++)
        {
            if (listOfStates[i].structureName == "Tower(Clone)") {
                shop.SelectStandardUnit();
                gridMouse.buildUnitAndAddItToTheList(listOfStates[i].transform.position);
                Debug.Log("Loaded " + listOfStates[i].transform.position+".");
            }
            
        }
    }
}
