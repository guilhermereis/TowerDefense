using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TileMap))]
public class GridMouse : MonoBehaviour {


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

    [SerializeField]
    public Property[,] propertiesMatrix;

    [SerializeField]
    public bool[,] previewMatrix;

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

    public void UpdateGrid()
    {

        buildManager = BuildManager.instance;
        _transform.position = new Vector3(0f, ZOffset, 0f);
        _transform.localScale = new Vector3(_gridSize.x, _gridSize.y, 1.0f);

        //_material.SetTextureScale("_MainTex", new Vector2(_columns, _rows));
        _material.SetTextureScale("_MainTex", new Vector2(_gridSize.x, _gridSize.y));

        propertiesMatrix = new Property[Mathf.FloorToInt(_gridSize.x),Mathf.FloorToInt(_gridSize.y)];
        previewMatrix = new bool[Mathf.FloorToInt(_gridSize.x), Mathf.FloorToInt(_gridSize.y)];

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
        //Debug.Log("Gonna run script !!!");

    }
    void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.blue);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            Vector3 position = CoordToPosition(x, z);
            Debug.Log("x: " + x + ", z: " + z);
            buildManager.BuildUnitOn(position);
            //Transform newObstacleCube = Instantiate(obstacleCube, position, Quaternion.identity) as Transform;
            propertiesMatrix[x, z] = new Property("Obstacle");
        }
    }
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
            int x = Mathf.FloorToInt(hitInfo.point.x + _gridSize.x / 2);
            int z = Mathf.FloorToInt(hitInfo.point.z + _gridSize.y / 2);
            position = CoordToPosition(x, z);
            //Debug.Log("x: " + x + ", z: " + z);
            Debug.Log("previewMatrix[x, z] = " + previewMatrix[x, z]);
            selectionCube.transform.position = position;
            if (previousPosition == position)
            {
                
                if (previewMatrix[x, z] == false)
                {

                    temporaryInstance = new GameObject();
                    buildManager.BuildPreviewOn(ref temporaryInstance, position);
                    previewMatrix[x, z] = true;
                    Debug.Log("construiu preview !");
                }
                
            }
            else
            {
                //Debug.Log("moveu !");
                if (temporaryInstance != null)
                {
                    Destroy(temporaryInstance);
                    
                    previewMatrix[prevX,prevZ] = false;
                                       
                    Debug.Log("destruiu preview !");
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
    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset + 0.5f, -_gridSize.y / 2 + 0.5f + y);
    }

    [System.Serializable]
    public struct Property
    {
        public string type;

        public Property(string _type = "Normal")
        {
            type = _type;
        }
    }

}
