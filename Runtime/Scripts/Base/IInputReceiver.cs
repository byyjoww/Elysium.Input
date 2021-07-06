using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Input
{
    public interface IInputReceiver
    {
        bool AnalogMovement { get; }

        Vector2 Move { get; }
        Vector2 Look { get; }
        bool Jump { get; }
        bool Sprint { get; }
        bool Aim { get; }

        event UnityAction<Vector2> OnMoveInputChanged;
        event UnityAction<Vector2> OnLookInputChanged;
        event UnityAction<bool> OnJumpStatusChanged;
        event UnityAction<bool> OnSprintStatusChanged;
        event UnityAction<bool> OnAimStatusChanged;

        void SetLookInput(Vector2 _direction);
        void SetMoveInput(Vector2 _direction);
        void SetJumpInput(bool _isJumping);
        void SetSprintInput(bool _isSprinting);
        void SetAimInput(bool _isAiming);
    }
}