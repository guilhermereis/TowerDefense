using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLife : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>().playOnAwake)
        {
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
        }
    }
	

}
