using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour {

    GridMouse gridMouse;

    void Start()
    {
        gridMouse = GridMouse.instance;
    }

    public void doDestroyAll()
    {

        Debug.Log("Gonna destroy all !");
        for (int i = 0; i < gridMouse.ListOfGameObjects.Count; i++)
        {
            Destroy(gridMouse.ListOfGameObjects[i]);
        }
        
    }
    
}
