﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TopRightMenu : MonoBehaviour {

    public Button pauseSpeed;
    public Sprite pauseSelected;
    public Sprite pauseUnselected;

    public Button normalSpeed;
    public Sprite playSelected;
    public Sprite playUnselected;

    public Button doubleSpeed;
    public Sprite doubleSelected;
    public Sprite doubleUnselected;

    public Button tripleSpeed;
    public Sprite tripleSelected;
    public Sprite tripleUnselected;

    public GameObject configMenu;
    public static bool isConfigOn;
    public static bool isGamePaused;
    public static bool isSecondChanceOn;

    public void Start()
    {
        isConfigOn = false;
        isGamePaused = false;
        isSecondChanceOn = false;
        NormalSpeedOnClick();
    }

    private void clearButtons() {
        pauseSpeed.image.overrideSprite = pauseUnselected;
        pauseSpeed.interactable = true;
        normalSpeed.image.overrideSprite = playUnselected;
        normalSpeed.interactable = true;
        doubleSpeed.image.overrideSprite = doubleUnselected;
        doubleSpeed.interactable = true;
        tripleSpeed.image.overrideSprite = tripleUnselected;
        tripleSpeed.interactable = true;
    }
    public void DisableUI()
    {
        GameObject.Find("RepairButton").GetComponent<Button>().enabled = false;
        GameObject.Find("MineBuild").GetComponent<Button>().enabled = false;
        GameObject.Find("TowerBuild").GetComponent<Button>().enabled = false;
        BuildManager.instance.HideOptions();
        BuildManager.instance.forceHideUpgradeWheel();
    }
    public void EnableUI()
    {
        GameObject.Find("RepairButton").GetComponent<Button>().enabled = true;
        GameObject.Find("MineBuild").GetComponent<Button>().enabled = true;
        GameObject.Find("TowerBuild").GetComponent<Button>().enabled = true;
    }

    public void ShowConfigurationMenu()
    {
        isConfigOn = true;
        PauseSpeedOnClick();
        if (configMenu)
        {
            configMenu.GetComponent<ConfigurationMenu>().Show();
        }
    }

    public void HideConfigurationMenu()
    {
        isConfigOn = false;
        NormalSpeedOnClick();
        if (configMenu)
        {
            configMenu.GetComponent<ConfigurationMenu>().Hide();
        }
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void PauseSpeedOnClick() {
        GameObject.Find("Grid").GetComponent<GridMouse>().canClickGrid = true;
        //GridMouse.instance.canClickGrid = false;
        DisableUI();
        clearButtons();
        pauseSpeed.image.overrideSprite = pauseSelected;
        pauseSpeed.interactable = false;
        Time.timeScale = 0;
        isGamePaused = true;
        
    }

    public void NormalSpeedOnClick()
    {
        GameObject.Find("Grid").GetComponent<GridMouse>().canClickGrid = true;
        //GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        normalSpeed.image.overrideSprite = playSelected;
        normalSpeed.interactable = false;
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void DoubleSpeedOnClick() {
        GameObject.Find("Grid").GetComponent<GridMouse>().canClickGrid = true;
        //GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        doubleSpeed.image.overrideSprite = doubleSelected;
        doubleSpeed.interactable = false;
        Time.timeScale = 2;
        isGamePaused = false;

    }

    public void TripleSpeedOnClick()
    {
        GameObject.Find("Grid").GetComponent<GridMouse>().canClickGrid = true;
        //GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        tripleSpeed.image.overrideSprite = tripleSelected;
        tripleSpeed.interactable = false;
        Time.timeScale = 3;
        isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (!isSecondChanceOn) {
                if (isConfigOn)
                {
                    HideConfigurationMenu();
                }
                else
                {
                    ShowConfigurationMenu();
                }
            }
        }

        if (!isConfigOn){
            if (Input.GetButtonDown("Pause"))
            {
                PauseSpeedOnClick();
            }
            if (Input.GetButtonDown("Speedx1"))
            {
                NormalSpeedOnClick();
            }
            if (Input.GetButtonDown("Speedx2"))
            {
                DoubleSpeedOnClick();
            }
            if (Input.GetButtonDown("Speedx3"))
            {
                TripleSpeedOnClick();
            }
        }
        
    }

}
