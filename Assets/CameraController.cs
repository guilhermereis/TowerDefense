using UnityEngine;

public class CameraController : MonoBehaviour {


    public float panSpeed = 40f;
    public float panBorderThickness = 10f;
    public float perspectiveRatio = 1.78f;
    public Vector2 panLimit = new Vector2(15f,15f);
    private Vector3 forwardVector, rightVector;
    private Vector3 cameraStartPosition;

    private void Start(){
        forwardVector = transform.forward;
        forwardVector.y = 0;
        forwardVector = Vector3.Normalize(forwardVector);
        rightVector = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVector;
        cameraStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        Vector3 rightMovement = rightVector * panSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        float horizontalDistance = Vector3.Magnitude(((transform.position + rightMovement) - cameraStartPosition));
        float verticalDistance = Vector3.Magnitude(((transform.position + upMovement) - cameraStartPosition));

        if (Input.mousePosition.y >= Screen.height - panBorderThickness){
            upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * 1;
        }
        if (Input.mousePosition.y <= panBorderThickness){
            upMovement = forwardVector * perspectiveRatio * panSpeed * Time.deltaTime * -1;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness){
            rightMovement = rightVector * panSpeed * Time.deltaTime * 1;
        }
        if (Input.mousePosition.x <= panBorderThickness){
            rightMovement = rightVector * panSpeed * Time.deltaTime * -1;
        }

        if (horizontalDistance < panLimit.x) {
            transform.position += rightMovement;
        }
        if (verticalDistance < panLimit.y) {
            transform.position += upMovement;
        }
    }
}
