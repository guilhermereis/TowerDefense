using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject square;
    public GameObject diagonal_grid;
    public GameObject monster;
	// Use this for initialization
	void Start () {
        UpdateMap();
    }
    public void UpdateMap()
    {
        //Camera.main.WorldToScreenPoint()
        Debug.Log("GOING TO MOVE " + square);
        Vector2 centerOfMinimap = new Vector2(790 / 2 + 320, 369 / 2 + 60);
        square.GetComponent<RectTransform>().position = centerOfMinimap;
        float diagonal_minimap = this.GetComponent<RectTransform>().rect.width;
        float scale = diagonal_grid.GetComponent<Renderer>().bounds.size.x / diagonal_minimap;
        Debug.Log("Scale = " + scale);


        Vector3 vector = monster.transform.position * scale;
        Vector2 new_vector = new Vector2(vector.x, vector.z);
        square.transform.position = centerOfMinimap + new_vector;


    }


	// Update is called once per frame
	void Update () {
        //square.transform.position;
        UpdateMap();
	}
}
