using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject configMenu;
    public AudioSource mainMenuBG;

    private void Start()
    {
        SoundToPlay.PlayMusic(mainMenuBG);
    }
    public void StartGame() {
        SceneManager.LoadScene("MainScene");
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
}
