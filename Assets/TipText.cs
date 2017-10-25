using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipText : MonoBehaviour {

    public string[] tips = { " Build mine to make money", " Ice towers slow enemies" };
    private Text tipText;
	// Use this for initialization
	void Start () {
        tipText = GetComponent<Text>();
        tipText.text += tips[Random.Range(0, tips.Length)];
	}
	
	
}
