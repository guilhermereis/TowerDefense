using UnityEngine;

public class CameraController : MonoBehaviour {


    public float panSpeed = 40f;
    public float panBorderThickness = 10f;
    public float perspectiveRatio = 1.78f;
    
    public float zoomSpeed = 100f;
    public float zoomDeacceleration = 0.2f;
    public Vector2 zoomBounds = new Vector2(2f, 11f);
    public float panLimit = 15f;
    public float cameraMaxDistance = 100f;

    private Vector3 forwardVector, rightVector;
    private Vector3 cameraStartPosition;
    private float accumulatedZoomAcceleration = 0f;
    private float panMultiplier = 0f;
    private float cameraDistanceFromStart = 0f;

    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private void Start(){
        forwardVector = transform.forward;
        forwardVector.y = 0;
        forwardVector = Vector3.Normalize(forwardVector);
        rightVector = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVector;
        cameraStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {

        //Camera Zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            accumulatedZoomAcceleration += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
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
        Vector3 rightMovement = rightVector * panSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        isMovingDown = false;
        isMovingLeft = false;
        isMovingRight = false;
        isMovingUp = false;

        //Using directional keys
        if (Input.GetAxis("Horizontal") > 0)
        {
            isMovingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isMovingLeft = true;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            isMovingUp = true;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            isMovingDown = true;
        }

        //Using screen borders
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * 1f;
            isMovingUp = true;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * -1f;
            isMovingDown = true;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            rightMovement = rightVector * panSpeed * Time.deltaTime * 1f;
            isMovingRight = true;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            rightMovement = rightVector * panSpeed * Time.deltaTime * -1f;
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
        if (cameraDistanceFromStart > panLimit + 10* panMultiplier) {
            float influence = 0.001f * cameraDistanceFromStart * ((isMovingDown || isMovingUp || isMovingRight || isMovingLeft) ? 2f : 10f) * Time.deltaTime - panMultiplier/1000f;
            transform.position = Vector3.Lerp(transform.position, cameraStartPosition, 0.005f + influence);
        }

    }
}
