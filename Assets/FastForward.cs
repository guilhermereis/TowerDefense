using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FastForward : MonoBehaviour {

    public Button normalSpeed;
    public Button doubleSpeed;
    public Button tripleSpeed;


    public void PauseSpeedOnClick() {
        Debug.Log("pausing speed");
        Time.timeScale = 0;
    }

    public void NormalSpeedOnClick()
    {
        Debug.Log("normal speed");
        Time.timeScale = 1;
    }

    public void DoubleSpeedOnClick() {
        Debug.Log("double speed");
        Time.timeScale = 2;

    }

    public void TripleSpeedOnClick()
    {
        Debug.Log("triple speed");
        Time.timeScale = 3;
    }

}
