using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to enable parts of player to rotate.
 * Used in the Rotate game object (child of player).
 * Rotate using mouse position (inpus saction) on screen to world position.
 */
public class PlayerRotation : MonoBehaviour
{
    #region Rotation Variables
    [Header("External Variables")]
    [Tooltip("Current mouse position")]
    private Vector2 _mousePosition = new Vector2(0, 0);
    [Tooltip("The camera that follows the player")]
    [SerializeField] private Camera _mainCamera;
    [Tooltip("The Plane used to map the mouse position")]
    private Plane _playerMovementPlane;
    [Tooltip("The PlayerBase for constants")]
    private PlayerBase _playerBase;

    [Space][Header("Rotation Constants")]
    [Tooltip("The last position of this object")]
    private Vector3 _posPrev;
    [Tooltip("Direction from player to cursor position")]
    private Vector3 _playerToMouse;
    [Tooltip("The rotation speed used")]
    private float _rotationSpeed;
    #endregion

    private void Awake()
    {
        _playerBase = GetComponentInParent<PlayerMovement>().GetPlayerBase();
        CreatePlayerMovementPlane();
        _posPrev = this.transform.position; // Save init pos
    }

    private void Start()
    {
        _rotationSpeed = _playerBase.GetRotationSpeed();
    }

    private void Update()
    {
        RotatePlayer();

        // Move plane based on player movement
        _playerMovementPlane.Translate(_posPrev - this.transform.position);
        _posPrev = this.transform.position;
    }

    #region Input System
    // Read mouse position to change player direction
    public void OnMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
        //Debug.Log("[PLAYER] Movement direction: " + mousePosition);
    }
    #endregion

    private void RotatePlayer()
    {
        Vector3 cursorScenePosition = _mousePosition;
        Vector3 cursorWorldPosition = ScreenPointToWorldPlane(cursorScenePosition, _playerMovementPlane, _mainCamera);
        _playerToMouse = cursorWorldPosition - transform.position;
        _playerToMouse.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(_playerToMouse);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

    #region Screen to World
    // Convert a screenpoint on camera to plane
    Vector3 ScreenPointToWorldPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }

    // Cast a ray to the plane and find the intersection point
    Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        plane.Raycast(ray, out float dist);
        Vector3 worldPos = ray.GetPoint(dist);
        //Debug.DrawLine(ray.origin, worldPos, Color.blue);
        return worldPos;
    }

    private void CreatePlayerMovementPlane()
    {
        _playerMovementPlane = new Plane(new Vector3(0,1,-1), this.transform.position);
    }
    #endregion
}
