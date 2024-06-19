using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject cam;

    public float zoomSpeed = 5f;
    public float zoomMin = 5f;
    public float zoomMax = 500f;

    public float rotationSpeed = 1f;
    public float dragSpeed = 0.5f;
    private Vector3 dragOrigin;

    void Start()
    {
        cam = this.gameObject;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
        HandleDrag();
    }

    void HandleMovement()
    {
        var forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.Translate(forward * Time.deltaTime * 10f, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.Translate(-cam.transform.right * Time.deltaTime * 10f, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.Translate(-forward * Time.deltaTime * 10f, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cam.transform.Translate(cam.transform.right * Time.deltaTime * 10f, Space.World);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Amplify the input to ensure it's significantly affecting the zoom.
        scroll *= 10; // Example of amplifying the input.

        if (scroll != 0f)
        {
            Vector3 dir = cam.transform.forward * scroll * zoomSpeed;
            Vector3 newPos = cam.transform.position + dir;
            cam.transform.position = newPos;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X") * (rotationSpeed / 4f);
            float mouseY = Input.GetAxis("Mouse Y") * (rotationSpeed / 4f);

            // Rotate the camera around its local Y axis
            cam.transform.Rotate(Vector3.up, -mouseX, Space.World);

            // Rotate the camera around its local X axis
            cam.transform.Rotate(Vector3.right, mouseY, Space.Self);

            // Optional: Clamp the vertical rotation to prevent flipping
            Vector3 currentRotation = cam.transform.localEulerAngles;
            currentRotation.x = ClampAngle(currentRotation.x, -90f, 90f);
            cam.transform.localEulerAngles = currentRotation;
        }
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(2)) // Middle mouse button pressed
        {
            dragOrigin = Input.mousePosition; // Save the start position of the drag
            return;
        }

        if (!Input.GetMouseButton(2)) return; // Exit if middle mouse button is not held

        // Calculate the distance the mouse has moved since the drag started
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        
        // Translate the camera in the direction of mouse movement and make it 10 times faster
        Vector3 move = new Vector3(pos.x * dragSpeed * 25, 0, pos.y * dragSpeed * 25); // Increased speed by 10 times and removed negation for direction reversal

        // For a top-down view, you might want to move along the world's X and Z axes
        cam.transform.Translate(move, Space.World); // Adjusted to reverse the direction

        dragOrigin = Input.mousePosition; // Update the origin to the current position
    }

    // Helper method to clamp angles between -360 and 360 degrees
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
