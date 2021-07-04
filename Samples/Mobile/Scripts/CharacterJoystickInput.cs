using Elysium.Utils;
using Elysium.Utils.Attributes;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Elysium.Input.Mobile
{
	[RequireComponent(typeof(DisableAutoSwitchControls))]
    public class CharacterJoystickInput : MonoBehaviour, IInputReceiver
	{
		[Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;
		[SerializeField, ReadOnly] private bool aim = default;

		[Separator("Movement Settings", true)]
		[SerializeField] private bool analogMovement = default;

		[Separator("Joysticks", true)]
		[SerializeField][RequireInterface(typeof(IInputBroadcaster<Vector2>))]
		private UnityEngine.Object moveJoystick = default;
		[SerializeField][RequireInterface(typeof(IInputBroadcaster<Vector2>))]
		private UnityEngine.Object lookJoystick = default;
		[SerializeField][RequireInterface(typeof(IInputBroadcaster<bool>))]
		private UnityEngine.Object jumpButton = default;
		[SerializeField][RequireInterface(typeof(IInputBroadcaster<bool>))]
		private UnityEngine.Object sprintButton = default;
		[SerializeField][RequireInterface(typeof(IInputBroadcaster<bool>))]
		private UnityEngine.Object aimButton = default;

		// public properties
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		public bool Jump => jump;
		public bool Aim => aim;
		public bool AnalogMovement => analogMovement;

		protected IInputBroadcaster<Vector2> MoveJoystick => moveJoystick as IInputBroadcaster<Vector2>;
		protected IInputBroadcaster<Vector2> LookJoystick => lookJoystick as IInputBroadcaster<Vector2>;
		protected IInputBroadcaster<bool> JumpButton => jumpButton as IInputBroadcaster<bool>;
		protected IInputBroadcaster<bool> SprintButton => sprintButton as IInputBroadcaster<bool>;
		protected IInputBroadcaster<bool> AimButton => aimButton as IInputBroadcaster<bool>;

		// public events
		public event UnityAction<Vector2> OnMoveInputChanged = delegate { };
		public event UnityAction<Vector2> OnLookInputChanged = delegate { };
		public event UnityAction<bool> OnJumpStatusChanged = delegate { };
		public event UnityAction<bool> OnSprintStatusChanged = delegate { };
		public event UnityAction<bool> OnAimStatusChanged = delegate { };

        private void OnEnable()
        {
			MoveJoystick.OnBroadcast += SetMoveInput;
			LookJoystick.OnBroadcast += SetLookInput;
			JumpButton.OnBroadcast += SetJumpInput;
			SprintButton.OnBroadcast += SetSprintInput;
			AimButton.OnBroadcast += SetAimInput;
		}

        private void OnDisable()
        {
			MoveJoystick.OnBroadcast -= SetMoveInput;
			LookJoystick.OnBroadcast -= SetLookInput;
			JumpButton.OnBroadcast -= SetJumpInput;
			SprintButton.OnBroadcast -= SetSprintInput;
			AimButton.OnBroadcast -= SetAimInput;
		}

        public virtual void SetMoveInput(Vector2 _direction)
		{
			Vector2 prev = move;
			move = _direction;
			if (move != prev) { OnMoveInputChanged?.Invoke(move); }
		}

		public virtual void SetLookInput(Vector2 _direction)
		{
			Vector2 prev = look;
			look = _direction;
			if (look != prev) { OnLookInputChanged?.Invoke(look); }
		}

		public virtual void SetJumpInput(bool _isJumping)
		{
			bool prev = jump;
			jump = _isJumping;
			if (jump != prev) { OnJumpStatusChanged?.Invoke(jump); }
		}

		public virtual void SetSprintInput(bool _isSprinting)
		{
			bool prev = sprint;
			sprint = _isSprinting;
			if (sprint != prev) { OnSprintStatusChanged?.Invoke(sprint); }
		}

		public virtual void SetAimInput(bool _isAiming)
		{
			bool prev = aim;
			aim = _isAiming;
			if (aim != prev) { OnAimStatusChanged?.Invoke(aim); }
		}
	}
}