using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeRandomizer : MonoBehaviour {
    public Material[] MVariation;
    public Material[] SnowMVariation;
    public Vector3 initialScale;
    public Theme theme;

    public enum Theme {Grassland, Snow};

#if UNITY_EDITOR
    // Use this for initialization
    void Start () {
        if (!Application.isPlaying) {
            float xyRandom = Random.Range(-0.002f, 0.002f);
            transform.localScale = initialScale + new Vector3(xyRandom, xyRandom, Random.Range(-0.002f, 0.002f));
            transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));

            switch (theme) {
                case Theme.Grassland:
                    if(MVariation.Length>0)
                    GetComponent<Renderer>().material = MVariation[(int)Random.Range(0, MVariation.Length)];
                    break;
                case Theme.Snow:
                    if (SnowMVariation.Length > 0)
                        GetComponent<Renderer>().material = SnowMVariation[(int)Random.Range(0, SnowMVariation.Length)];
                    break;
            }
            
        }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
    }
#endif
}
