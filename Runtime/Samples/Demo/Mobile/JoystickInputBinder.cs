using Elysium.Utils.Attributes;
using UnityEngine;

namespace Elysium.Input
{
    [RequireComponent(typeof(DisableAutoSwitchControls))]
    public class JoystickInputBinder : MonoBehaviour
    {
        [SerializeField][RequireInterface(typeof(IInputReceiver))]
        private UnityEngine.Object input;

        private IInputReceiver Input => input as IInputReceiver;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            Input.SetMoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            Input.SetLookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            Input.SetJumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            Input.SetSprintInput(virtualSprintState);
        }        
    }
}
