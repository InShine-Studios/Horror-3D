using UnityEngine;

public class ThermometerActiveState : ThermometerState, IActiveState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        materialInUse = Resources.Load("EvidenceItem/MAT_Thermometer_Active", typeof(Material)) as Material;
    }
    #endregion
}
