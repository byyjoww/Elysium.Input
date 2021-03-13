using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputChannelSO", menuName = "Scriptable Objects/Event/Input Channel SO")]
public class InputChannelSO : InputChannelBaseSO
{
    // public InputHandler InputHandler { get; private set; } = new InputKeybinds(InputHandler);

    protected override void OnEnable()
    {
        //if (InputHandler == null)
        //{
        //    InputHandler = new InputHandler();
        //    InputHandler.XYZ.SetCallbacks(this);
        //}
    }
    protected override void OnDisable()
    {

    }

    protected override void EnableDefaultInput()
    {
        // InputHandler.Menu.Enable();
        // InputHandler.Gameplay.Disable();
    }

    protected override void DisableAllInput()
    {
        // InputHandler.Gameplay.Disable();
        // InputHandler.Menu.Disable();
    }
}


