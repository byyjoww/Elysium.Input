using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Input.Static
{
    public class CharacterInputStatic : MonoBehaviour, IInputReceiver
	{
		[Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;
		[SerializeField, ReadOnly] private bool aim = default;

		[Separator("Movement Settings", true)]
		[SerializeField] private bool analogMovement = default;
		[SerializeField] private CursorLock cursorLock = new CursorLock();

		[Separator("Look Settings", true)]
		[SerializeField] private bool lookInvertMouseX = false;
		[SerializeField] private bool lookInvertMouseY = true;
		[SerializeField] private Vector2 lookInputScaling = new Vector2(100, 100);

		[Separator("Keybinds", true)]
		[SerializeField] private KeyCode jumpKey = KeyCode.Space;
		[SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
		[SerializeField] private KeyCode aimKey = KeyCode.Mouse1;

		// public properties
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		public bool AnalogMovement => analogMovement;
		public bool Jump => jump;
		public bool Aim => aim;

		// public events
		public event UnityAction<Vector2> OnMoveInputChanged = delegate { };
		public event UnityAction<Vector2> OnLookInputChanged = delegate { };
		public event UnityAction<bool> OnJumpStatusChanged = delegate { };
		public event UnityAction<bool> OnSprintStatusChanged = delegate { };
		public event UnityAction<bool> OnAimStatusChanged = delegate { };

		protected virtual void Update()
		{
			CheckMove();
			CheckLook();
			CheckJump();
			CheckSprint();
			CheckAim();
		}

		public virtual void CheckMove()
		{
			float h = UnityEngine.Input.GetAxisRaw("Horizontal");
			float v = UnityEngine.Input.GetAxisRaw("Vertical");
			Vector2 m = new Vector2(h, v);
			SetMoveInput(m);
		}

		public virtual void CheckLook()
		{
			// Prevent look camera from moving as soon as the game finishes loading
			if (Time.frameCount < 10) { return; }

			if (cursorLock.UseCursorDeltaForLook && cursorLock.IsLocked)
			{
				float h = UnityEngine.Input.GetAxis("Mouse X");
				float v = UnityEngine.Input.GetAxis("Mouse Y");
				Vector2 delta = new Vector2(h, v);

				float x = lookInvertMouseX ? -delta.x : delta.x;
				float y = lookInvertMouseY ? -delta.y : delta.y;

				Vector2 l = new Vector2(x * lookInputScaling.x, y * lookInputScaling.y);
				SetLookInput(l);
			}
		}

		public virtual void CheckJump()
		{
			bool isJumping = UnityEngine.Input.GetKeyDown(jumpKey);
			SetJumpInput(isJumping);
		}

		public virtual void CheckSprint()
		{
			bool isSprinting = UnityEngine.Input.GetKey(sprintKey);
			SetSprintInput(isSprinting);
		}

		public virtual void CheckAim()
		{
			bool isAiming = UnityEngine.Input.GetKey(aimKey);
			SetAimInput(isAiming);
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