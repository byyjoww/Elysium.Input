using Elysium.Utils;
using Elysium.Utils.Attributes;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Elysium.Input
{
#if ENABLE_INPUT_SYSTEM
	[RequireComponent(typeof(PlayerInput))]
	public class CharacterInput : MonoBehaviour, IInputReceiver
	{
        [Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;
		[SerializeField, ReadOnly] private bool aim = default;

		[Separator("Movement Settings", true)]
        [SerializeField] private bool analogMovement = default;

		[Separator("Cursor Settings", true)]
		[SerializeField] private CursorLock cursorLock = new CursorLock();

		// public properties
		public Vector2 Move => move;
        public Vector2 Look => look;        
        public bool Sprint => sprint;        
        public bool Jump => jump;
		public bool Aim => aim;
		public bool AnalogMovement => analogMovement;

		// public events
		public event UnityAction<Vector2> OnMoveInputChanged = delegate { };
		public event UnityAction<Vector2> OnLookInputChanged = delegate { };
		public event UnityAction<bool> OnJumpStatusChanged = delegate { };
		public event UnityAction<bool> OnSprintStatusChanged = delegate { };
		public event UnityAction<bool> OnAimStatusChanged = delegate { };

		public void OnMove(InputValue value)
		{
			SetMoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorLock.CursorInputForLook)
			{
				SetLookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			SetJumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SetSprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			SetAimInput(value.isPressed);
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
#endif
}