using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class ConfigurationMenu : MonoBehaviour {

    public Slider[] volumeSliders;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropDown;

    private float sfx_volume;
    private float music_volume;
    private float master_volume;
    int activeScreenResIndex;
    CanvasGroup cg;

    private Resolution[] resolutions; //new Resolution[4];

    public delegate void SoundSliderChangedDelegate();
    public static SoundSliderChangedDelegate soundSliderDelegate;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        soundSliderDelegate += processSliderChange;
    }

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        Hide();

        master_volume = PlayerPrefs.GetFloat("master volume");
        sfx_volume = PlayerPrefs.GetFloat("sfx volume");
        music_volume = PlayerPrefs.GetFloat("music volume");
        activeScreenResIndex = PlayerPrefs.GetInt("resolution index");

        volumeSliders[0].GetComponent<Slider>().value = master_volume; 
        volumeSliders[1].GetComponent<Slider>().value = sfx_volume; 
        volumeSliders[2].GetComponent<Slider>().value = music_volume;

        getScreenResolutions();

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
        soundSliderDelegate();
    }

    public void processSliderChange() {
    }

    public void getScreenResolutions() {
        resolutions = Screen.resolutions;
        resolutionDropDown.options.Clear();
        string pattern = @" \d+Hz";
        string replacement = "";
        Regex rgx = new Regex(pattern);

        foreach (Resolution r in resolutions) {
            resolutionDropDown.options.Add(new Dropdown.OptionData(r.width +"x"+ r.height));
        }
        if (resolutions.Length > activeScreenResIndex) {
            //string result = ;
            resolutionDropDown.value = activeScreenResIndex;
            resolutionDropDown.transform.Find("Label").GetComponent<Text>().text = rgx.Replace(("" + resolutions[activeScreenResIndex]), replacement);
        }
    }

    public void OnScreenResSet(int index) {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("Screenmanager Resolution Width", resolutions[index].width);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", resolutions[index].height);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", (fullscreenToggle.isOn) ? 1 : 0);
        PlayerPrefs.SetInt("resolution index", index);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        OnScreenResSet(resolutionDropDown.value);
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