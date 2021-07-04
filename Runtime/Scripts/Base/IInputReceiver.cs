using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Input
{
    public interface IInputReceiver
    {
        // not sure what this is for
        bool AnalogMovement { get; }

        Vector2 Move { get; }
        Vector2 Look { get; }
        bool Jump { get; }
        bool Sprint { get; }

        void SetLookInput(Vector2 _direction);
        void SetMoveInput(Vector2 _direction);
        void SetJumpInput(bool _isJumping);
        void SetSprintInput(bool _isSprinting);
    }
}