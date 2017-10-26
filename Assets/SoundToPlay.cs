using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundToPlay : MonoBehaviour
{
    static GameObject soundObject;
    static AudioSource audioSource;
    static float sfx_volume;
    static float music_volume;
    static float master_volume;
    static List<AudioSource> BGs = new List<AudioSource>();

    private void Awake()
    {
        ConfigurationMenu.soundSliderDelegate += volumesChangedDelegate;
    }

    private void OnDestroy()
    {
        ConfigurationMenu.soundSliderDelegate -= volumesChangedDelegate;
    }

    void volumesChangedDelegate() {
        SetAllVolumes();
        List<AudioSource> audiosToRemove = new List<AudioSource>();
        BGs = BGs.Where(audio => audio != null).ToList();

        foreach (AudioSource audio in BGs)
        {
            if(audio){
                if (audio.isPlaying)
                {
                    audio.volume = PlayerPrefs.GetFloat("music volume");
                }
            }
        }
    }

    public static void SetSoundToPlay(GameObject _soundObject)
    {
        soundObject = _soundObject;
        float master_volume = PlayerPrefs.GetFloat("master volume");
        SetAllVolumes();
        SetGlobalVolume(master_volume);
    }

    public static void SetSoundToPlay(AudioSource _audioSource)
    {
        audioSource = _audioSource;
        float master_volume = PlayerPrefs.GetFloat("master volume");
        SetAllVolumes();
        SetGlobalVolume(master_volume);
    }

    public static void SetAllVolumes()
    {
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        master_volume = PlayerPrefs.GetFloat("master volume");
    }

    public static void SetGlobalVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

    public static void PlayMusic(GameObject _soundObject)
    {
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        src.volume = music_volume;
        BGs.Add(src);
        MonoBehaviour.Instantiate(soundObject);
    }

    public static void PlayMusic(AudioSource _audioSource)
    {
        SetSoundToPlay(_audioSource);
        audioSource.volume = music_volume;
        BGs.Add(audioSource);
        audioSource.Play();
    }

    public static void PlaySfx(GameObject _soundObject)
    {
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        src.volume = sfx_volume;
        MonoBehaviour.Instantiate(soundObject);
    }

    public static void PlaySfx(AudioSource _audioSource)
    {
        SetSoundToPlay(_audioSource);
        audioSource.volume = sfx_volume;
        audioSource.Play();
    }

    public static void PlayAtLocation(GameObject soundObject, Vector3 position, Quaternion rotation)
    {
        MonoBehaviour.Instantiate(soundObject, position, rotation);
    }

    public static void PlayAtLocation(AudioSource audioSource, Vector3 position, Quaternion rotation)
    {
        audioSource.Play();
    }
}
