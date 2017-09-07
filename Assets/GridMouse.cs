using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TileMap))]
public class GridMouse : MonoBehaviour {


    public static GridMouse instance;
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
    public static List<T> CloneList<T>(List<T> oldList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, oldList);
        stream.Position = 0;
        return (List<T>)formatter.Deserialize(stream);
    }

    void Start()
    {
        UpdateGrid();
        ReadSpecialTiles();

    }
    void ReadSpecialTiles()
    {
        GameObject Track = GameObject.Find("CubeTrack");
        int x;
        int z;
        Debug.Log("Child Count = " + Track.transform.childCount);
        foreach (Transform child in Track.transform)
        {
            //tower
            x = Mathf.FloorToInt(child.transform.position.x + _gridSize.x / 2);
            z = Mathf.FloorToInt(child.transform.position.z + _gridSize.y / 2);
            //Vector3 position = CoordToPosition(x, z);
            Debug.Log(x + "," + z + " = Track");
            propertiesMatrix[x, z] = new PropertyScript.Property("Track");
        }
        Track.SetActive(false);
    }
    private void HandleBuildingSoldierCamp()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo, Mathf.Infinity);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())

        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);


            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);
            if (temporaryInstance != null)
            {
                position = temporaryInstance.transform.position;
                x = Mathf.FloorToInt(position.x + _gridSize.x / 2);
                z = Mathf.FloorToInt(position.z + _gridSize.y / 2);
            }
            //Debug.Log("x: " + x + ", z: " + z);


            //If I hit a Tower
            if (hitInfo.transform.gameObject.name == "Tower(Clone)")
            {
                BuildableController buildable = hitInfo.transform.gameObject.GetComponent<BuildableController>();
                buildManager.SelectBuilding(buildable.getArrayListPosition());
                buildManager.ShowOptions();
            }
            //If I hit the Grid
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                PropertyScript.Property propertyInQuestion = propertiesMatrix[x, z];
                if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
                {
                    if (propertiesMatrix[x, z].unit != null)
                        propertyInQuestion = propertiesMatrix[x, z];
                    else if (propertiesMatrix[x + 1, z + 1].unit != null)
                        propertyInQuestion = propertiesMatrix[x + 1, z + 1];
                    else if (propertiesMatrix[x, z + 1].unit != null)
                        propertyInQuestion = propertiesMatrix[x, z + 1];
                    else if (propertiesMatrix[x + 1, z].unit != null)
                        propertyInQuestion = propertiesMatrix[x + 1, z];
                }

                if (propertyInQuestion.unit != null)
                {
                    buildManager.SelectBuilding(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
                    buildManager.ShowOptions();
                    Debug.Log("Selecionou a posição: " + x + ", " + z);
                    //Destroy(hitInfo.transform.gameObject);
                }
                else
                {
                    if (buildManager.getUnitToBuild() != null)
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
                    else
                    {
                        buildManager.HideOptions();
                        Debug.Log("Hide Options");
                    }
                }
            }
        }
    }
    private void HandleBuildingTower()
    {
        //Debug.Log("Mouse Down");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo, Mathf.Infinity);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())

        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);
            if (temporaryInstance != null)
            {
                position = temporaryInstance.transform.position;
                x = Mathf.FloorToInt(position.x + _gridSize.x / 2);
                z = Mathf.FloorToInt(position.z + _gridSize.y / 2);
            }
            //Debug.Log("x: " + x + ", z: " + z);


            //If I hit a Tower
            if (hitInfo.transform.gameObject.name == "Tower(Clone)")
            {
                BuildableController buildable = hitInfo.transform.gameObject.GetComponent<BuildableController>();
                buildManager.SelectBuilding(buildable.getArrayListPosition());
                buildManager.ShowOptions();
            }
            //If I hit the Grid
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                PropertyScript.Property propertyInQuestion = propertiesMatrix[x, z];
                if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
                {
                    if (propertiesMatrix[x, z].unit != null)
                        propertyInQuestion = propertiesMatrix[x, z];
                    else if (propertiesMatrix[x + 1, z + 1].unit != null)
                        propertyInQuestion = propertiesMatrix[x + 1, z + 1];
                    else if (propertiesMatrix[x, z + 1].unit != null)
                        propertyInQuestion = propertiesMatrix[x, z + 1];
                    else if (propertiesMatrix[x + 1, z].unit != null)
                        propertyInQuestion = propertiesMatrix[x + 1, z];
                }

                if (propertyInQuestion.unit != null)
                {
                    buildManager.SelectBuilding(propertyInQuestion.unit, propertyInQuestion.builtGameObject);
                    buildManager.ShowOptions();
                    Debug.Log("Selecionou a posição: " + x + ", " + z);
                    //Destroy(hitInfo.transform.gameObject);
                }
                else
                {
                    if (buildManager.getUnitToBuild() != null)
                    {
                        int added_index = buildUnitAndAddItToTheList(position);
                        Destroy(temporaryInstance);
                        propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref ListOfGameObjects, added_index, "Obstacle");
                        Debug.Log("Construiu na posição " + x + ", " + z);
                        Debug.Log("Position = " + position);
                    }
                    else
                    {
                        buildManager.HideOptions();
                        Debug.Log("Hide Options");
                    }
                }
            }
        }
    }
    void OnMouseDown()
    {
        //Debug.Log("Mouse Down");
        if (buildManager.getUnitToBuild() == Shop.instance.missileLauncher)
        {
            HandleBuildingSoldierCamp();
        }
        else if (buildManager.getUnitToBuild() == Shop.instance.standardUnit)
        {
            HandleBuildingTower();
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
    void RotateAccordingly(int x, int z)
    {
            if (z > instance_z + 1)
            {
                Debug.Log("Rotate up from " + rotation);
                rotation = new Vector3(-90, 180, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (x > instance_x + 1)
            {
                Debug.Log("Rotate right from " + rotation);
                rotation = new Vector3(-90, -90, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (x < instance_x)
            {
                Debug.Log("Rotate left from " + rotation);
                rotation = new Vector3(-90,90, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
            }
            else if (z < instance_z)
            {
                Debug.Log("Rotate down from " + rotation);
                rotation = new Vector3(-90, 0, 0);
                Debug.Log("New rotation = " + rotation);
                temporaryInstance.transform.rotation = Quaternion.Euler(rotation);
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

                        temporaryInstance = new GameObject();
                        buildManager.BuildPreviewOn(ref temporaryInstance, position);
                        rotated = false;

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
                    }
                    else
                    {
                        //if the logic doens't involve going over track tiles

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

            Debug.Log("TILE: " + x + "," + z + " OF TYPE: " + propertiesMatrix[x, z].type);
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

                        temporaryInstance = new GameObject();
                        buildManager.BuildPreviewOn(ref temporaryInstance, position);
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
            Debug.Log("Property of this tile: " + propertiesMatrix[x, z].type);
        }
    }

	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity);

        if (didHit)
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            //Debug.Log("Hitou " + hitInfo.transform.gameObject);
            int x = Mathf.FloorToInt(hitInfo.point.x + 0.5f + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + 0.5f + _gridSize.y / 2);
            position = CoordToPosition(x, z);


            //Debug.Log("x: " + x + ", z: " + z);
            //Debug.Log("previewMatrix[x, z] = " + previewMatrix[x, z]);
            Vector3 positionCube = new Vector3(position.x, position.y + 0.5f, position.z);
            selectionCube.transform.position = positionCube;

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
