using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    #region Movement Values
    private Vector2 mousePosition = new Vector2(0, 0);
    [SerializeField] private Camera mainCamera;
    private Plane playerMovementPlane;
    private Vector3 playerToMouse;
    #endregion

    private void Awake()
    {
        CreatePlayerMovementPlane();
    }

    private void Update()
    {
        RotatePlayer();
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
        transform.rotation = newRotation;
    }

    #region Screen to World
    Vector3 ScreenPointToWorldPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }

    Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        plane.Raycast(ray, out float dist);
        return ray.GetPoint(dist);
    }

    private void CreatePlayerMovementPlane()
    {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }
    #endregion
}
