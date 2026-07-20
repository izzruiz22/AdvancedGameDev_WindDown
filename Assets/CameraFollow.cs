using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer background;

    [Header("Follow Settings")]
    [SerializeField] private float cameraOffsetX = 2f;
    [SerializeField] private float smoothSpeed = 5f;

    private Camera cam;

    private float minimumCameraX;
    private float maximumCameraX;
    private float fixedY;
    private float fixedZ;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        if (cam == null)
        {
            Debug.LogError("CameraFollow must be attached to a Camera.");
            enabled = false;
            return;
        }

        if (background == null)
        {
            Debug.LogError("Assign the background SpriteRenderer.");
            enabled = false;
            return;
        }

        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        CalculateCameraBounds();

        // Begin with the camera aligned to the background's left edge.
        transform.position = new Vector3(
            minimumCameraX,
            fixedY,
            fixedZ
        );
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        float desiredX = player.position.x + cameraOffsetX;

        float targetX = Mathf.Clamp(
            desiredX,
            minimumCameraX,
            maximumCameraX
        );

        Vector3 targetPosition = new Vector3(
            targetX,
            fixedY,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    private void CalculateCameraBounds()
    {
        // Half of the camera's visible width in world units.
        float cameraHalfWidth =
            cam.orthographicSize * cam.aspect;

        Bounds backgroundBounds = background.bounds;

        minimumCameraX =
            backgroundBounds.min.x + cameraHalfWidth;

        maximumCameraX =
            backgroundBounds.max.x - cameraHalfWidth;

        // Handles a background that is narrower than the camera.
        if (minimumCameraX > maximumCameraX)
        {
            float backgroundCenterX = backgroundBounds.center.x;
            minimumCameraX = backgroundCenterX;
            maximumCameraX = backgroundCenterX;
        }
    }
}