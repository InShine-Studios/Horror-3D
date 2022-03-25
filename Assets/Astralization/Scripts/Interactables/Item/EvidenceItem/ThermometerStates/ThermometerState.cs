using UnityEngine;

public class ThermometerState : State
{
    #region Variables
    private MeshRenderer _mesh;
    protected ThermometerManager owner;
    protected Material materialInUse;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<ThermometerManager>();
        _mesh = transform.Find("Model").GetComponentInChildren<MeshRenderer>(true);
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        _mesh.material = materialInUse;
    }
    #endregion
}
