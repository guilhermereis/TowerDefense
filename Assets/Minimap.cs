using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject square; 
	// Use this for initialization
	void Start () {
        UpdateMap();
    }
    public void UpdateMap()
    {
        Debug.Log("GOING TO MOVE " + square);
        Matrix4x4 matrix = square.GetComponent<RectTransform>().worldToLocalMatrix;

        square.GetComponent<RectTransform>().localScale = this.transform.localScale;
        square.GetComponent<RectTransform>().position = matrix.MultiplyPoint(new Vector3(0, 0, 0));
        Debug.Log("Set!!!!");
    }


	// Update is called once per frame
	void Update () {
        //square.transform.position;
        	
	}
}
