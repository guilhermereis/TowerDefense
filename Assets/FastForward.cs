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

    public void PauseSpeedOnClick() {
        clearButtons();
        pauseSpeed.image.overrideSprite = pauseSelected;
        pauseSpeed.interactable = false;
        Time.timeScale = 0;
    }

    public void NormalSpeedOnClick()
    {
        clearButtons();
        normalSpeed.image.overrideSprite = playSelected;
        normalSpeed.interactable = false;
        Time.timeScale = 1;
    }

    public void DoubleSpeedOnClick() {
        clearButtons();
        doubleSpeed.image.overrideSprite = doubleSelected;
        doubleSpeed.interactable = false;
        Time.timeScale = 2;

    }

    public void TripleSpeedOnClick()
    {
        clearButtons();
        tripleSpeed.image.overrideSprite = tripleSelected;
        tripleSpeed.interactable = false;
        Time.timeScale = 3;
    }

}
