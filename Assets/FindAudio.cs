using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAudio : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        AudioSource[] myListeners = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < myListeners.Length; i++)
        {
            
            Debug.Log("Found:  " + myListeners[i].gameObject, myListeners[i].gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
