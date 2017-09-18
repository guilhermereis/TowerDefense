using UnityEngine;

public class CameraController : MonoBehaviour {

    private float customDeltaTime = 0f;

    public float panSpeed = 40f;
    public float panBorderThickness = 10f;
    public float perspectiveRatio = 1.78f;
    
    public float zoomSpeed = 100f;
    public float zoomDeacceleration = 0.2f;
    public Vector2 zoomBounds = new Vector2(2f, 11f);
    public float panLimit = 15f;
    public float cameraMaxDistance = 100f;
    public float panAcceleration = 1f;

    private Vector3 forwardVector, rightVector;
    private Vector3 cameraStartPosition;
    private Vector3 rotationResetPosition;
    private float accumulatedZoomAcceleration = 0f;
    private float panMultiplier = 0f;
    private float cameraDistanceFromStart = 0f;
    private float panHorizontalAcceleration = 0f;
    private float panVerticalAcceleration = 0f;

    private int currentRotation = 0;

    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;


    private void Start(){
        SetInitialValues();
        cameraStartPosition = transform.position;
        rotationResetPosition = transform.position;
    }

    public void SetInitialValues()
    {
        forwardVector = transform.forward;
        forwardVector.y = 0;
        forwardVector = Vector3.Normalize(forwardVector);
        rightVector = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVector;

        switch (currentRotation) {
            case 0:
                cameraStartPosition = rotationResetPosition;
                break;
            case 1:
                cameraStartPosition = new Vector3(-cameraStartPosition.x, cameraStartPosition.y, cameraStartPosition.z);
                break;
            case 2:
                cameraStartPosition = new Vector3(cameraStartPosition.x, cameraStartPosition.y, -cameraStartPosition.z);
                break;
            case 3:
                cameraStartPosition = new Vector3(-cameraStartPosition.x, cameraStartPosition.y, cameraStartPosition.z);
                break;
        }
    }
    // Update is called once per frame
    void Update () {

        //Setting custom deltaTime according to its timescale
        if (Time.timeScale == 0f)
        {
            customDeltaTime = 0.016f; //fixed value like 60fps
        }
        else if (Time.timeScale == 2f)
        {
            customDeltaTime = Time.deltaTime/2f;
        }
        else if (Time.timeScale == 3f) {
            customDeltaTime = Time.deltaTime/3f;
        }
        else
        {
            customDeltaTime = Time.deltaTime;
        }

        isMovingDown = false;
        isMovingLeft = false;
        isMovingRight = false;
        isMovingUp = false;

        //Using directional keys and calculating acceleration
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            panHorizontalAcceleration = Mathf.Clamp((panHorizontalAcceleration + Input.GetAxisRaw("Horizontal") * panAcceleration * customDeltaTime), -1f, 1f);
            isMovingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            panHorizontalAcceleration = Mathf.Clamp((panHorizontalAcceleration -Input.GetAxisRaw("Horizontal") * panAcceleration * customDeltaTime), -1f, 1f);
            isMovingLeft = true;
        }
        else {
            panHorizontalAcceleration = Mathf.Lerp(panHorizontalAcceleration, 0f, 0.2f);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            panVerticalAcceleration = Mathf.Clamp((panVerticalAcceleration + Input.GetAxisRaw("Vertical") * panAcceleration * customDeltaTime), -1f, 1f);
            isMovingUp = true;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            panVerticalAcceleration = Mathf.Clamp((panVerticalAcceleration - Input.GetAxisRaw("Vertical") * panAcceleration * customDeltaTime), -1f, 1f);
            isMovingDown = true;
        }
        else {
            panVerticalAcceleration = Mathf.Lerp(panVerticalAcceleration, 0f, 0.2f);
        }

        //Camera Zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            accumulatedZoomAcceleration += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * customDeltaTime;
            accumulatedZoomAcceleration = Mathf.Clamp(accumulatedZoomAcceleration, -100f, 100f);
        }
        else {
            accumulatedZoomAcceleration = Mathf.Lerp(accumulatedZoomAcceleration, 0f, zoomDeacceleration);
        }

        float totalCameraSize = Camera.main.orthographicSize - accumulatedZoomAcceleration;

        Camera.main.orthographicSize = Mathf.Clamp(totalCameraSize, zoomBounds.x, zoomBounds.y);
        if (Camera.main.orthographicSize == zoomBounds.x || Camera.main.orthographicSize == zoomBounds.y) {
            accumulatedZoomAcceleration = 0f;
        }

        //Camera PAN
        panMultiplier = 1 - Camera.main.orthographicSize / zoomBounds.y;
        Vector3 rightMovement = rightVector * panSpeed * customDeltaTime * Input.GetAxisRaw("Horizontal") * panHorizontalAcceleration;
        Vector3 upMovement = forwardVector * perspectiveRatio * panSpeed * customDeltaTime * Input.GetAxisRaw("Vertical") * panVerticalAcceleration;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);


        //Using screen borders
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            upMovement = forwardVector * perspectiveRatio * panSpeed * customDeltaTime * 1f;
            isMovingUp = true;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            upMovement = forwardVector * perspectiveRatio * panSpeed * customDeltaTime * -1f;
            isMovingDown = true;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            rightMovement = rightVector * panSpeed * customDeltaTime * 1f;
            isMovingRight = true;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            rightMovement = rightVector * panSpeed * customDeltaTime * -1f;
            isMovingLeft = true;
        }

        float horizontalDistance = Vector3.Magnitude(((transform.position + rightMovement) - cameraStartPosition));
        float verticalDistance = Vector3.Magnitude(((transform.position + upMovement) - cameraStartPosition));

        //Clamping movement on maximum radius from camera's start position
        if (horizontalDistance < cameraMaxDistance) {
            if(isMovingUp || isMovingDown){
                transform.position += rightMovement * 1.5f;
            }
            else transform.position += rightMovement * 2f; //Boost when moving in only 1 axes
        }
        if (verticalDistance < cameraMaxDistance) {
            if (isMovingRight || isMovingLeft){
                transform.position += upMovement * 1.5f;
            }
            else transform.position += upMovement * 2f; //Boost when moving in only 1 axes
        }

        
        //Camera Pressure/Relief when out of bounds to return to rest position
        cameraDistanceFromStart = Vector3.Magnitude(transform.position - cameraStartPosition);
        float distanceFromBorderMultiplier = cameraDistanceFromStart - panLimit;



        if (cameraDistanceFromStart > panLimit + 10* panMultiplier) {
            float influence = customDeltaTime * 0.005f * distanceFromBorderMultiplier * ((isMovingDown || isMovingUp || isMovingRight || isMovingLeft) ? 2f : 10f); //- panMultiplier/100f;
            transform.position = Vector3.Lerp(transform.position, cameraStartPosition, 0.001f + influence);
        }

        //Camera Rotation logic
        if (Input.GetKeyDown("r"))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 90);
            switch (currentRotation) {
                case 0:
                    transform.position.Set(51.81f, 28.54f, -24.34f);
                    currentRotation = (currentRotation + 1) % 4;
                    break;
                case 1:
                    transform.position.Set(-24.34f, 28.54f, 51.81f);
                    currentRotation = (currentRotation + 1) % 4;
                    break;
                case 2:
                    transform.position.Set(51.81f, 28.54f, 24.34f);
                    currentRotation = (currentRotation + 1) % 4;
                    break;
                case 3:
                    transform.position.Set(51.81f, 28.54f, -24.34f);
                    currentRotation = (currentRotation + 1) % 4;
                    break;
            }
            GetComponent<CameraController>().SetInitialValues();
        }
    }
    
}
