using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FastForward : MonoBehaviour {

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

    public void Start()
    {
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
        GameObject.Find("CampBuild").GetComponent<Button>().enabled = false;
        GameObject.Find("TowerBuild").GetComponent<Button>().enabled = false;
        BuildManager.instance.HideOptions();
        BuildManager.instance.forceHideUpgradeWheel();
    }
    public void EnableUI()
    {
        GameObject.Find("RepairButton").GetComponent<Button>().enabled = true;
        GameObject.Find("CampBuild").GetComponent<Button>().enabled = true;
        GameObject.Find("TowerBuild").GetComponent<Button>().enabled = true;
    }
    public void PauseSpeedOnClick() {
        GridMouse.instance.canClickGrid = false;
        DisableUI();
        clearButtons();
        pauseSpeed.image.overrideSprite = pauseSelected;
        pauseSpeed.interactable = false;
        Time.timeScale = 0;
        
    }

    public void NormalSpeedOnClick()
    {
        GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        normalSpeed.image.overrideSprite = playSelected;
        normalSpeed.interactable = false;
        Time.timeScale = 1;
    }

    public void DoubleSpeedOnClick() {
        GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        doubleSpeed.image.overrideSprite = doubleSelected;
        doubleSpeed.interactable = false;
        Time.timeScale = 2;

    }

    public void TripleSpeedOnClick()
    {
        GridMouse.instance.canClickGrid = true;
        EnableUI();
        clearButtons();
        tripleSpeed.image.overrideSprite = tripleSelected;
        tripleSpeed.interactable = false;
        Time.timeScale = 3;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
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
