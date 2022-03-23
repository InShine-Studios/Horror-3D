using UnityEngine;

public class ThermometerNegativeState : ThermometerState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("MAT_Thermometer_Negative", typeof(Material)) as Material;
    }
    #endregion
}
