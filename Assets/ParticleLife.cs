﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLife : MonoBehaviour {
    public ParticleSystem thisParticleSystem; 
	// Use this for initialization
	void Start () {
        thisParticleSystem = this.GetComponent<ParticleSystem>();
        if (!thisParticleSystem.main.loop)
        {
            Destroy(gameObject, 3);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
}
