using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject configMenu;
    public AudioSource mainMenuBG;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FirstAccess") != 1)
        {
            Debug.Log("FIRST ACCESS");
            PlayerPrefs.SetFloat("master volume", 1f);
            PlayerPrefs.SetFloat("sfx volume", 0.8f);
            PlayerPrefs.SetFloat("music volume", 0.5f);
            PlayerPrefs.SetInt("tutorial", 1);
            Resolution[] resolutions = Screen.resolutions;

            Resolution currentRes = Screen.currentResolution;
            for (int i = 0; i < resolutions.Length; i++)
            {
                Resolution res = resolutions[i];
                if (res.width == currentRes.width && res.height == currentRes.height)
                {
                    PlayerPrefs.SetInt("resolution index", i);
                }
            }   
        }

        PlayerPrefs.SetInt("FirstAccess", 1);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        SoundToPlay.PlayMusic(mainMenuBG,0.7f);
    }
    public void StartGame() {
        SceneManager.LoadScene("LoadingScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ShowConfigScreen() {
        if(configMenu)
            configMenu.GetComponent<ConfigurationMenu>().Show();
    }

    public void HideConfigScreen() {
        if (configMenu)
            configMenu.GetComponent<ConfigurationMenu>().Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            GameController.TakeScreenshot();
        }
    }
}
