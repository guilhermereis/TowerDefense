using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheBigEar : MonoBehaviour {

    private Ray ray;
    private RaycastHit hitInfo;
    private GameObject CameraReference;
    public GameObject TheBigEar;
    private Collider collider;

    // Use this for initialization
    void Start () {
        CameraReference = GameObject.Find("Main Camera");
        collider = GridMouse.instance.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Vector3.forward);
        //bool didHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity);
        bool didHit = Physics.Raycast(CameraReference.transform.position, CameraReference.transform.forward,out hitInfo,Mathf.Infinity,GridMouse.layerMask);
        if (didHit)
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.yellow);
            Vector3 pos = hitInfo.transform.position;
            if (hitInfo.transform.gameObject.name == "Grid")
            {
                TheBigEar.transform.position = new Vector3(pos.x,pos.y,pos.z);
            }
        }

    }
}
