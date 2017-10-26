using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource snowlandsAmbientSound;
    public AudioSource grasslandsAmbientSound;
    public AudioSource desertAmbientSound;
    public AudioSource vulcanoLandAmbientSound;


    // Use this for initialization
    void Start()
    {
        SoundToPlay.PlayMusic(snowlandsAmbientSound);
        SoundToPlay.PlayMusic(grasslandsAmbientSound);
        SoundToPlay.PlayMusic(desertAmbientSound);
        SoundToPlay.PlayMusic(vulcanoLandAmbientSound);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
