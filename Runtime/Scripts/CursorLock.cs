using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Input
{
    [System.Serializable]
    public class CursorLock
    {
#if !UNITY_IOS || !UNITY_ANDROID
        [Separator("Mouse Cursor Settings", true)]
        [SerializeField] private bool cursorLocked = true;
        [SerializeField] private bool cursorInputForLook = true;

        public bool CursorLocked => cursorLocked;
        public bool CursorInputForLook => cursorInputForLook;

        public event UnityAction<bool> OnCursorLockStatusChanged = delegate { };

        public CursorLock()
        {
            Application.focusChanged += OnApplicationFocusChanged;
        }

        ~CursorLock()
        {
            Application.focusChanged -= OnApplicationFocusChanged;
        }

        private void OnApplicationFocusChanged(bool _isFocused)
        {
            if (!_isFocused) { return; }
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool _lock)
        {
            CursorLockMode prev = Cursor.lockState;
            Cursor.lockState = _lock ? CursorLockMode.Locked : CursorLockMode.None;
            if (Cursor.lockState != prev) { OnCursorLockStatusChanged?.Invoke(cursorLocked); }
        }
#endif
    }
}