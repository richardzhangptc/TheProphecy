using UnityEngine;

public class CameraFollowTwoPlayers : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private float followSpeed = 7f;
    private float zoomSpeed = 7f;
    private float minZoom = 5f;
    private float maxZoom = 15f;
    private float zoomLimiter = 3f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (player1 == null || player2 == null) return;

        Vector3 midpoint = (player1.position + player2.position) / 2f;
        midpoint.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, midpoint, followSpeed * Time.deltaTime);

        float distanceX = Mathf.Abs(player1.position.x - player2.position.x);
        float distanceY = Mathf.Abs(player1.position.y - player2.position.y);
        
        float maxDistance = Mathf.Max(distanceX, distanceY * cam.aspect);
        float desiredZoom = Mathf.Clamp(maxDistance / zoomLimiter, minZoom, maxZoom);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredZoom, zoomSpeed * Time.deltaTime);
    }
}