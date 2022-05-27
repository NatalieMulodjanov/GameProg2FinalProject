using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    float yRotation = 0f;

    private Vector2 rotation;

    public Vector2 acceleration;

    private Vector2 velocity;
    private Vector2 lastInputEvent;

    public float inputLagPeriod;
    public float maxVerticalAngleFromHorizon;
    private float inputLagTimer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = GetInput();
        velocity = new Vector2(Mathf.MoveTowards(velocity.x, input.x, acceleration.x * Time.deltaTime), Mathf.MoveTowards(velocity.y, input.y, acceleration.y * Time.deltaTime));

        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        // xRotation -= mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // yRotation -= mouseY;
        // yRotation = Mathf.Clamp(mouseY, -90f, 90f);

        // Debug.Log(xRotation);
        // Debug.Log(yRotation);
        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0f);
        playerBody.localEulerAngles = new Vector3(rotation.y, rotation.x, 0f);
    }

    private float ClampVerticalAngle(float angle) {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput() {
        inputLagTimer += Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector2 vector = new Vector2(mouseX, mouseY);

        if((Mathf.Approximately(0, vector.x) && Mathf.Approximately(0, vector.y)) == false || inputLagTimer >= inputLagPeriod) {
            lastInputEvent = vector;
            inputLagTimer = 0;
        }

        return lastInputEvent;
    }
}
