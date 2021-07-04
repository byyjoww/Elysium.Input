using Elysium.Utils.Attributes;
using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Elysium.Input
{
    [CreateAssetMenu(fileName = "CharacterInputSO", menuName = "Scriptable Objects/Input/CharacterInputSO")]
    public class CharacterInputSystemSO : BaseInputSO, IInputReceiver, CharacterInputControls.IPlayerActions
    {
		[Separator("Character Input Values", true)]
		[SerializeField, ReadOnly] private Vector2 move = default;
		[SerializeField, ReadOnly] private Vector2 look = default;
		[SerializeField, ReadOnly] private bool jump = default;
		[SerializeField, ReadOnly] private bool sprint = default;

		[Separator("Movement Settings", true)]
		[SerializeField] private bool analogMovement = default;
		[SerializeField] private CursorLock cursorLock = new CursorLock();

		// public properties
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		public bool AnalogMovement => analogMovement;
		public bool Jump => jump;

		// public events
		public event UnityAction<Vector2> OnMoveInputChanged = delegate { };
		public event UnityAction<Vector2> OnLookInputChanged = delegate { };
		public event UnityAction<bool> OnJumpStatusChanged = delegate { };
		public event UnityAction<bool> OnSprintStatusChanged = delegate { };		


#if ENABLE_INPUT_SYSTEM
		public CharacterInputControls InputHandler { get; private set; }

		protected override void OnEnable()
		{
			if (InputHandler == null)
            {
                InputHandler = new CharacterInputControls();
                InputHandler.Player.SetCallbacks(this);
            }

			base.OnEnable();
		}

		protected override void EnableDefaultInput()
		{
			InputHandler.Player.Enable();
		}

		protected override void DisableAllInput()
		{
			InputHandler.Player.Disable();
		}

		public void OnMove(InputAction.CallbackContext _context)
		{
			SetMoveInput(_context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext _context)
		{
			if (cursorLock.CursorInputForLook)
			{
				SetLookInput(_context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext _context)
		{
			SetJumpInput(_context.phase == InputActionPhase.Performed);
		}

		public void OnSprint(InputAction.CallbackContext _context)
		{
			SetSprintInput(_context.phase == InputActionPhase.Performed);
		}
#endif

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
    }
}