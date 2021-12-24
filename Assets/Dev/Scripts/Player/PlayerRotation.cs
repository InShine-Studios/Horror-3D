using UnityEngine;
using UnityEngine.InputSystem;

// TODO: Docs & Tooltip
public class PlayerRotation : MonoBehaviour
{
    #region Movement Values
    private Vector2 mousePosition = new Vector2(0, 0);
    [SerializeField] private Camera mainCamera;
    private Plane playerMovementPlane;
    private Vector3 playerToMouse;

    public float rotationSpeed;
    private Vector3 posPrev;
    #endregion

    private void Awake()
    {
        CreatePlayerMovementPlane();
        posPrev = this.transform.position;
    }

    private void Update()
    {
        RotatePlayer();
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
    Vector3 ScreenPointToWorldPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }

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
