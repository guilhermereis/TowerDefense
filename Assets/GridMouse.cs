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

    [SerializeField]
    public Property[,] propertiesMatrix;

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

        _transform.position = new Vector3(0f, ZOffset, 0f);
        _transform.localScale = new Vector3(_gridSize.x, _gridSize.y, 1.0f);

        //_material.SetTextureScale("_MainTex", new Vector2(_columns, _rows));
        _material.SetTextureScale("_MainTex", new Vector2(_gridSize.x, _gridSize.y));

        propertiesMatrix = new Property[Mathf.FloorToInt(_gridSize.x),Mathf.FloorToInt(_gridSize.y)];

    }

    void Start()
    {
        UpdateGrid();
        Debug.Log("Gonna run script !!!");

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
            Transform newObstacleCube = Instantiate(obstacleCube, position, Quaternion.identity) as Transform;
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
            Vector3 position = CoordToPosition(x, z);
            Debug.Log("x: " + x + ", z: " + z);
            selectionCube.transform.position = position;
            //Transform newSelectionCube = Instantiate(obstaclePrefab, position + Vector3.up * .5f, Quaternion.identity) as Transform;
        }
            
	}
    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_gridSize.x / 2 + 0.5f + x, 0f + ZOffset + 0.5f, -_gridSize.y / 2 + 0.5f + y);
    }

    public struct Property
    {
        string type;

        public Property(string _type = "Normal")
        {
            type = _type;
        }
    }

}
