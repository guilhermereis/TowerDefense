using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private GameObject freeCamera;
    private GameObject topCamera;
    private CameraController freeCameraController;
    private int cameraState = 0;

	// Use this for initialization
	void Start () {
        if(!freeCamera)
            freeCamera = GameObject.FindWithTag("MainCamera");
        if (!topCamera)
            topCamera = GameObject.FindWithTag("TopCamera");

        if (freeCamera)
            freeCameraController = freeCamera.GetComponent<CameraController>();
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

    public void rotateCamera(bool clockwise) {
        if (freeCameraController)
        {
            if (clockwise)
            {
                freeCameraController.rotateClockwise();
            }
            else {
                freeCameraController.rotateCounterClockwise();
            }
        }
    }

	// Update is called once per frame
	void Update () {
            
    }
}
