using System.Collections;
using UnityEngine;

public class GridMouse : MonoBehaviour {

    public Transform selectionCube;
    private float square_size = 30f / 20f;
    void Start()
    {
        Debug.Log("Gonna run script !!!");
        //GetComponent<Renderer>().material.color = Color.blue;
    }
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            GetComponent<Renderer>().material.color = Color.red;
            Debug.Log(hitInfo.point);
            selectionCube.position = hitInfo.point;
            //Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
	}
}
