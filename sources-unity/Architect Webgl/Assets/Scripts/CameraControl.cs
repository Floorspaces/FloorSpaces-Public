using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public int panSpeed = 50; // Increased panning speed

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Zoom In/Out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize -= scroll * zoomSpeed;

        // Pan Camera/Grid
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            float deltaX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float deltaY = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            Vector3 pan = new Vector3(deltaX, deltaY, 0);
            mainCamera.transform.Translate(-pan, Space.Self);
        }
    }
}
