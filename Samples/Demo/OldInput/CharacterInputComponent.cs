using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Input
{
    public class CharacterInputComponent : MonoBehaviour, IInputReceiver
	{
		[Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;

		[Separator("Movement Settings", true)]
		[SerializeField] private bool analogMovement = default;
		[SerializeField] private CursorLock cursorLock = new CursorLock();

		[Separator("Look Settings", true)]
		[SerializeField] private bool lookInvertMouseX = false;
		[SerializeField] private bool lookInvertMouseY = false;
		[SerializeField] private Vector2 lookInputScaling = new Vector2(15, 15);

		[Separator("Keybinds", true)]		
		[SerializeField] private KeyCode jumpKey = KeyCode.Space;
		[SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
		
		// private variables
		private Vector2? lastFrameMousePosition = null;

		// public properties
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		public bool AnalogMovement => analogMovement;
		public bool Jump => jump;
		
		protected virtual void Update()
		{
			CheckMove();
			CheckLook();
			CheckJump();
			CheckSprint();
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
			if(cursorLock.CursorInputForLook)
			{
				Vector2 mousePos = UnityEngine.Input.mousePosition;
				Vector2 delta = lastFrameMousePosition.HasValue ? mousePos - lastFrameMousePosition.Value : Vector2.zero;
				lastFrameMousePosition = mousePos;

				float x = lookInvertMouseX ? -delta.x : delta.x;
				float y = lookInvertMouseX ? -delta.y : delta.y;

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
			bool isSprinting = UnityEngine.Input.GetKeyDown(sprintKey);
			SetSprintInput(isSprinting);
		}

		public virtual void SetMoveInput(Vector2 _direction)
		{
			move = _direction;
		}

		public virtual void SetLookInput(Vector2 _direction)
		{
			look = _direction;
		}

		public virtual void SetJumpInput(bool _isJumping)
		{
			jump = _isJumping;
		}

		public virtual void SetSprintInput(bool _isSprinting)
		{
			sprint = _isSprinting;
		}
	}
}