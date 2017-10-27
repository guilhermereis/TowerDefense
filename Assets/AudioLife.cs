using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLife : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>().playOnAwake)
        {
            Debug.Log(gameObject + " duration " + GetComponent<AudioSource>().clip.length);
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
        }
        else
        {
            Debug.Log(gameObject + " duration " + GetComponent<AudioSource>().clip.length);
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
        }
    }
	

}
