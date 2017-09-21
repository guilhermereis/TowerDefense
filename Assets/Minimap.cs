using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject square;
    public GameObject diagonal_grid;
    public GameObject monster;
    private static int index = 0;
    public static GameObject[] allSquares;
    static Vector2 centerOfMinimap;
    static Vector3 vector;
    static Vector2 new_vector;
    static float scale;
    public static List<GameObject> monsterBatch;
    // Use this for initialization
    void Start () {
        UpdateMap();
        allSquares = new GameObject[100];
        
    }
    public void addNewSquare()
    {
        //Transform newSelectionCube = 
        allSquares[index] = Instantiate(square, centerOfMinimap + new_vector, Quaternion.identity);
        allSquares[index].transform.parent = transform;
        index++;
    }

    public void UpdateMap()
    {
        //Camera.main.WorldToScreenPoint()
        Debug.Log("GOING TO MOVE " + square);
        centerOfMinimap = new Vector2(790 / 2 + 320, 369 / 2 + 60);
        square.GetComponent<RectTransform>().position = centerOfMinimap;
        float diagonal_minimap = this.GetComponent<RectTransform>().rect.width;
        scale = diagonal_grid.GetComponent<Collider>().bounds.size.x / diagonal_minimap;
        //scale += 0.45f;
        Debug.Log("Scale = " + scale);


        vector = monster.transform.position * scale;
        new_vector = new Vector2(vector.x, vector.z);
        square.transform.position = centerOfMinimap + new_vector;


    }
    public void UpdateMonsterBatch()
    {
        monsterBatch = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().monsterBatch;
        Debug.Log("ADDING NEW SQUARE");
        addNewSquare();
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("MonsterBatch.SIZE = " + monsterBatch.Count);
        for (int i = 0; i < monsterBatch.Count; i++)
        {
            vector = monsterBatch[i].transform.position * scale;
            new_vector = new Vector2(vector.x, vector.z);
            allSquares[i].transform.position = centerOfMinimap + new_vector;
        }
	}
}
