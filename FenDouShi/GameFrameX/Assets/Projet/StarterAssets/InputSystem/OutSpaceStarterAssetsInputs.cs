using UnityEngine;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class OutSpaceStarterAssetsInputs : MonoBehaviour
	{
		[Header("虚拟摇杆对象")]
		public GameObject Joysticks;
		[Header("Character Input Values")]
		public Vector2 look;
		public bool isFastGun;
		public bool isDefendGun;
		public bool isAOEGun;
		public bool isSkillDisturbGun;

		public bool isAutoGunShoot=false;

		public bool isUVAGun;//无人机


		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = false;
		public bool cursorInputForLook = false;
        private void Awake()
        {
            if (Joysticks == null)
            {
				Debug.LogWarning(this.gameObject.name + "没有赋值Joysticks对象");
				Joysticks=GameObject.FindGameObjectWithTag("Joystick");
            }
        }
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			
			MoveInput(value.Get<Vector2>());
		}

		public void OnFastGun(InputValue value)
		{
			//Debug.Log("$$$$$$$$$$$$$ OnFastGun() value=" + value.isPressed);
			FastGunInput(value.isPressed);
			
		}

		public void OnDefendGun(InputValue value)
		{
			//	Debug.Log("@@@@@@@@@@@@@@@@OnJump value.isPressed=" + value.isPressed);
			DefendGunInput(value.isPressed);
		}

		public void OnAOEGun(InputValue value)
		{
			//Debug.Log("$$$$$$$$$$$$$ OnAOEGun() value=" + value.isPressed);
			AOEGunInput(value.isPressed);
		}
		public void OnSkillDisturbGun(InputValue value)
		{
			SkillDisturbGunInput(value.isPressed);
		}
		
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			
			look = newMoveDirection;
			//Debug.Log("MoveInput look=" + look);
		}

		public void FastGunInput(bool isShoot)
		{
			isFastGun = isShoot;
			//	Debug.Log("LookInput() newLookDirection=" + newLookDirection);
		}

		public void DefendGunInput(bool isDefend)
		{
			isDefendGun = isDefend;
		}

		public void AOEGunInput(bool isAOEGunT)
		{
			isAOEGun = isAOEGunT;
		}

		public void SkillDisturbGunInput(bool isSkillDisturbGunT)
		{
			isSkillDisturbGun = isSkillDisturbGunT;

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