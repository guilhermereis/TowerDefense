using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioOptions : MonoBehaviour {

    AudioSource audio;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }    
    void Start()
    {
        audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    public void OnValueChanged(float newValue)
    {
        audio.volume = newValue;
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
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
