using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TileMap))]
public class GridMouse : MonoBehaviour
{

    //    Layers:
    //        0: Default
    //        1: TransparentFX
    //        2: Ignore Raycast
    //        3: 
    //        4: Water
    //        5: UI
    //        6:
    //        7:
    //        8:Coins
    //        9:Projectiles
    //        10:Monsters
    //        11:EdgeTiles

    //ignore layers 8,9,10 and 2 (IgnoreRaycast Layer)
    //(lowest order bit is 0-indexed)
    private int layerMask = Convert.ToInt32("11111111111111111111000011101001", 2);
    //private int layerMask = ~(1 << 10);

    public static GridMouse instance;
    public GameObject Track;
    public GameObject Trees;
    public GameObject Edges;
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
    private int cursor_x;
    private int cursor_z;
    private GameObject temporaryInstance;
    private Vector3 position;
    private Vector3 rotation = new Vector3(-90, 0, 0);
    private bool rotated = false;
    private 
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
                propertiesMatrix[k, l] = new PropertyScript.Property("Normal");
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
            if (x == 45 && z == 13)
            {
                Debug.Log(x + "," + z + " = Track");
            }
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }


        foreach (Transform child in Trees.transform)
        {
         
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }

        foreach (Transform child in Edges.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
    }
    //HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    private bool CheckIfHitStructure()
    {
        BuildableController bc = hitInfo.transform.gameObject.GetComponent<BuildableController>();
        if (bc == null)
            return false;
        UnitBlueprint unit = bc.getUnitBlueprint();
        return
            (unit.name == Shop.instance.standardUnit.name
            || unit.name == Shop.instance.missileLauncher.name
            || unit.name == Shop.instance.towerLevel2.name
            || unit.name == Shop.instance.towerLevel3.name
            || unit.name == Shop.instance.towerSlow.name
            || unit.name == Shop.instance.towerTesla.name);
    }
    private void HandleBuildingSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        //this if doesn't make sense, because if we already decided to build we don't care
        //if mouse is over any UI's
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

                                int added_index = buildUnitAndAddItToTheList(newPosition,false);
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
    private Vector2 ReturnFirstFreeTileAround()
    {
        //center is (cursor_x, cursor_z)
        int x = cursor_x;
        int z = cursor_z;
        Vector2 answer = Vector2.zero;
        //look for space on the right up
        if (propertiesMatrix[x+1, z+1].type == "Normal"
                    && propertiesMatrix[x + 2, z + 1].type == "Normal"
                    && propertiesMatrix[x+1, z].type == "Normal"
                    && propertiesMatrix[x + 2, z].type == "Normal")
        {
            answer = new Vector2(x+1, z);
        }
        //look for space on the right down
        if (propertiesMatrix[x + 1, z - 1].type == "Normal"
                    && propertiesMatrix[x + 2, z - 1].type == "Normal"
                    && propertiesMatrix[x + 1, z].type == "Normal"
                    && propertiesMatrix[x + 2, z].type == "Normal")
        {
            answer = new Vector2(x + 1, z - 1);
        }
        //look for space on the left up
        else if (propertiesMatrix[x - 1, z].type == "Normal"
                    && propertiesMatrix[x - 2, z].type == "Normal"
                    && propertiesMatrix[x - 1, z + 1].type == "Normal"
                    && propertiesMatrix[x - 2, z + 1].type == "Normal")
        {
            answer = new Vector2(x - 2, z);
        }
        //look for space on the left down
        else if (propertiesMatrix[x - 1, z].type == "Normal"
                    && propertiesMatrix[x - 2, z].type == "Normal"
                    && propertiesMatrix[x - 1, z - 1].type == "Normal"
                    && propertiesMatrix[x - 2, z - 1].type == "Normal")
        {
            answer = new Vector2(x - 2, z - 1);
        }
        //look for space on the top-left
        else if (propertiesMatrix[x - 1, z + 1].type == "Normal"
                    && propertiesMatrix[x - 1, z + 2].type == "Normal"
                    && propertiesMatrix[x, z + 1].type == "Normal"
                    && propertiesMatrix[x, z + 2].type == "Normal")
        {
            answer = new Vector2(x - 1, z + 1);
        }
        //look for space on the top-right
        else if (propertiesMatrix[x, z + 1].type == "Normal"
                    && propertiesMatrix[x + 1, z + 1].type == "Normal"
                    && propertiesMatrix[x, z + 2].type == "Normal"
                    && propertiesMatrix[x + 1, z + 2].type == "Normal")
        {
            answer = new Vector2(x, z + 1);
        }
        //look for space on the bottom-left
        else if (propertiesMatrix[x, z - 1].type == "Normal"
                    && propertiesMatrix[x - 1, z - 1].type == "Normal"
                    && propertiesMatrix[x, z - 2].type == "Normal"
                    && propertiesMatrix[x - 1, z - 2].type == "Normal")
        {
            answer = new Vector2(x - 1, z - 2);
        }
        //look for space on the bottom-right
        else if (propertiesMatrix[x, z - 1].type == "Normal"
                    && propertiesMatrix[x, z - 2].type == "Normal"
                    && propertiesMatrix[x+1, z - 1].type == "Normal"
                    && propertiesMatrix[x+1, z - 2].type == "Normal")
        {
            answer = new Vector2(x, z - 2);
        }
        return answer;
    }
    //HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    private void HandleBuildingTower(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        //this if doesn't make sense, because if we already decided to build we don't care
        //if mouse is over any UI's
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
                                int added_index = buildUnitAndAddItToTheList(position,false);
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
    public void SelectPosition(UnitBlueprint unit, GameObject gameObject)
    {
        buildManager.SelectBuilding(unit, gameObject);
        buildManager.ShowOptions();
        buildManager.showBottomBar();
        //Destroy(hitInfo.transform.gameObject);
    }

    void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
        if (didHit)
        {
            Debug.Log("Just hit: " + hitInfo.transform.gameObject.name);
        }
        int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
        int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
        PropertyScript.Property propertyInQuestion = propertiesMatrix[x, z];
        Debug.Log("PROPERTY IN QUESTION = " + propertyInQuestion.type);

        if (propertyInQuestion.unit != null) // If the tile contains a Structure
        {
            buildManager.DeselectUnitToBuild();
            buildManager.DeselectSelectedUnit();
            buildManager.HideOptions();
            SelectPosition(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
            Debug.Log("Selecionou a posição: " + x + ", " + z);
        }
        else if (CheckIfHitStructure()) // If I hit a Structure
        {
            BuildableController buildable = hitInfo.transform.gameObject.GetComponent<BuildableController>();
            buildManager.SelectBuilding(buildable.getArrayListPosition());
            buildManager.ShowOptions();
            buildManager.showBottomBar();
        }
        else if (propertyInQuestion.type == "Tree") // If I hit a Tree
        {
            buildManager.HideOptions();
            buildManager.hideBottomBar();
            //IGNORE THE CLICK
        }
        else // Decide to Build something
        {
            Debug.Log("ENTERED HERE");
            if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
            {
                if (propertyInQuestion.type == "Track")
                {
                    //FOR SOLDIER CAMP,
                    //I ONLY BUILD SOMETHING IF I CLICKED ON A TRACK TILE.
                    HandleBuildingSoldierCamp(ray, hitInfo, didHit, x, z);
                    buildManager.DeselectUnitToBuild();
                }
            }
            else if (buildManager.getUnitToBuild() == Shop.instance.standardUnit)
            {
                Debug.Log("NOW HERE");
                HandleBuildingTower(ray, hitInfo, didHit, x, z);
                buildManager.DeselectUnitToBuild();
            }
            else //if there's nothing to build, then hide the options
            {
                //this if here makes sense, because when clicking over menus we dont want to
                //hide the options.
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    buildManager.HideOptions();
                    buildManager.hideBottomBar();
                    Debug.Log("Hide Options");
                }
            }
        }
    }
    public Vector3 getPreviewRotation()
    {
        return this.rotation;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition, bool upgraded) {
        ListOfGameObjects.Add(new GameObject());
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition, upgraded);
        return AddedElmtIndex;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition, Quaternion rotation, bool upgraded = false)
    {
        ListOfGameObjects.Add(new GameObject());
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition,rotation, upgraded);
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
            //Debug.Log("SETTING matt: " + matt.name);
            matt.SetColor("_Color", color);
        }
    }
    private void Instantiate(Vector3 pos)
    {
        temporaryInstance = buildManager.BuildPreviewOn(new GameObject(), pos);
        rotated = false;
        SetPreviewColor(Color.red);
        instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
        instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);

        previewMatrix[instance_x, instance_z] = true;
        previewMatrix[instance_x + 1, instance_z + 1] = true;
        previewMatrix[instance_x + 1, instance_z] = true;
        previewMatrix[instance_x, instance_z + 1] = true;
    }
    private void BuildSoldierCampPreview()
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
    private void DestroySoldierCampPreview()
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
                    //if cursor is on the track tile
                    //and there's no preview anywhere, build one.
                    if (propertiesMatrix[x, z].type == "Track")
                    {
                        if (temporaryInstance == null)
                        {
                            Vector2 tempPosition = ReturnFirstFreeTileAround();
                            Vector3 foundPosition = CoordToPosition(Mathf.FloorToInt(tempPosition.x), Mathf.FloorToInt(tempPosition.y));
                            if (foundPosition != CoordToPosition(0, 0))
                            {
                                Instantiate(foundPosition);

                                Debug.Log("GOING TO INSTANTIATE ON " + foundPosition.x + ", " + foundPosition.z);
                            }

                        }
                    }
                    //don't build
                    //ROTATE !
                    if (!rotated)
                    {
                        if (propertiesMatrix[x, z].type == "Track")
                        {
                            SetPreviewColor(Color.green);
                            RotateAccordingly(x, z);
                        }
                        else
                        {
                            SetPreviewColor(Color.red);
                        }
                        
                        rotated = true;
                    }
                }
                //stepped over a TREE tile
                else if (propertiesMatrix[x, z].type == "Tree"
                    || propertiesMatrix[x + 1, z + 1].type == "Tree"
                    || propertiesMatrix[x, z + 1].type == "Tree"
                    || propertiesMatrix[x + 1, z].type == "Tree")
                {
                    //DON'T build and DON'T rotate.
                    SetPreviewColor(Color.red);
                    //DestroySoldierCampPreview();
                }
                else
                {//if the logic doens't involve going over track tiles
                    if (previewMatrix[x, z] == false
                        && previewMatrix[x + 1, z + 1] == false
                        && previewMatrix[x + 1, z] == false
                        && previewMatrix[x, z + 1] == false)
                    {
                        BuildSoldierCampPreview();
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
                        if (propertiesMatrix[x, z].type == "Track")
                        {
                            SetPreviewColor(Color.green);
                            RotateAccordingly(x, z);
                        }
                        else
                        {
                            SetPreviewColor(Color.red);
                            DestroySoldierCampPreview();
                        }
                    }
                    else if (propertiesMatrix[x, z].type == "Tree"
                    || propertiesMatrix[x + 1, z + 1].type == "Tree"
                    || propertiesMatrix[x, z + 1].type == "Tree"
                    || propertiesMatrix[x + 1, z].type == "Tree")
                    {
                        DestroySoldierCampPreview();
                    }
                    else
                    {
                        DestroySoldierCampPreview();
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
                else if (propertiesMatrix[x, z].type == "Tree")
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
                    else if (propertiesMatrix[x, z].type == "Tree")
                    {
                        //don't build
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
        bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);

        if (didHit)
        {

            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            cursor_x = x;
            cursor_z = z;
            position = CoordToPosition(x, z);


            Vector3 positionCube = new Vector3(position.x, position.y + 0.5f, position.z);
            selectionCube.transform.position = positionCube;
            //Debug.Log("TILE: " + x + "," + z + " OF TYPE: " + propertiesMatrix[x, z].type);

            //ONLY BUILD PREVIEWS IF YOU HIT THE GRID
            if (hitInfo.transform.gameObject.name == "Grid")
            { 
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
	}
    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset, -_gridSize.y / 2 + 0.5f + y);
    }

    
}
