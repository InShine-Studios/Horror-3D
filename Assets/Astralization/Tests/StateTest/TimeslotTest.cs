using Assets.Astralization.Scripts.UI;
using Astralization.Player;
using Astralization.States.TimeslotStates;
using Astralization.Utils.Calculation;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TimeslotTest : TestBase
{
    #region Const
    private const string ActivityTimeslotChangerGameObjectName = "ActivityTimeslotChanger";
    private const string DungeonTimeslotChangerGameObjectName = "DungeonTimeslotChanger";
    #endregion

    #region Variables
    private GameObject _activityTimeslotChanger;
    private GameObject _dungeonTimeslotChanger;
    private TimeslotStateMachine _timeslotStateMachine;
    private ITimeslotHud _timeslotHud;
    #endregion

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
                _timeslotHud = gameObject.GetComponentInChildren<ITimeslotHud>();
            }
            else if (gameObject.name == ActivityTimeslotChangerGameObjectName)
            {
                _activityTimeslotChanger = gameObject;
            }
            else if (gameObject.name == DungeonTimeslotChangerGameObjectName)
            {
                _dungeonTimeslotChanger = gameObject;
            }
        }

        _timeslotStateMachine = TimeslotStateMachine.Instance;
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "TimeslotTestScene";
        base.SetUp();
    }
    #endregion

    #region Timeslot Manipulation
    [UnityTest]
    public IEnumerator Timeslot_AdvanceTimeslotBy_DoingRegularActivity()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        TimeslotState currentState = _timeslotStateMachine.CurrentDateTimeslot.Timeslot;
        DateTime currentDate = _timeslotStateMachine.CurrentDateTimeslot.Date;

        float moveDuration = GetMovementDurationTowards(_activityTimeslotChanger.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        int interactionCount = 0;
        while(interactionCount <= _timeslotStateMachine.TimeslotCount)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
            interactionCount++;
            TimeslotState nextState = _timeslotStateMachine.CurrentDateTimeslot.Timeslot;
            int currentTimeNum = MathCalcu.mod(currentState.TimeNum + interactionCount, _timeslotStateMachine.TimeslotCount);
            Assert.AreEqual(currentTimeNum, nextState.TimeNum);
            Assert.AreEqual(currentTimeNum, _timeslotHud.GetCurrentAnimatorParam());
        }
        DateTime nextDate = _timeslotStateMachine.CurrentDateTimeslot.Date;
        Assert.Greater(nextDate,currentDate);
    }

    [UnityTest]
    public IEnumerator Timeslot_AdvanceTimeslotBy_EnteringDungeon()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        TimeslotState currentState = _timeslotStateMachine.CurrentDateTimeslot.Timeslot;
        DateTime currentDate = _timeslotStateMachine.CurrentDateTimeslot.Date;

        float moveDuration = GetMovementDurationTowards(_dungeonTimeslotChanger.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, 0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        int interactionCount = 0;
        while (interactionCount <= _timeslotStateMachine.TimeslotCount)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
            interactionCount++;
            TimeslotState nextState = _timeslotStateMachine.CurrentDateTimeslot.Timeslot;
            int currentTimeNum = MathCalcu.mod(currentState.TimeNum + (interactionCount*2), _timeslotStateMachine.TimeslotCount);
            Assert.AreEqual(currentTimeNum, nextState.TimeNum);
            Assert.AreEqual(currentTimeNum, _timeslotHud.GetCurrentAnimatorParam());
        }
        DateTime nextDate = _timeslotStateMachine.CurrentDateTimeslot.Date;
        Assert.Greater(nextDate, currentDate);
    }
    #endregion
}
