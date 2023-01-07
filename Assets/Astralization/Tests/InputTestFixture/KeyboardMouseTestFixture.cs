using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class KeyboardMouseTestFixture: InputTestFixture
{
    public enum RegisteredInput
    {
        MoveForward,
        MoveLeft,
        MoveRight,
        MoveBack,
        Sprint,
        Interact,
        PickItem,
        DiscardItem,
        UseItem,
        ChangeItem,
        NextDialogueEnter,
        NextDialogueClick,
        SimulateGhostInteract,
        ChangeCoreForward,
        ChangeCoreBack,
        ChangeClueForward,
        ChangeClueBack
    }
    private Keyboard keyboard;
    private Mouse mouse;
    public Dictionary<RegisteredInput, KeyControl> keyboardInputMap;
    public Dictionary<RegisteredInput, ButtonControl> buttonInputMap;
    public Dictionary<RegisteredInput, AxisControl> axisInputMap;

    public override void Setup()
    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = InputSystem.AddDevice<Mouse>();
        keyboardInputMap = new Dictionary<RegisteredInput, KeyControl>() {
            {RegisteredInput.MoveForward, keyboard.wKey },
            {RegisteredInput.MoveLeft, keyboard.aKey },
            {RegisteredInput.MoveRight, keyboard.dKey },
            {RegisteredInput.MoveBack, keyboard.sKey },
            {RegisteredInput.Sprint, keyboard.leftShiftKey },
            {RegisteredInput.Interact, keyboard.eKey },
            {RegisteredInput.PickItem, keyboard.fKey },
            {RegisteredInput.DiscardItem, keyboard.gKey },
            {RegisteredInput.NextDialogueEnter, keyboard.enterKey },
            {RegisteredInput.SimulateGhostInteract, keyboard.zKey }, // [TODO] Remove when Ghost Interaction fully implemented
            {RegisteredInput.ChangeCoreForward, keyboard.eKey },
            {RegisteredInput.ChangeCoreBack, keyboard.qKey },
            {RegisteredInput.ChangeClueForward, keyboard.dKey },
            {RegisteredInput.ChangeClueBack, keyboard.aKey },
        };
        buttonInputMap = new Dictionary<RegisteredInput, ButtonControl>(){
            {RegisteredInput.UseItem, mouse.leftButton},
            {RegisteredInput.ToggleFlashlight, mouse.rightButton},
            {RegisteredInput.NextDialogueClick, mouse.leftButton }
        };
        axisInputMap = new Dictionary<RegisteredInput, AxisControl>() {
            { RegisteredInput.ChangeItem, mouse.scroll.y }
        };
    }

    public override void TearDown()
    {
        InputSystem.RemoveDevice(keyboard);
        InputSystem.RemoveDevice(mouse);
        base.TearDown();
    }

    public void Press(RegisteredInput action)
    {
        if(keyboardInputMap.ContainsKey(action)) 
            Press(keyboardInputMap[action]);
        else
            Press(buttonInputMap[action]);
    }

    public void Release(RegisteredInput action)
    {
        if (keyboardInputMap.ContainsKey(action))
            Release(keyboardInputMap[action]);
        else
            Release(buttonInputMap[action]);
    }

    public void Set(string motionType, float value)
    {
        Set<float>(mouse, motionType, value);
    }
}
