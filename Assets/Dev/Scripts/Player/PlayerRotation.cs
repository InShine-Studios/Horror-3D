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
    private Vector2 mousePosition = new Vector2(0, 0);
    [Tooltip("The camera that follows the player")]
    [SerializeField] private Camera mainCamera;
    [Tooltip("The Plane used to map the mouse position")]
    private Plane playerMovementPlane;
    [Tooltip("The PlayerBase for constants")]
    private PlayerBase playerBase;

    [Space][Header("Rotation Constants")]
    [Tooltip("The last position of this object")]
    private Vector3 posPrev;
    [Tooltip("Direction from player to cursor position")]
    private Vector3 playerToMouse;
    [Tooltip("The rotation speed used")]
    private float rotationSpeed;
    #endregion

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        CreatePlayerMovementPlane();
        posPrev = this.transform.position; // Save init pos
    }

    private void Start()
    {
        rotationSpeed = playerBase.GetRotationSpeed();
    }

    private void Update()
    {
        RotatePlayer();

        // Move plane based on player movement
        playerMovementPlane.Translate(posPrev - this.transform.position);
        posPrev = this.transform.position;
    }

    #region Input System
    // Read mouse position to change player direction
    public void OnMousePosition(InputAction.CallbackContext mousePos)
    {
        mousePosition = mousePos.ReadValue<Vector2>();
        //Debug.Log("[PLAYER] Movement direction: " + mousePosition);
    }
    #endregion

    private void RotatePlayer()
    {
        Vector3 cursorScenePosition = mousePosition;
        Vector3 cursorWorldPosition = ScreenPointToWorldPlane(cursorScenePosition, playerMovementPlane, mainCamera);
        playerToMouse = cursorWorldPosition - transform.position;
        playerToMouse.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
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
        playerMovementPlane = new Plane(new Vector3(0,1,-1), this.transform.position);
    }
    #endregion
}
