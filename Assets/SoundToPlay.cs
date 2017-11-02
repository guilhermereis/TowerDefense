using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomAudioSource {
    public AudioSource source;
    public float nativeVolumeMultiplier = 1f;

    public CustomAudioSource(AudioSource source, float volumeMultiplier) {
        this.source = source;
        this.nativeVolumeMultiplier = volumeMultiplier;
    }
}

public class SoundToPlay : MonoBehaviour
{
    static GameObject soundObject;
    static AudioSource audioSource;
    static float sfx_volume;
    static float music_volume;
    static float master_volume;
    static List<CustomAudioSource> BGs = new List<CustomAudioSource>();

    private void Awake()
    {
        ConfigurationMenu.soundSliderDelegate += volumesChangedDelegate;
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        master_volume = PlayerPrefs.GetFloat("master volume");
    }

    private void OnDestroy()
    {
        ConfigurationMenu.soundSliderDelegate -= volumesChangedDelegate;
    }

    void volumesChangedDelegate() {
        SetAllVolumes();
        List<AudioSource> audiosToRemove = new List<AudioSource>();
        BGs = BGs.Where(audio => audio != null).ToList();

        foreach (CustomAudioSource audio in BGs)
        {
            if(audio.source){
                if (audio.source.isPlaying)
                {
                    audio.source.volume = PlayerPrefs.GetFloat("music volume") * audio.nativeVolumeMultiplier;
                }
            }
        }
    }

    void updateBGsVolumes() {
        List<AudioSource> audiosToRemove = new List<AudioSource>();
        BGs = BGs.Where(audio => audio != null).ToList();

        foreach (CustomAudioSource audio in BGs)
        {
            if (audio.source)
            {
                if (audio.source.isPlaying)
                {
                    audio.source.volume = PlayerPrefs.GetFloat("music volume") * audio.nativeVolumeMultiplier;
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
        //SetAllVolumes();
        //SetGlobalVolume(master_volume);
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
        CustomAudioSource cas = new CustomAudioSource(src, 1f);
        BGs.Add(cas);
        MonoBehaviour.Instantiate(soundObject);
    }

    public static void PlayMusic(AudioSource _audioSource)
    {
        SetSoundToPlay(_audioSource);
        audioSource.volume = music_volume;
        CustomAudioSource cas = new CustomAudioSource(audioSource, 1f);
        BGs.Add(cas);
        audioSource.Play();
    }

    public static void PlayMusic(AudioSource _audioSource, float volumeMultiplier)
    {
        SetSoundToPlay(_audioSource);
        audioSource.volume = music_volume;
        CustomAudioSource cas = new CustomAudioSource(audioSource, volumeMultiplier);
        BGs.Add(cas);
        audioSource.volume = PlayerPrefs.GetFloat("music volume") * cas.nativeVolumeMultiplier;
        audioSource.Play();
    }


    public static void PlaySfx(GameObject _soundObject, float volumeMultiplier)
    {
        
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        src.volume = sfx_volume *volumeMultiplier;
        MonoBehaviour.Instantiate(soundObject);
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

    public static void PlayAtLocation(GameObject _soundObject, Vector3 position, Quaternion rotation, float distance = 10f)
    {
        float cameraMultiplier = Mathf.Clamp(1f - (Camera.main.orthographicSize - 3f) / 9f, 0.3f, 1f);
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        if (src.spatialBlend == 0f){
            src.spatialBlend = 1f;
            src.maxDistance = distance;
            src.minDistance = 3f;
        }
        src.volume = sfx_volume * cameraMultiplier;
        MonoBehaviour.Instantiate(soundObject, position, rotation);
    }

    public static void PlayAtLocation(GameObject _soundObject, Vector3 position, Quaternion rotation, float volumeMultiplier, float distance = 10f)
    {
        float cameraMultiplier = Mathf.Clamp(1f - (Camera.main.orthographicSize - 3f) / 9f, 0.3f, 1f);
        SetSoundToPlay(_soundObject);
        AudioSource src = soundObject.GetComponent<AudioSource>();
        if (src.spatialBlend == 0f)
        {
            src.spatialBlend = 1f;
            src.maxDistance = distance;
            src.minDistance = 3f;
        }
        src.volume = sfx_volume * volumeMultiplier * cameraMultiplier;
        MonoBehaviour.Instantiate(soundObject, position, rotation);
    }

    public static void PlayAtLocation(AudioSource _audioSource, Vector3 position, Quaternion rotation, float distance = 10f)
    {
        float cameraMultiplier = Mathf.Clamp(1f - (Camera.main.orthographicSize - 3f) / 9f, 0.3f, 1f);
        SetSoundToPlay(_audioSource);
        AudioSource src = _audioSource;
        if (src.spatialBlend == 0f)
        {
            src.spatialBlend = 1f;
            src.maxDistance = distance;
            src.minDistance = 3f;
        }
        src.volume = sfx_volume * cameraMultiplier;
        audioSource.Play();
    }

    public static void PlayAtLocation(AudioSource _audioSource, Vector3 position, Quaternion rotation, float volumeMultiplier, float distance = 10f)
    {
        float cameraMultiplier = Mathf.Clamp(1f - (Camera.main.orthographicSize - 3f) / 9f, 0.3f, 1f);
        SetSoundToPlay(_audioSource);
        AudioSource src = _audioSource;
        if (src.spatialBlend == 0f)
        {
            src.spatialBlend = 1f;
            src.maxDistance = distance;
            src.minDistance = 3f;
        }
        src.volume = sfx_volume * volumeMultiplier * cameraMultiplier;
        audioSource.Play();
    }
}
