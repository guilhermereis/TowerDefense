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
    public GameObject quitConfirmationScreen;
    public GameObject resConfirmationScreen;

    private float sfx_volume;
    private float music_volume;
    private float master_volume;

    public static int previousResIndex;
    public static int activeScreenResIndex;

    /*Set the following to true everytime you want to force the resolution dropdown to not do
    anything when setting its value, and change only the Text and selected text:
    resForceValue = true;
    dropdown.value = newValue;*/
    private bool resForceValue = false;

    CanvasGroup cg;

    private Resolution[] resolutions; //new Resolution[4];

    public delegate void SoundSliderChangedDelegate();
    public static SoundSliderChangedDelegate soundSliderDelegate;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        soundSliderDelegate += processSliderChange;
        ResolutionConfirmationScreenController.countDownFinished += onScreenResolutionCountDownFinished;
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
        //resolutions = new Resolution[4];
        //resolutions[0] = new Resolution(); resolutions[0].width = 1920; resolutions[0].height = 1080;
        //resolutions[1] = new Resolution(); resolutions[1].width = 1600; resolutions[1].height = 900;
        //resolutions[2] = new Resolution(); resolutions[2].width = 800; resolutions[2].height = 600;
        //resolutions[3] = new Resolution(); resolutions[3].width = 640; resolutions[3].height = 420;

        resolutionDropDown.options.Clear();
        string pattern = @" \d+Hz";
        string replacement = "";
        Regex rgx = new Regex(pattern);

        foreach (Resolution r in resolutions) {
            resolutionDropDown.options.Add(new Dropdown.OptionData(r.width +"x"+ r.height));
        }
        if (resolutions.Length > activeScreenResIndex) {
            resForceValue = true;
            resolutionDropDown.value = activeScreenResIndex;

            resolutionDropDown.transform.Find("Label").GetComponent<Text>().text = rgx.Replace(("" + resolutions[activeScreenResIndex]), replacement);
        }
    }

    public void OnScreenResSet(int index) {        
        previousResIndex = activeScreenResIndex;
        activeScreenResIndex = index;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreenToggle.isOn);
        if(!resForceValue)
            resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().showAndStartCountDown();
        resForceValue = false;
    }

    public void onScreenResolutionCountDownFinished()
    {
        resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().stopCountDown();
        resForceValue = true;
        Screen.SetResolution(resolutions[previousResIndex].width, resolutions[previousResIndex].height, fullscreenToggle.isOn);
        Debug.Log("REVERTING: ");
        resolutionDropDown.value = previousResIndex;
    }

    public void screenResolutionSetConfirmed() {
        resConfirmationScreen.GetComponent<ResolutionConfirmationScreenController>().stopCountDown();
        PlayerPrefs.SetInt("Screenmanager Resolution Width", resolutions[activeScreenResIndex].width);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", resolutions[activeScreenResIndex].height);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", (fullscreenToggle.isOn) ? 1 : 0);
        PlayerPrefs.SetInt("resolution index", activeScreenResIndex);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        OnScreenResSet(resolutionDropDown.value);
    }

    public void QuitButtonPressed() {
        showQuitConfirmationScreen();
    }

    public void showQuitConfirmationScreen() {
        quitConfirmationScreen.GetComponent<CanvasGroup>().alpha = 1;
        quitConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void hideQuitConfirmationScreen() {
        quitConfirmationScreen.GetComponent<CanvasGroup>().alpha = 0;
        quitConfirmationScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
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