using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    // [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : SingleMonobehaviour3<ThirdPersonController>
    {
        public bool isMyPlayer = true;
      //  public ReadyPlayerMe.VoiceHandler voiceHandle;
        public GameObject FollowCamera;
        private GravityAttractor _currAttractor;

        new private Rigidbody rigidbody;

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0;
        private float _rotationVelocity;

        private float _terminalVelocity = 53.0f;
        private float _verticalVelocity = 0;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDSit;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
           
        
            Debug.Log("<color='blue'>===========ThirdPersonController Awake()==============</color>");
        
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            rigidbody = this.transform.GetComponent<Rigidbody>();
            //   rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;


            //if (SceneManager.Instance.curLuaScene == null)//TEst 测试用
            //{
            //    PlanetGameManager.Instance.initGameScene();
            //    PrinceDialogueManager.Instance.init();
            //    GameAudioManager.Instance.InitializeAudio();
            //    PrinceDialogueManager.Instance.ShowDialog(DialogConversationID.GameStart);
            //    return;
            //}
            //else
            //{
            //    Logger.PrintColor("red", "getSceneName=" + SceneManager.Instance.curLuaScene.getSceneName());
            //    if (SceneManager.Instance.curLuaScene.getSceneName() != "StartGame")
            //    {

            //        PlanetGameManager.Instance.initGameScene();
            //    }
            //}
        }


        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();


            //moveAmount = new Vector3(_input.move.x, 0.0f, _input.move.y) * Time.deltaTime;
            //rigidbody.MovePosition(rigidbody.position + this.transform.TransformDirection(moveAmount));



        }

        private void FixedUpdate()
        {

        }
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDSit = Animator.StringToHash("Sit");
        }
        //private void OnDrawGizmos()
        //{

        //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y- GroundedOffset,
        //     transform.position.z);

        //    //触碰的地面
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(this.transform.position, spherePosition);
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(spherePosition, GroundedRadius);
        //}

        private void GroundedCheck()
        {
            // set sphere position, with offset
            //  Vector3 toPlanetDir = (attractor.transform.position - this.transform.position).normalized;//朝向星球的引力方向

            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
            // update animator if using character
            //   Debug.Log("@@@@@@@@@@Grounded="+ Grounded);

            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
                if (Grounded)
                {
  
                   _animator.SetBool(_animIDSit, _input.sit);
                    
                }
            }
        
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        public bool isShpwSmooth = false;
        private void Move()
        {
            if (_input.sit)
            {
                return;
            }
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            //@@@@@test
            if (_input.move == Vector2.zero)
            {
                targetSpeed = 0.0f;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }

            // a reference to the players current horizontal velocity
            //@@@@@@@@test
            // float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float currentHorizontalSpeed = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z).magnitude;

            //    float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            //if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            //    currentHorizontalSpeed > targetSpeed + speedOffset)
            //{
            //    // creates curved result rather than a linear one giving a more organic speed change
            //    // note T in Lerp is clamped, so we don't need to clamp our speed
            //    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
            //        Time.deltaTime * SpeedChangeRate);

            //    // round speed to 3 decimal places
            //    _speed = Mathf.Round(_speed * 1000f) / 1000f;
            //}
            //else
            //{
            _speed = targetSpeed;
            //}

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            Vector3 moveDirection = Vector3.zero;

            CharacterPlanetRotate();
            if (_input.move != Vector2.zero)
            {
                //   AttractorPlanet();
                moveDirection = this.transform.forward.normalized;
            }

            // 使角色移动和添加重力  _verticalVelocity 弹跳的矢量
            // @@@@@@@@需优化移动抖动  ====》向心力使移动的x、y值片大， 解决 使物体在没forward移动轨迹是沿球边缘移动
            if (moveDirection != Vector3.zero || (_verticalVelocity > 0 || !Grounded))
            {
                //   showSmoothMove(moveDirection);
                if (!isShpwSmooth)
                {
                    deltMove(moveDirection);
                }
                else
                {
                    showSmoothMove(moveDirection);
                }
                //rigidbody.MovePosition(rigidbody.position + this.transform.TransformDirection(moveAmount));

            }

            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }
        private void CharacterPlanetRotate()
        {
            //if (_input && _input.fly)
            //{
            //    CharacterFlyRotate();
            //    return;
            //}
            if (_input.move == Vector2.zero)
            {
                return;
            }
            //下面是 摄像机水平旋转  使玩家水平朝向摄像机方向
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
            float angel = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            _targetRotation = angel;
            if (this.transform.up.y > -0.9f)
            {
                _targetRotation = angel + _mainCamera.transform.eulerAngles.y;
            }
            //飞行状态 可以向下朝向和运动 ，并添加旋转缓动（星球运动不可以添加，根据当前角色角度计算的）
            if (_input && _input.fly)
            {
                switchFlyRotate();

            }
            else
            {
                AttractorPlanet();
            }
        }
        private void deltMove(Vector3 moveDirection)
        {
            Vector3 targetPos = rigidbody.position + moveDirection * _speed * Time.deltaTime;
            Vector3 upPos = this.transform.up * _verticalVelocity * Time.deltaTime;//弹跳高度
            rigidbody.MovePosition(targetPos + upPos);
        }
        private void showSmoothMove(Vector3 moveDirection)
        {
            Vector3 targetPos = rigidbody.position + moveDirection;
            Vector3 smoothMove = Vector3.Lerp(rigidbody.position, targetPos, _speed * Time.deltaTime);
            //   Debug.Log("distace =" + Vector3.Distance(rigidbody.position, smoothMove));
            Vector3 upPos = this.transform.up * _verticalVelocity * Time.deltaTime;//弹跳高度
            rigidbody.MovePosition(smoothMove + upPos);
        }

        /// <summary>
        /// 飞行状态 可以向下朝向和运动 ，并添加旋转缓动（星球运动不可以添加，根据当前角色角度计算的）
        /// </summary>
        private void switchFlyRotate()
        {
            // rotate to face input direction relative to camera position
            float charecterX = _mainCamera.transform.eulerAngles.x;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime); ;


            transform.rotation = Quaternion.Euler(charecterX, rotation, 0.0f);
        }
        /// <summary>
        /// 使玩家Y轴朝向球心（球面运动）
        /// </summary>
        private void AttractorPlanet()
        {
            transform.rotation = Quaternion.Euler(0, _targetRotation, 0);
            Vector3 targetDir = (_currAttractor.transform.position - this.transform.position).normalized;//朝向星球的引力方向
            Vector3 bodyDown = -this.transform.up;//物体向上

            this.transform.rotation = Quaternion.FromToRotation(bodyDown, targetDir) * this.transform.rotation;//从自身到目标方向X身体当前的旋转=使目标Up方向朝星球
        }

        /// <summary>
        /// 设置走动的星球
        /// </summary>
        public GravityAttractor planetAttract
        {
            set
            {
                if (value)
                {
                    _currAttractor = value;
                    Gravity = -_currAttractor.gravity;
                    AttractorPlanet();
                }
            }
            get
            {
                return _currAttractor;
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);//36

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            //@@@test
            if (_input.fly && _verticalVelocity <= 0)
            {
                _verticalVelocity = 0;
                return;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)//36《53.0f
            {
                _verticalVelocity += Gravity * Time.deltaTime;//36-4
                //Debug.Log("_verticalVelocity < _terminalVelocity _verticalVelocity=" + _verticalVelocity);
            }

        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    // AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], this.transform.position, FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                //   AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                AudioSource.PlayClipAtPoint(LandingAudioClip, this.transform.position, FootstepAudioVolume);
            }
        }

        public void SpeakVoice(AudioClip clip)
        {
            //if (clip)
            //    voiceHandle.PlayAudioClip(clip);
        }
        public void StopSpeak()
        {
          //  voiceHandle.StopAudioClipSource();
        }
    }
}