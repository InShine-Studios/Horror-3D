using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITimeslotHud
{
    int GetCurrentAnimatorParam();
    void SetDateDay(DateTime date);
    void SetTimeslot(ITimeslotState timeslotState);
}

public class TimeslotHud : MonoBehaviour, ITimeslotHud
{
    #region Const
    private const string DateGameObjectName = "Date";
    private const string DayGameObjectName = "Day";
    private const string TimeslotGameObjectName = "Timeslot";
    private const string TimeslotLogoGameObjectName = "TimeslotLogo";
    private const string TimeStateParamAnimator = "TimeState";
    #endregion

    #region Variables - Singleton
    private static TimeslotHud _instance;
    public static TimeslotHud Instance { get { return _instance; } }
    #endregion

    #region Variables
    private Text _dateText;
    private Text _dayText;
    private Text _timeslotText;
    private Animator _timeslotLogoAnimator;
    #endregion

    #region SetGet
    private void SetDay(string day)
    {
        _dayText.text = day;
    }

    private void SetDate(DateTime date)
    {
        string dateString = (date.Day < 10) ? string.Format("0{0}", date.Day) : date.Day.ToString();
        string monthString = (date.Month < 10) ? string.Format("0{0}", date.Month) : date.Month.ToString();

        _dateText.text = string.Format("{0} / {1}", monthString, dateString);
    }

    public void SetDateDay(DateTime date)
    {
        string dayName = date.DayOfWeek.ToString();
        SetDay(dayName.Substring(0, 3).ToUpperInvariant());
        SetDate(date);
    }

    public void SetTimeslot(ITimeslotState timeslotState)
    {
        _timeslotLogoAnimator.SetInteger(TimeStateParamAnimator, timeslotState.TimeNum);
        _timeslotText.text = timeslotState.TimeName;
    }

    public int GetCurrentAnimatorParam()
    {
        return _timeslotLogoAnimator.GetInteger(TimeStateParamAnimator);
    }
    #endregion

    #region MonoBehavior
    private void Awake()
    {
        _instance = this;
        _dateText = transform.Find(DateGameObjectName).GetComponent<Text>();
        _dayText = transform.Find(DayGameObjectName).GetComponent<Text>();
        _timeslotText = transform.Find(TimeslotGameObjectName).GetComponent<Text>();
        _timeslotLogoAnimator = transform.Find(TimeslotLogoGameObjectName).GetComponent<Animator>();
    }
    #endregion
}
