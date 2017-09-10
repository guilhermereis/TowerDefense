using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        parent.GetComponent<BuildableController>().OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        parent.GetComponent<BuildableController>().OnTriggerExit(other);
    }
}
