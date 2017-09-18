﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TileMap))]
public class GridMouse : MonoBehaviour {

    //ignore layers 8,9,10 and 2 (IgnoreRaycast Layer)
    //(lowest order bit is 0-indexed)
    private int layerMask = Convert.ToInt32("11111111111111111111100011001001", 2);
    //private int layerMask = ~(1 << 10);

    public static GridMouse instance;
    public GameObject Track;
    public float ZOffset;
    private TileMap _tileMap;
    public Transform selectionCube;
    public Transform obstacleCube;
    public Transform gridTransform;
    private float tile_size;
    private Vector3 currentTileCoord;
    private BuildManager buildManager;
    private Vector3 previousPosition;
    private int prevX;
    private int prevZ;
    private int instance_x;
    private int instance_z;
    private GameObject temporaryInstance;
    private Vector3 position;
    private Vector3 rotation = new Vector3(-90, 0, 0);
    private bool rotated = false;
    GameObject temp;

    [SerializeField]
    public PropertyScript.Property[,] propertiesMatrix;

    [SerializeField]
    public bool[,] previewMatrix;

    [SerializeField]
    public List<GameObject> ListOfGameObjects;

    private Ray ray;
    private RaycastHit hitInfo;

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private Vector2 _gridSize;

    /*
    [SerializeField]
    private int _rows;

    [SerializeField]
    private int _columns;
    */
    void Awake()
    {
        if (instance != null) //if instance has been set before 
        {
            Debug.LogError("More than one GridMouse in scene !");
            return;
        }
        instance = this;
    }
    public Vector2 getGridSize()
    {
        return _gridSize;
    }
    public void UpdateGrid()
    {

        buildManager = BuildManager.instance;
        _transform.position = new Vector3(0f, ZOffset, 0f);
        _transform.localScale = new Vector3(_gridSize.x, _gridSize.y, 1.0f);

        //_material.SetTextureScale("_MainTex", new Vector2(_columns, _rows));
        _material.SetTextureScale("_MainTex", new Vector2(_gridSize.x, _gridSize.y));

        propertiesMatrix = new PropertyScript.Property[Mathf.FloorToInt(_gridSize.x),Mathf.FloorToInt(_gridSize.y)];
        previewMatrix = new bool[Mathf.FloorToInt(_gridSize.x), Mathf.FloorToInt(_gridSize.y)];
        //matrixOfGameObjects = new GameObject[Mathf.FloorToInt(_gridSize.x), Mathf.FloorToInt(_gridSize.y)];
        ListOfGameObjects = new List<GameObject>();

        for (int k = 0; k < previewMatrix.GetLength(0); k++)
        {
            for (int l = 0; l < previewMatrix.GetLength(1); l++)
            {
                previewMatrix[k, l] = false;
            }
        }
            


    }

    void Start()
    {
        UpdateGrid();
        ReadSpecialTiles();

    }
    void ReadSpecialTiles()
    {
        int x;
        int z;
        Debug.Log("Child Count = " + Track.transform.childCount);
        foreach (Transform child in Track.transform)
        {
            //tower
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            //Debug.Log(x + "," + z + " = Track");
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }
    }
    //HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    private bool CheckIfHitStructure()
    {
        return
            hitInfo.transform.gameObject.name == "PrefabArcherTower1(Clone)"
            || hitInfo.transform.gameObject.name == "PrefabCamp(Clone)";
    }
    private void HandleBuildingSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        if (true || !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())

        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);            
            
            //If I hit the Grid
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                if (temporaryInstance != null)
                {
                    position = temporaryInstance.transform.position;
                    x = Mathf.FloorToInt(position.x -0.5f + _gridSize.x / 2);
                    z = Mathf.FloorToInt(position.z -0.5f + _gridSize.y / 2);

                    {
                        if (buildManager.getUnitToBuild() != null)
                        {

                            if (CheckIfGameObjectIsOfColor(Color.green))
                            {
                                Vector3 newPosition = new Vector3(position.x - 0.5f, position.y, position.z - 0.5f);

                                int added_index = buildUnitAndAddItToTheList(newPosition);
                                Destroy(temporaryInstance);
                                //int added_index = buildUnitAndAddItToTheList(position);
                                propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
                                {
                                    propertiesMatrix[x + 1, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                    propertiesMatrix[x, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                    propertiesMatrix[x + 1, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                }
                                Debug.Log("Construiu na posição " + x + ", " + z);
                                Debug.Log("Position = " + position);
                            }
                        }
                    }
                }
            }
        }
    }
    //HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    private void HandleBuildingTower(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        
        if (true || !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())

        {
            Debug.Log("HANDLE BUILDING TOWER");
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);

            
            //If I hit the Grid
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                if (temporaryInstance != null) { 

                    position = temporaryInstance.transform.position;
                    x = Mathf.FloorToInt(position.x + _gridSize.x / 2);
                    z = Mathf.FloorToInt(position.z + _gridSize.y / 2);


                    {
                        if (buildManager.getUnitToBuild() != null)
                        {
                            if (CheckIfGameObjectIsOfColor(Color.green))
                            {
                                int added_index = buildUnitAndAddItToTheList(position);
                                Destroy(temporaryInstance);
                                propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                Debug.Log("Construiu na posição " + x + ", " + z);
                                Debug.Log("Position = " + position);
                            }
                        }
                    }
            }
            }
        }
    }
    void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity);
        if (didHit)
        {
            Debug.Log("Just hit: " + hitInfo.transform.gameObject.name);
        }
        int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
        int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
        PropertyScript.Property propertyInQuestion = propertiesMatrix[x, z];

        if (propertyInQuestion.unit != null) // If the tile contains a Structure
        {
            buildManager.SelectBuilding(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
            buildManager.ShowOptions();
            Debug.Log("Selecionou a posição: " + x + ", " + z);
            //Destroy(hitInfo.transform.gameObject);
        }
        else if (CheckIfHitStructure()) // If I hit a Structure
        {
            BuildableController buildable = hitInfo.transform.gameObject.GetComponent<BuildableController>();
            buildManager.SelectBuilding(buildable.getArrayListPosition());
            buildManager.ShowOptions();
        }
        else // Build something
        {
            if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
            {
                HandleBuildingSoldierCamp(ray, hitInfo, didHit, x, z);
                buildManager.DeselectUnitToBuild();
            }
            else if (buildManager.getUnitToBuild() == Shop.instance.standardUnit)
            {
                HandleBuildingTower(ray, hitInfo, didHit, x, z);
                buildManager.DeselectUnitToBuild();
            }
            else //if there's nothing to build, then hide the options
            {
                if (true || !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    buildManager.HideOptions();
                    Debug.Log("Hide Options");
                }
            }
        }
    }
    public Vector3 getPreviewRotation()
    {
        return this.rotation;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition) {
        ListOfGameObjects.Add(new GameObject());
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition);
        return AddedElmtIndex;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition, Quaternion rotation)
    {
        ListOfGameObjects.Add(new GameObject());
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition,rotation);
        return AddedElmtIndex;
    }
    void RotateAccordingly(int x, int z)
    {
            bool inside_width = x <= instance_x + 1 && x >= instance_x;
            bool inside_height = z<=instance_z+1 && z>=instance_z;
            if (z > instance_z + 1 && inside_width)
            {
                Debug.Log("Rotate up from " + rotation);
                rotation = new Vector3(-90, 180, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (x > instance_x + 1 && inside_height)
            {
                Debug.Log("Rotate right from " + rotation);
                rotation = new Vector3(-90, -90, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (x < instance_x && inside_height)
            {
                Debug.Log("Rotate left from " + rotation);
                rotation = new Vector3(-90,90, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (z < instance_z && inside_width)
            {
                Debug.Log("Rotate down from " + rotation);
                rotation = new Vector3(-90, 0, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
    }
    private bool CheckIfGameObjectIsOfColor(Color color)
    {
        bool result = false;
        foreach (Material matt in temporaryInstance.GetComponent<MeshRenderer>().materials)
        {
            if (matt.color == color)
                result = true;
        }
        return result;
    }
    private void SetPreviewColor(Color color)
    {
        foreach (Material matt in temporaryInstance.GetComponent<MeshRenderer>().materials)
        {
            Debug.Log("SETTING matt: " + matt.name);
            matt.SetColor("_Color", color);
        }
    }

    private void HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit,int x, int z)
    {
        if (didHit)
        {
            if (previousPosition == position)
            {
                //stepped over a track tile
                if (propertiesMatrix[x, z].type == "Track"
                    || propertiesMatrix[x + 1, z + 1].type == "Track"
                    || propertiesMatrix[x, z + 1].type == "Track"
                    || propertiesMatrix[x + 1, z].type == "Track")
                {
                    //don't build
                    //ROTATE !
                    if (!rotated)
                    {
                        RotateAccordingly(x, z);
                        if (propertiesMatrix[x, z].type == "Track")
                        {
                            SetPreviewColor(Color.green);
                        }
                        else
                        {
                            SetPreviewColor(Color.red);
                        }
                        
                        rotated = true;
                    }
                }
                else
                {//if the logic doens't involve going over track tiles
                    if (previewMatrix[x, z] == false
                        && previewMatrix[x + 1, z + 1] == false
                        && previewMatrix[x + 1, z] == false
                        && previewMatrix[x, z + 1] == false)
                    {


                        temporaryInstance = buildManager.BuildPreviewOn(new GameObject(), position);
                        rotated = false;
                        SetPreviewColor(Color.red);
                        instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
                        instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);

                        previewMatrix[instance_x, instance_z] = true;
                        previewMatrix[instance_x + 1, instance_z + 1] = true;
                        previewMatrix[instance_x + 1, instance_z] = true;
                        previewMatrix[instance_x, instance_z + 1] = true;
                        //Debug.Log("construiu preview !");
                    }
                }
            }
            else
            {
                //Debug.Log("moveu !");
                if (temporaryInstance != null)
                {
                    //stepped over a track tile
                    if (propertiesMatrix[x, z].type == "Track"
                        || propertiesMatrix[x + 1, z + 1].type == "Track"
                        || propertiesMatrix[x + 1, z].type == "Track"
                        || propertiesMatrix[x, z + 1].type == "Track")
                    {
                        RotateAccordingly(x, z);
                        if (propertiesMatrix[x, z].type == "Track")
                        {
                            SetPreviewColor(Color.green);
                        }
                        else
                        {
                            SetPreviewColor(Color.red);
                        }
                    }
                    else
                    {
                        //if the logic doens't involve going over track tiles
                        SetPreviewColor(Color.red);
                        instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
                        instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);
                        Destroy(temporaryInstance);

                        previewMatrix[instance_x, instance_z] = false;
                        previewMatrix[instance_x + 1, instance_z + 1] = false;
                        previewMatrix[instance_x + 1, instance_z] = false;
                        previewMatrix[instance_x, instance_z + 1] = false;
                        //previewMatrix[prevX, prevZ] = false;
                    }

                    //Debug.Log("destruiu preview !");
                }

            }
            previousPosition = position;
            prevX = x;
            prevZ = z;
            //previewMatrix[x, z] = true;
            //Transform newSelectionCube = Instantiate(obstaclePrefab, position + Vector3.up * .5f, Quaternion.identity) as Transform;
            
            //Debug.Log("Property of this tile: " + propertiesMatrix[x, z].type);
        }
    }
    private void HandlePreviewTower(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        if (didHit)
        {
            if (previousPosition == position)
            {
                //stepped over a track tile
                if (propertiesMatrix[x, z].type == "Track")
                {
                    //don't build
                }
                else
                {//if the logic doens't involve going over track tiles
                    if (previewMatrix[x, z] == false)
                    {

                        temporaryInstance = buildManager.BuildPreviewOn(new GameObject(), position);
                        previewMatrix[x, z] = true;
                        //Debug.Log("construiu preview !");
                    }
                }
            }
            else
            {
                //Debug.Log("moveu !");
                if (temporaryInstance != null)
                {
                    //stepped over a track tile
                    if (propertiesMatrix[x, z].type == "Track")
                    {

                    }
                    else
                    {
                        //if the logic doens't involve going over track tiles
                        Destroy(temporaryInstance);
                        int instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x + _gridSize.x / 2);
                        int instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z + _gridSize.y / 2);

                        previewMatrix[instance_x, instance_z] = false;
                        //previewMatrix[prevX, prevZ] = false;
                    }

                    //Debug.Log("destruiu preview !");
                }

            }
            previousPosition = position;
            prevX = x;
            prevZ = z;
            //previewMatrix[x, z] = true;
            //Transform newSelectionCube = Instantiate(obstaclePrefab, position + Vector3.up * .5f, Quaternion.identity) as Transform;
            //Debug.Log("Property of this tile: " + propertiesMatrix[x, z].type);
        }
    }

	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity);

        if (didHit)
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);

            
            Vector3 positionCube = new Vector3(position.x, position.y + 0.5f, position.z);
            selectionCube.transform.position = positionCube;
            //Debug.Log("TILE: " + x + "," + z + " OF TYPE: " + propertiesMatrix[x, z].type);

            if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
            {
                HandlePreviewSoldierCamp(ray, hitInfo, didHit, x, z);
            }
            else if (buildManager.getUnitToBuild() == Shop.instance.standardUnit)
            {
                HandlePreviewTower(ray, hitInfo, didHit, x, z);
            }
        }    
	}
    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset, -_gridSize.y / 2 + 0.5f + y);
    }

    
}
