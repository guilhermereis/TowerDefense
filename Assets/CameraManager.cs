using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private GameObject freeCamera;
    private GameObject topCamera;
    private int cameraState = 0;
    private int CurrentRotation = 0;

	// Use this for initialization
	void Start () {
        if(!freeCamera)
            freeCamera = GameObject.FindWithTag("MainCamera");
        if (!topCamera)
            topCamera = GameObject.FindWithTag("TopCamera");
    }

    public void shakeCamera(float duration, float speed, float magnitude) {
        switch (cameraState) {
            case 0:
                freeCamera.GetComponent<CameraShake>().PlayShake(duration, speed, magnitude);
                break;
            case 1:
                topCamera.GetComponent<CameraShake>().PlayShake(duration, speed, magnitude);
                break;
        }
    }

    public void toggleCamera() {
        cameraState = (cameraState + 1) % 2;
        //0 = free camera, 1 = topCamera
        //TODO: Camera transition animation and Enable/Disable camera afterwards
        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            freeCamera.transform.RotateAround(Vector3.zero, Vector3.up, 90);
            if (CurrentRotation == 0)
            {
                freeCamera.transform.position.Set(51.81f, 28.54f, -24.34f);
                CurrentRotation++;
            }
            else if (CurrentRotation == 1)
            {
                freeCamera.transform.position.Set(-24.34f,28.54f,51.81f);
                CurrentRotation++;
            }
            else if (CurrentRotation == 2)
            {
                freeCamera.transform.position.Set(51.81f,28.54f,24.34f);
                CurrentRotation++;
            }
            else if (CurrentRotation == 3)
            {
                freeCamera.transform.position.Set(51.81f, 28.54f, -24.34f);
                CurrentRotation = 0;
            }
            freeCamera.GetComponent<CameraController>().SetInitialValues();
        }
            
    }
}
