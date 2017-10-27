using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour {

    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }


	// Use this for initialization
	void Start () {
        
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        //Any other settings you want to initialize...
        button.onClick.AddListener(() => PlaySound());	
	}

    void PlaySound()
    {
        SoundToPlay.PlaySfx(source);
        //source.PlayOneShot();
    }
}
