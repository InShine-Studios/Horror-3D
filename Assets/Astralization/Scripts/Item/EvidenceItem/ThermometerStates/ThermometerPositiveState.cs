using UnityEngine;

public class ThermometerPositiveState : ThermometerState, IPositiveState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("EvidenceItem/MAT_Thermometer_Positive", typeof(Material)) as Material;
    }
    #endregion
}
