using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioOptions : MonoBehaviour {

    public int[] ScreenWidths;
    public Toggle[] ResolutionToggles;
    public Slider[] volumeSliders;
    public Toggle fullscreenToggle;
    int activeScreenResIndex;
    

    AudioSource audio;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }    
    void Start()
    {
        //audio
        audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //resolutions
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
        for (int i = 0; i < ResolutionToggles.Length; i++)
        {
            ResolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        fullscreenToggle.isOn = isFullscreen;

    }
    public void OnValueChanged(float newValue)
    {
        audio.volume = newValue;
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetScreenResolution(int i)
    {
        if (ResolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(ScreenWidths[i], (int)(ScreenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }
    public void SetFullScreen(bool isFullscreen)
    {
        for (int i = 0; i < ResolutionToggles.Length; i++)
        {
            //If FULLSCREEN, disable resolution toggles.
            //If not fullscreen, enable resolution toggles.
            ResolutionToggles[i].interactable = !isFullscreen;
        }
        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
            Debug.Log("Set Fullscreen.");
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

}
