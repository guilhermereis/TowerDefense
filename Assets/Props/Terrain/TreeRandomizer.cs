using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeRandomizer : MonoBehaviour {
    public Material[] MVariation;
    public Vector3 initialScale;
#if UNITY_EDITOR
    // Use this for initialization
    void Start () {
        if (!Application.isPlaying) {
            float xyRandom = Random.Range(-0.002f, 0.002f);
            transform.localScale = initialScale + new Vector3(xyRandom, xyRandom, Random.Range(-0.002f, 0.002f));
            transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
            GetComponent<Renderer>().material = MVariation[(int)Random.Range(0, MVariation.Length)];
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
