using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
    public GameObject parent;
    private BuildableController bc;
    private void OnTriggerEnter(Collider other)
    {
        if (parent != null && other != null)
        {
            bc = parent.GetComponent<BuildableController>();
            if (bc != null)
                bc.OnTriggerEnter(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (parent != null && other != null)
        {
            bc = parent.GetComponent<BuildableController>();
            if (bc != null)
                bc.OnTriggerEnter(other);
        }    

    }
}
