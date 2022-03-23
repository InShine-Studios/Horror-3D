using UnityEngine;

public class ThermometerInactiveState : ThermometerState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("MAT_Thermometer_Base", typeof(Material)) as Material;
    }
    #endregion
}
