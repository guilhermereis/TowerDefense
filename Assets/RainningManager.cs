using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainningManager : MonoBehaviour {


    private ParticleSystem raining;
    public List<GameObject> lightningStrikeEffects;
    float spawnRadius;
    float timeToStrike = 10f;
    float countdown = 0;
    

	// Use this for initialization
	void Start () {
        raining = GetComponentInChildren<ParticleSystem>();
        //raining.shape.radius;
        countdown = timeToStrike;
	}
	
    public void Strike()
    {
        int start = 0;
        int end = lightningStrikeEffects.Count - 1;
        int striker = Random.Range(start, end);
        //lightningStrikeEffects[striker].SetActive(true);
        if (lightningStrikeEffects[striker].GetComponent<ParticleSystem>() != null)
        {
            lightningStrikeEffects[striker].GetComponent<ParticleSystem>().Play();
            lightningStrikeEffects[striker].GetComponent<AudioSource>().Play();
        }
    }

	// Update is called once per frame
	void Update () {
		
        if(countdown <=0)
        {
            Strike();
            countdown = timeToStrike;
        }

        countdown -= Time.deltaTime;

	}
}
