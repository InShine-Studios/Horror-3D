using UnityEngine;

public class ThermometerState : State
{
    #region Variables
    protected ThermometerManager owner;
    protected Material materialInUse;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<ThermometerManager>();
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        MeshRenderer mesh = transform.Find("Model").GetComponentInChildren<MeshRenderer>(true);
        mesh.material = materialInUse;
    }
    #endregion
}
