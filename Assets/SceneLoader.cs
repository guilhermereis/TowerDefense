using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {


    private bool loadScene = false;
    public int scene;
    public Text tipText;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadNewScene());
	}
	
    IEnumerator LoadNewScene()
    {

        //yield return new WaitForSeconds(3);
        Debug.Log("Loading . . . ");
        AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
        while (!async.isDone)
        {
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
    }

    private void OnDestroy()
    {
        
    }

}
