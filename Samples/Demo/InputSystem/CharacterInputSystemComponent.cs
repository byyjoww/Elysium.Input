using Elysium.Utils;
using Elysium.Utils.Attributes;
using System.Linq;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Elysium.Input
{
	[RequireComponent(typeof(PlayerInput))]
	public class CharacterInputSystemComponent : MonoBehaviour, IInputReceiver
	{
        [Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;

		[Separator("Movement Settings", true)]
        [SerializeField] private bool analogMovement = default;
		[SerializeField] private CursorLock cursorLock = new CursorLock();

		public Vector2 Move => move;
        public Vector2 Look => look;        
        public bool Sprint => sprint;
        public bool AnalogMovement => analogMovement;
        public bool Jump => jump;


#if ENABLE_INPUT_SYSTEM
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

#endif

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