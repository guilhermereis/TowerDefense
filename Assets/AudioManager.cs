using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource snowlandsAmbientSound;
    public AudioSource grasslandsAmbientSound;
    public AudioSource desertAmbientSound;
    public AudioSource vulcanoLandAmbientSound;
    public AudioSource backgroundMusic;

    // Use this for initialization
    void Start()
    {
        SoundToPlay.PlayMusic(snowlandsAmbientSound, 0.3f);
        SoundToPlay.PlayMusic(grasslandsAmbientSound, 0.2f);
        SoundToPlay.PlayMusic(desertAmbientSound, 0.3f);
        SoundToPlay.PlayMusic(vulcanoLandAmbientSound);
       // SoundToPlay.PlayMusic(backgroundMusic, 0.25f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
