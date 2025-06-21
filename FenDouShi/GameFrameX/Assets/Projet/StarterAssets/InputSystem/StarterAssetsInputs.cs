using UnityEngine;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool fly = false;
		public bool sit = true;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			//	Debug.Log("@@@@@@@@@@@@@@@@OnJump value.isPressed=" + value.isPressed);
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			Debug.Log("@@@@@@@@@@@@@@@@OnJump value.OnSprint=" + value.isPressed);
			SprintInput(value.isPressed);
		}
		public void OnFire(InputValue value)
		{
			Debug.Log("input fire=" + value.isPressed);
			FireInput(value.isPressed);
		}
		public void OnFly(InputValue value)
		{
			Debug.Log("input OnFly=" + value.isPressed);
			FlyInput(value.isPressed);
		}

		public void OnSit(InputValue value)
        {
			SitInput(value.isPressed);
        }
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
			sit = false;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
			//	Debug.Log("LookInput() newLookDirection=" + newLookDirection);
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void FireInput(bool newFireState)
		{
			fire = newFireState;

		}
		public void FlyInput(bool newFlyState)
		{
			fly = !fly;
			Debug.Log("@@@@ÇÐ»»·ÉÐÐ×´Ì¬ fly=" + fly);
		}
		public void SitInput(bool newFlyState)
		{
			sit = !sit;
			Debug.Log("@@@@ÇÐ»»×´Ì¬ sit=" + sit);
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}