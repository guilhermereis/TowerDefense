using System.Collections.Generic;
using System;
using UnityEngine;

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
    //        8: Coins
    //        9: Projectiles
    //        10:Monsters
    //        11:EdgeTiles
    //        12:RiverColliders

    //ignore layers 8,9,10 and 2 (IgnoreRaycast Layer)
    //(lowest order bit is 0-indexed)
    public static int layerMask = Convert.ToInt32("11111111111111111111000011101001", 2);
    //private int layerMask = ~(1 << 10);

    public static GridMouse instance;
    public GameObject CubeTrack;
    public GameObject CubeTrack2;
    public GameObject CubeTrack3;
    public GameObject Trees;
    public GameObject DesertProps;
    public GameObject SnowTrees;
    public GameObject Edges;
    public GameObject EdgesSnow;
    public GameObject EdgesDesert;
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
    private GameObject temp;
    public bool canClickGrid = true;

    [Header("Cursor")]
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot;
    private Vector2 defaultHotSpot = Vector2.zero;
    private bool isCursorDefault = true;

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
            //Debug.LogError("More than one GridMouse in scene !");
            return;
        }
        instance = this;

        if (cursorTexture)
        {
            hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        }
        else {
            hotSpot = new Vector2(256f, 256f);
        }
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

        ReadSpecialTiles();

    }

    void Start()
    {
        UpdateGrid();
    }
    void ReadSpecialTiles()
    {
        int x;
        int z;
        //Debug.Log("Child Count = " + CubeTrack.transform.childCount);
        foreach (Transform child in CubeTrack.transform)
        {
            //tower
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            if (x == 45 && z == 13)
            {
                //Debug.Log(x + "," + z + " = Track");
            }
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }
        foreach (Transform child in CubeTrack2.transform)
        {
            //tower
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            if (x == 45 && z == 13)
            {
               // Debug.Log(x + "," + z + " = Track");
            }
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }
        foreach (Transform child in CubeTrack3.transform)
        {
            //tower
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            if (x == 45 && z == 13)
            {
                //Debug.Log(x + "," + z + " = Track");
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
        foreach (Transform child in DesertProps.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        foreach (Transform child in SnowTrees.transform)
        {
            //Debug.Log(child.name + " IS GONNA CRASH !!!");
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
            
        }
        foreach (Transform child in Edges.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            if (x == 31 && z == 29)
            {
               // Debug.Log("31, 29 = Edges");
            }
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        foreach (Transform child in EdgesSnow.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        foreach (Transform child in EdgesDesert.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        Transform EdgesVolcanic = GameObject.Find("EdgesVolcanic").transform;
        foreach (Transform child in EdgesVolcanic.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        Transform RockBlockers = GameObject.Find("RockBlockers").transform;
        foreach (Transform child in RockBlockers.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        Transform RockBlockersLava = GameObject.Find("RockBlockersLava").transform;
        foreach (Transform child in RockBlockersLava.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        Transform BlackAreaBlockers = GameObject.Find("BlackAreaBlockers").transform;
        foreach (Transform child in BlackAreaBlockers.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }

        Transform CubeTrack4 = GameObject.Find("CubeTrack4").transform;
        foreach (Transform child in CubeTrack4.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }
        Transform FireTiles = GameObject.Find("FireTiles").transform;
        foreach (Transform child in FireTiles.transform)
        {

            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            propertiesMatrix[x, z] = new PropertyScript.Property("Tree");
        }
        Destroy(CubeTrack);
        Destroy(CubeTrack2);
        Destroy(CubeTrack3);
        Destroy(CubeTrack4.gameObject);
        Destroy(Edges);
        Destroy(EdgesSnow);
        Destroy(EdgesDesert);
        Destroy(EdgesVolcanic.gameObject);
        Destroy(RockBlockers.gameObject);
        Destroy(RockBlockersLava.gameObject);
        Destroy(BlackAreaBlockers.gameObject);
        Destroy(FireTiles.gameObject);
        //Destroy(Trees);
    }
    //HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    private bool CheckIfHitStructure()
    {
        BuildableController bc = hitInfo.transform.gameObject.GetComponent<BuildableController>();
        if (bc == null)
            return false;
        UnitBlueprint unit = bc.getUnitBlueprint();
        return
            (unit.name == Shop.instance.towerLevel1.name
            || unit.name == Shop.instance.miningCamp.name
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
                                Quaternion rotation = temporaryInstance.transform.rotation;
                                int added_index = buildUnitAndAddItToTheList(newPosition,rotation, false);
                                Destroy(temporaryInstance);
                                //int added_index = buildUnitAndAddItToTheList(position);
                                propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                if (buildManager.getUnitToBuild() == Shop.instance.miningCamp)
                                {
                                    propertiesMatrix[x + 1, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                    propertiesMatrix[x, z + 1] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                    propertiesMatrix[x + 1, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                                }
                                //Debug.Log("Construiu na posição " + x + ", " + z);
                                //Debug.Log("Position = " + position);
                            }
                            else
                            {
                                DestroySoldierCampPreview();
                            }
                        }
                    }
                }
            }
        }
        SetPreviewColor(Color.blue);
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
    
    private void HandleBuildingTower(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        
        Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);

            
        //If I hit the Grid
        if (hitInfo.transform.gameObject.name == "Grid")
        {

            if (temporaryInstance != null)
            {
                position = temporaryInstance.transform.position;
                x = Mathf.FloorToInt(position.x + _gridSize.x / 2);
                z = Mathf.FloorToInt(position.z + _gridSize.y / 2);


                {
                    if (buildManager.getUnitToBuild() != null)
                    {
                        if (CheckIfGameObjectIsOfColor(Color.green))
                        {
                            int added_index = buildUnitAndAddItToTheList(position, false);
                            Destroy(temporaryInstance);
                            propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                            //Debug.Log("Construiu na posição " + x + ", " + z);
                            // Debug.Log("Position = " + position);
                        }
                        else
                        {
                            DestroyTowerPreview();
                        }
                    }
                }
            }
            
        }

        SetPreviewColor(Color.blue);
    }
    public void SelectPosition(UnitBlueprint unit, GameObject gameObject)
    {
        buildManager.SelectBuilding(unit, gameObject);
        BuildManager.instance.ShowOptions();
       // Debug.Log("AAAAAA" + gameObject.GetComponent<TowerController>().getFireRate());
       // Debug.Log("AAAAAA" + gameObject.GetComponent<TowerController>().getAttackPower());
    }

    void OnMouseDown()
    {
        if (canClickGrid)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
            if (didHit)
            {
               // Debug.Log("Just hit: " + hitInfo.transform.gameObject.name);
            }
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            PropertyScript.Property propertyInQuestion = propertiesMatrix[x, z];

            if (propertyInQuestion.unit != null) // If the tile contains a Structure
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("DESELECT 1");
                    buildManager.DeselectUnitToBuild();
                    buildManager.DeselectSelectedUnit();
                    SelectPosition(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
                }
            }
            else if (CheckIfHitStructure()) // If I hit a Structure
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    BuildableController buildable = hitInfo.transform.gameObject.GetComponent<BuildableController>();
                    buildManager.SelectBuilding(buildable.getArrayListPosition());
                    BuildManager.instance.ShowOptions();
                }
            }
            else if (propertyInQuestion.type == "Tree") // If I hit a Tree
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    BuildManager.instance.HideOptions();
                }
                //IGNORE THE CLICK
            }
            else // Decide to Build something
            {
                if (buildManager.getUnitToBuild() == Shop.instance.miningCamp)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                         HandleBuildingSoldierCamp(ray, hitInfo, didHit, x, z);
                         buildManager.DeselectUnitToBuild();
                        //after building the soldier camp, revert to normal scale
                        selectionCube.transform.localScale = new Vector3(1f, 0.01f, 1f);
                    }
                }
                else if (buildManager.getUnitToBuild() == Shop.instance.towerLevel1)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        //Debug.Log("DESELECT 2");
                        
                        HandleBuildingTower(ray, hitInfo, didHit, x, z);
                        buildManager.DeselectUnitToBuild();
                        
                    }
                }
                else //if there's nothing to build, then hide the options
                {
                    //this if here makes sense, because when clicking over menus we dont want to
                    //hide the options.
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildManager.instance.HideOptions();

                    }
                }
            }
        }
    }
    public Vector3 getPreviewRotation()
    {
        return this.rotation;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition, bool upgraded) {
        ListOfGameObjects.Add(new GameObject("UnitGameObject"));
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition, upgraded);
        return AddedElmtIndex;
    }
    public int buildUnitAndAddItToTheList(Vector3 myPosition, Quaternion rotation, bool upgraded = false)
    {
        ListOfGameObjects.Add(new GameObject("UnitGameObject"));
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition,rotation, upgraded);
        return AddedElmtIndex;
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
        if (temporaryInstance)
        {
            foreach (Material matt in temporaryInstance.GetComponent<MeshRenderer>().materials)
            {
                //Debug.Log("SETTING matt: " + matt.name);
                matt.SetColor("_Color", color);
            }
        }
        selectionCube.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
    private void Instantiate(Vector3 pos)
    {
        temporaryInstance = buildManager.BuildPreviewOn(new GameObject("PreviewGameObject"), pos);
        rotated = false;
        SetPreviewColor(Color.green);
        instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
        instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);

        previewMatrix[instance_x, instance_z] = true;
        previewMatrix[instance_x + 1, instance_z + 1] = true;
        previewMatrix[instance_x + 1, instance_z] = true;
        previewMatrix[instance_x, instance_z + 1] = true;
    }
    private void BuildSoldierCampPreview()
    {
        temporaryInstance = buildManager.BuildPreviewOn(new GameObject("PreviewGameObject"), position);
        rotated = false;
        SetPreviewColor(Color.green);
        instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
        instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);

        previewMatrix[instance_x, instance_z] = true;
        previewMatrix[instance_x + 1, instance_z + 1] = true;
        previewMatrix[instance_x + 1, instance_z] = true;
        previewMatrix[instance_x, instance_z + 1] = true;
        //Debug.Log("construiu preview !");
    }
    private void DestroyTowerPreview()
    {
        if (temporaryInstance)
        {
            int instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x + _gridSize.x / 2);
            int instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z + _gridSize.y / 2);
            previewMatrix[instance_x, instance_z] = false;
            Destroy(temporaryInstance);
            selectionCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
    
    private void DestroySoldierCampPreview()
    {
        if (temporaryInstance)
        {
            //Debug.Log("DESTROYING SOLDIER CAMP PREVIEW");
            //if the logic doens't involve going over track tiles
            SetPreviewColor(Color.red);
            instance_x = Mathf.FloorToInt(temporaryInstance.transform.position.x - 0.5f + _gridSize.x / 2);
            instance_z = Mathf.FloorToInt(temporaryInstance.transform.position.z - 0.5f + _gridSize.y / 2);
            previewMatrix[instance_x, instance_z] = false;
            previewMatrix[instance_x + 1, instance_z + 1] = false;
            previewMatrix[instance_x + 1, instance_z] = false;
            previewMatrix[instance_x, instance_z + 1] = false;
            Destroy(temporaryInstance);
            selectionCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }

    private void HandlePreviewSoldierCamp(Ray ray, RaycastHit hitInfo, bool didHit,int x, int z)
    {
        if (didHit)
        {
            if (previousPosition == position)
            {

                //stepped over a NORMAL tile
                if (propertiesMatrix[x, z].type == "Normal"
                    && propertiesMatrix[x + 1, z + 1].type == "Normal"
                    && propertiesMatrix[x, z + 1].type == "Normal"
                    && propertiesMatrix[x + 1, z].type == "Normal")
                {
                    if (previewMatrix[x, z] == false
                        && previewMatrix[x + 1, z + 1] == false
                        && previewMatrix[x + 1, z] == false
                        && previewMatrix[x, z + 1] == false)
                    {
                        BuildSoldierCampPreview();
                        SetPreviewColor(Color.green);
                    }
                }
                else
                {
                    if (previewMatrix[x, z] == false
                        && previewMatrix[x + 1, z + 1] == false
                        && previewMatrix[x + 1, z] == false
                        && previewMatrix[x, z + 1] == false)
                    {
                        BuildSoldierCampPreview();
                        SetPreviewColor(Color.red);
                    }
                }
            }
            else
            {
                //Debug.Log("moveu !");
                if (temporaryInstance != null)
                {
                    DestroySoldierCampPreview();
                }

            }
            previousPosition = position;
            prevX = x;
            prevZ = z;
        }
    }
    private void HandlePreviewTower(Ray ray, RaycastHit hitInfo, bool didHit, int x, int z)
    {
        if (didHit)
        {
            if (previousPosition == position)
            {
                //stepped over a track tile
                if (propertiesMatrix[x, z].type == "Normal")
                {//if the logic doens't involve going over track tiles
                    if (previewMatrix[x, z] == false)
                    {

                        temporaryInstance = buildManager.BuildPreviewOn((temporaryInstance == null) ? new GameObject("PreviewGameObject") : temporaryInstance, position);
                        SetPreviewColor(Color.green);
                        previewMatrix[x, z] = true;
                        //Debug.Log("construiu preview !");
                    }
                }
                else
                {
                    if (previewMatrix[x, z] == false)
                    {
                        
                        temporaryInstance = buildManager.BuildPreviewOn((temporaryInstance == null) ? new GameObject("PreviewGameObject") : temporaryInstance, position);
                        SetPreviewColor(Color.red);
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
                    DestroyTowerPreview();

                    //Debug.Log("destruiu preview !");
                }

            }
            previousPosition = position;
            prevX = x;
            prevZ = z;
        }
    }

	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            //Deselect via ESC
            if (temporaryInstance && temporaryInstance.name == "MinePrefab(Clone)")
            {
                DestroySoldierCampPreview();
            }
            else
            {
                DestroyTowerPreview();
            }
            buildManager.DeselectUnitToBuild();
            //after building the soldier camp, revert to normal scale
            selectionCube.transform.localScale = new Vector3(1f, 0.01f, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PlayerStats.SetMoney(0);
        }
        else if (didHit)
        {

            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            cursor_x = x;
            cursor_z = z;
            position = CoordToPosition(x, z);

            Vector3 positionCube = new Vector3(position.x+0.1f, position.y + 0.1f, position.z - 0.1f);
            selectionCube.transform.position = positionCube;
            //Debug.Log("TILE: " + x + "," + z + " OF TYPE: " + propertiesMatrix[x, z].type);

            if (cursorTexture)
            {
                if (propertiesMatrix[x, z].type == "Obstacle")
                {
                    if (isCursorDefault) {
                        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                        isCursorDefault = !isCursorDefault;
                    }
                    
                }
                else
                {
                    if (!isCursorDefault) {
                        Cursor.SetCursor(null, defaultHotSpot, cursorMode);
                        isCursorDefault = !isCursorDefault;
                    }
                    
                }
            }

            //ONLY BUILD PREVIEWS IF YOU HIT THE GRID
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                if (buildManager.getUnitToBuild() == Shop.instance.miningCamp)
                {
                    Vector3 newPositionCube = new Vector3(position.x+0.5f, position.y, position.z+0.5f);
                    selectionCube.transform.position = newPositionCube;
                    selectionCube.transform.localScale = new Vector3(2f, 0.01f, 2f);
                    HandlePreviewSoldierCamp(ray, hitInfo, didHit, x, z);
                }
                else if (buildManager.getUnitToBuild() == Shop.instance.towerLevel1)
                {
                    Vector3 newPositionCube = new Vector3(position.x, position.y + 0.1f, position.z);
                    selectionCube.transform.position = newPositionCube;
                    selectionCube.transform.localScale = new Vector3(1f, 0.01f, 1f);
                    HandlePreviewTower(ray, hitInfo, didHit, x, z);
                }
            }
            else
            {
                //Debug.Log("JUST HIT: " + hitInfo.transform.gameObject.name);
            }
        }
	}
    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset, -_gridSize.y / 2 + 0.5f + y);
    }
    public Vector3 PositionToCoord(int x, int y)
    {
        return new Vector3(_gridSize.x / 2 - 0.5f + x + 1, 0f - ZOffset, _gridSize.y / 2 - 0.5f + y + 1);
    }
    
}
