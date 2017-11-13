using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipText : MonoBehaviour {

    private string[] tips = { " Build Mines to make money", " Ice towers slow enemies", " Don't forget to collect money from mine", "Bombers can explode your castle" };
    private Text tipText;
	// Use this for initialization
	void Start () {
        tipText = GetComponent<Text>();
        tipText.text += tips[Random.Range(0, tips.Length)];
	}
	
	
}
