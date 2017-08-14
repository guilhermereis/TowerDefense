using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public GameObject standardUnitPrefab;
    public GameObject secondaryUnitPrefab;

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

    private GameObject unitToBuild;

    public GameObject getUnitToBuild()
    {
        return unitToBuild;
    }

    public void setUnitToBuild(GameObject unit)
    {
        unitToBuild = unit;
    }

}
