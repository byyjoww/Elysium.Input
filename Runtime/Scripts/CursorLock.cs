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
        [SerializeField] private bool lockCursor = true;
        [SerializeField] private bool useCursorDeltaForLook = true;

        public bool LockCursor => lockCursor;
        public bool UseCursorDeltaForLook => useCursorDeltaForLook;
        public bool IsLocked => Cursor.lockState == CursorLockMode.Locked;

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
            if (!_isFocused || !Application.isPlaying) { return; }
            SetCursorState(lockCursor);
        }

        private void SetCursorState(bool _lock)
        {
            CursorLockMode prev = Cursor.lockState;
            Cursor.lockState = _lock ? CursorLockMode.Locked : CursorLockMode.None;
            if (Cursor.lockState != prev) { OnCursorLockStatusChanged?.Invoke(lockCursor); }
        }
#endif
    }
}