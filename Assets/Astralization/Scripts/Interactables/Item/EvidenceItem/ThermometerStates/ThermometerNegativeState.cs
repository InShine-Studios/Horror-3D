using UnityEngine;

public class ThermometerNegativeState : ThermometerState, INegativeState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("EvidenceItem/MAT_Thermometer_Negative", typeof(Material)) as Material;
    }
    #endregion
}
