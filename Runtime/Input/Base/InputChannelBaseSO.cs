using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InputChannelBaseSO : ScriptableObject
{
    public InputKeybinds Keybinds { get; protected set; }

    // DEVICE CONNECTIVITY
    public event UnityAction OnDeviceDisconnected;
    public event UnityAction OnDeviceReconnected;

    protected virtual void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChanged;
        EnableDefaultInput();
    }
    protected virtual void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChanged;
        DisableAllInput();
    }

    protected abstract void EnableDefaultInput();

    protected abstract void DisableAllInput();

    protected virtual void OnDeviceChanged(InputDevice _device, InputDeviceChange _change)
    {
        if (_change == InputDeviceChange.Disconnected) { OnDeviceDisconnected?.Invoke(); }
        if (_change == InputDeviceChange.Reconnected) { OnDeviceReconnected?.Invoke(); }
    }
}
