using UnityEngine;

public class ThermometerActiveState : ThermometerState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("MAT_Thermometer_Active", typeof(Material)) as Material;
    }
    #endregion
}
