using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerUIController : MonoBehaviour {
    public Vector3 mapLocation;
    private Canvas hudCanvas;
    private Animator anim;
    private GameObject arrow;
    private GameObject image;

    private Vector3 screenPos;
    private Vector2 arrowOnScreenPos;
    float max;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        arrow = transform.Find("Arrow").gameObject;
        //gameObject.SetActive(false);
        arrow.transform.parent = transform.parent.gameObject.transform;
        //arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        transform.position = Camera.main.WorldToScreenPoint(new Vector3(-1.5f, 0.5f, 0f) + mapLocation);
        screenPos = Camera.main.ScreenToViewportPoint(transform.position);
        arrowOnScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;

        float angle = (Vector2.SignedAngle(arrowOnScreenPos, new Vector2(0f, 1f)) +40f) *-1;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1)
        {
            // On Screen
            arrow.SetActive(false);
            return; 
        }
        else {
            arrow.SetActive(true);
            max = Mathf.Max(Mathf.Abs(arrowOnScreenPos.x), Mathf.Abs(arrowOnScreenPos.y)) * 1.1f;
            arrowOnScreenPos = (arrowOnScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f);
            arrow.transform.position = new Vector3(arrowOnScreenPos.x * Screen.width, arrowOnScreenPos.y * Screen.height , arrow.transform.position.z);
        }
    }
}
