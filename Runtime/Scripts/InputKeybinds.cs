using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Elysium.Input
{
    public class InputKeybinds
    {
        // REBINDING
        public event UnityAction<InputAction> OnRebindingStarted;
        public event UnityAction<InputAction> OnRebindingComplete;

        public InputKeybinds(List<InputAction> _rebindableActions)
        {
            this.rebindableActions = _rebindableActions;
        }

        // public List<InputAction> AllActions => new List<InputAction>(); inputHandler.Gameplay.Get().actions; // => SET ALL ACTIONS HERE
        private List<InputAction> rebindableActions = default;

        public void ChangeKeybind(InputAction _action, int _keyIndex, UnityEvent _cancel)
        {
            OnRebindingStarted?.Invoke(_action);

            var operation = _action.PerformInteractiveRebinding()
                .WithControlsExcluding("<Mouse>")
                .WithControlsExcluding("<Keyboard>/escape")
                .WithCancelingThrough("<Keyboard>/escape")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(OnRebindComplete)
                .OnCancel(OnRebindComplete)
                .OnPotentialMatch(CheckForExistingBindings)
                .Start();

            void OnRebindComplete(InputActionRebindingExtensions.RebindingOperation _operation)
            {
                _operation.Dispose();
                _cancel.RemoveAllListeners();
                OnRebindingComplete?.Invoke(_action);
            }

            void CancelRebind()
            {
                operation.Cancel();
            }

            _cancel.AddListener(CancelRebind);

            // TODO: SAVE KEYBIND MODIFICATION
        }

        public void RevertAllKeybindingsToDefault()
        {
            List<InputAction> actions = new List<InputAction>(rebindableActions);
            for (int i = 0; i < actions.Count; i++)
            {
                if (!IsRebindableAction(actions[i])) { continue; }

                actions[i].RemoveAllBindingOverrides();
                OnRebindingComplete?.Invoke(actions[i]);
            }
        }

        private void CheckForExistingBindings(InputActionRebindingExtensions.RebindingOperation _operation)
        {
            InputControl control = InputSystem.FindControl(_operation.selectedControl.path);

            List<InputAction> actions = new List<InputAction>(rebindableActions);
            for (int i = 0; i < actions.Count; i++)
            {
                for (int j = 0; j < actions[i].controls.Count; j++)
                {
                    if (actions[i].controls[j] == control)
                    {
                        Debug.LogError("control already exists");
                        _operation.Cancel();
                    }
                }
            }
        }

        public bool IsRebindableAction(InputAction _action)
        {
            return rebindableActions.Contains(_action);
        }

        public static string GetActionKeybind(InputAction _action, int _index)
        {
            // int bindingIndex = _action.GetBindingIndexForControl(_action.controls[_index]);

            return InputControlPath.ToHumanReadableString(
                _action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
            );
        }
    }
}