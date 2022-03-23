using UnityEngine;

public class ThermometerPositiveState : ThermometerState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("MAT_Thermometer_Positive", typeof(Material)) as Material;
    }
    #endregion
}
