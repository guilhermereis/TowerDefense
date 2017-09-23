using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public Vector3 mapOffset;
    public float rotation = 0f;

    // Use this for initialization
    void Start()
    {
        mapOffset = new Vector3(-5f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
       //transform.rotation *= Quaternion.Euler(Vector3.forward * 20*Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelRect.size.x, 0f)) + mapOffset;
    }
}
