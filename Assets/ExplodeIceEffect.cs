﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIceEffect : MonoBehaviour {

   
    public GameObject target;
    public bool exploded;
    private AudioSource explodeIceSound;
	// Use this for initialization
	void Start () {


        explodeIceSound = GetComponent<AudioSource>();
        SoundToPlay.PlayAtLocation(explodeIceSound,transform.position,Quaternion.identity,0.2f, 10f);
        Destroy(gameObject, 1.2f);
	}
	
    public void ExplodeIce()
    {
        exploded = true;
        float impulseFactor = 2;
        Rigidbody[] rigs = GetComponentsInChildren<Rigidbody>();
        //Vector3 dir = (rigs[0].gameObject.transform.localPosition).normalized * impulseFactor;
        //rigs[0].AddForceAtPosition(dir, transform.localPosition, ForceMode.Impulse);
        //rigs[0].AddForce(dir, ForceMode.Impulse);
        //rigs[0].AddExplosionForce(impulseFactor, transform.position, 100f, 1.5f);
        //foreach (Rigidbody r in rigs)
        //{
        //    Vector3 dir = (r.gameObject.transform.localPosition).normalized * impulseFactor;
        //    //r.AddForce(dir, ForceMode.Impulse);
        //    r.velocity = dir;
        //}

    }

	
}
