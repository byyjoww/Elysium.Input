using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Elysium.Input
{
    public abstract class BaseInputSO : ScriptableObject
    {
        public InputKeybinds Keybinds { get; protected set; }
        protected virtual List<InputAction> RebindableActions { get; } = new List<InputAction>();

        // DEVICE CONNECTIVITY
        public event UnityAction OnDeviceDisconnected;
        public event UnityAction OnDeviceReconnected;

        protected virtual void OnEnable()
        {
            if (Keybinds == null)
            {
                Keybinds = new InputKeybinds(RebindableActions);
            }

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
}