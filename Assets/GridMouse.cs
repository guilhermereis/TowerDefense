using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
    private GameObject temporaryInstance;
    private Vector3 position;
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
        //Debug.Log("Gonna run script !!!");

    }
    void OnMouseDown()
    {
        //Debug.Log("Mouse Down");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);
            //Debug.Log("x: " + x + ", z: " + z);
            if (propertiesMatrix[x, z].unit != null)
            {
                buildManager.SelectBuilding(propertiesMatrix[x, z].unit, new Vector2(x,z));
                buildManager.ShowOptions();
                Debug.Log("Selecionou a posição: "+x+", "+z);
                //Destroy(hitInfo.transform.gameObject);
            }
            else
            {
                if (buildManager.getUnitToBuild() != null)
                {
                    buildUnitAndAddItToTheList(position);
                    
                    //Transform newObstacleCube = Instantiate(obstacleCube, position, Quaternion.identity) as Transform;
                    //propertiesMatrix[x, z] = new PropertyScript.Property(buildManager.getUnitToBuild(), ref gameObject, "Obstacle");
                    Debug.Log("Construiu na posição " + x + ", " + z);
                    Debug.Log("Position = "+position);
                }
                else
                {
                    buildManager.HideOptions();
                    Debug.Log("Hide Options");
                }
            }
        }
    }
    public void buildUnitAndAddItToTheList(Vector3 myPosition) {
        ListOfGameObjects.Add(new GameObject());
        int AddedElmtIndex = ListOfGameObjects.Count - 1;

        buildManager.BuildUnitOn(ref ListOfGameObjects, AddedElmtIndex, myPosition);
        ListOfGameObjects[AddedElmtIndex].GetComponent<BuildableController>().setArrayListPosition(AddedElmtIndex);
    }
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            //Debug.Log("Hitou " + hitInfo.transform.gameObject);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);
            //Debug.Log("x: " + x + ", z: " + z);
            //Debug.Log("previewMatrix[x, z] = " + previewMatrix[x, z]);
            Vector3 positionCube = new Vector3(position.x, position.y + 0.5f, position.z);
            selectionCube.transform.position = positionCube;
            if (previousPosition == position)
            {
                
                if (previewMatrix[x, z] == false)
                {

                    temporaryInstance = new GameObject();
                    buildManager.BuildPreviewOn(ref temporaryInstance, position);
                    previewMatrix[x, z] = true;
                    //Debug.Log("construiu preview !");
                }
                
            }
            else
            {
                //Debug.Log("moveu !");
                if (temporaryInstance != null)
                {
                    Destroy(temporaryInstance);
                    
                    previewMatrix[prevX,prevZ] = false;
                                       
                    //Debug.Log("destruiu preview !");
                }
                             
            }
            previousPosition = position;
            prevX = x;
            prevZ = z;
            //previewMatrix[x, z] = true;
            //Transform newSelectionCube = Instantiate(obstaclePrefab, position + Vector3.up * .5f, Quaternion.identity) as Transform;
            //Debug.Log("Property of this tile: "+propertiesMatrix[x,z].type);
        }
            
	}
    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset, -_gridSize.y / 2 + 0.5f + y);
    }

    
}
