using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour {

    public int[] ScreenWidths;
    public Toggle[] ResolutionToggles;
    public Slider[] volumeSliders;
    public Toggle fullscreenToggle;
    private float sfx_volume;
    private float music_volume;
    private float master_volume;
    int activeScreenResIndex;
    CanvasGroup cg;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        Hide();

        master_volume = PlayerPrefs.GetFloat("master volume");
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");

        volumeSliders[0].GetComponent<Slider>().value = master_volume; 
        volumeSliders[1].GetComponent<Slider>().value = sfx_volume; 
        volumeSliders[2].GetComponent<Slider>().value = music_volume;

        ////resolutions
        //activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        //bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
        //for (int i = 0; i < ResolutionToggles.Length; i++)
        //{
        //    ResolutionToggles[i].isOn = i == activeScreenResIndex;
        //}

        //fullscreenToggle.isOn = isFullscreen;
    }

    public void Hide()
    {
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }

    public void Show()
    {
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void Toggle()
    {
        if (cg.alpha == 1f)
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
        }
        else if (cg.alpha == 0f)
        {
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
    }

    public void SaveAudioConf() {
        PlayerPrefs.SetFloat("master volume", volumeSliders[0].value);
        PlayerPrefs.SetFloat("sfx volume", volumeSliders[1].value);
        PlayerPrefs.SetFloat("music volume", volumeSliders[2].value);
        PlayerPrefs.Save();
    }

    public void Continue()
    {
        SaveAudioConf();
        IngameMenu igm = GameObject.Find("IngameMenu").GetComponent<IngameMenu>();
        igm.Hide();
    }
    
    public void OnSliderChanged()
    {
        SaveAudioConf();
        AudioListener.volume = volumeSliders[0].value;
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
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //SceneManager.LoadScene("MainScene");
        //}
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