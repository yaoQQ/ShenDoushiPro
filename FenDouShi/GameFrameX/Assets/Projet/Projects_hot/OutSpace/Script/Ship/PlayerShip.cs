using StarterAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif
//玩家属性
public class PlayerShip : ShipBase
{
    public PlayerBlood blood;
    public EnemyBlood EnemyBlood;
    private int enemyLayout;
    public OutSpaceStarterAssetsInputs _input;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    private PlayerInput _playerInput;
#endif
    private DistanceComparer m_distanceComparer;

    [Header("玩家飞船基本属性类型")]
    public CharacterInfo characterInfo;

    private Transform cachTransform;
    public override void Awake()
    {
        base.Awake();
        cachTransform = this.transform;
        m_distanceComparer = new DistanceComparer(cachTransform, false);
        if (blood == null)
        {
            GameObject obj = GameObject.Find("PlayerbloodPanel");
            blood = obj.GetComponent<PlayerBlood>();
            blood.totalVaue = characterInfo.totalLife;
            this.speed = characterInfo.speed;
        }
        if (EnemyBlood == null)
        {
            GameObject obj = GameObject.Find("EnemybloodPanel");
            EnemyBlood = obj.GetComponent<EnemyBlood>();
            EnemyBlood.totalVaue = life;
        }
        enemyLayout = LayerMask.GetMask("Enemy")| LayerMask.GetMask("Collider");

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

        _input = this.gameObject.GetComponent<OutSpaceStarterAssetsInputs>();
        _playerInput = this.gameObject.GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
    }
    //void OnDrawGizmos()
    //{
    //    // Debug.Log("OnDrawGizmos() Time=" + Time.time);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(cachTransform.position, 0.1f);
    //    Gizmos.DrawRay(cachTransform.position, cachTransform.forward);

    //    //Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - 0.2f,
    //    // transform.position.z);

    //    ////触碰的地面
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawLine(this.transform.position, spherePosition);
    //    //Gizmos.color = Color.green;
    //    //Gizmos.DrawSphere(spherePosition, 100);
    //}

    private EnemyShipBase targetEnemy = null;
    private float delayFreshTime = 0.3f;
    private void Update()
    {

        RaycastHit hitInfo;
        if (Physics.Raycast(cachTransform.position, cachTransform.forward, out hitInfo, MyUtils.MaxEnemyDistance, enemyLayout))
        {
            //更新当前敌方血量
            if (targetEnemy != null && targetEnemy.transform == hitInfo.transform)
            {
                delayFreshTime -= Time.deltaTime;
                if (delayFreshTime < 0)
                {
                    EnemyBlood.SetBloodValue(targetEnemy.life);
                    delayFreshTime = 0.3f;
                }
                return;
            }
            targetEnemy = hitInfo.transform.GetComponent<EnemyShipBase>();
            if (targetEnemy)
            {
                EnemyBlood.gameObject.SetActive(true);
                EnemyBlood.totalVaue = targetEnemy.totalLife;
                EnemyBlood.SetBloodValue(targetEnemy.life);
                EnemyBlood.SetBloodName(targetEnemy.ShipName);
            }
        }
        else
        {
            if (targetEnemy)
            {
                targetEnemy = null;
            }
            if (EnemyBlood.gameObject.activeSelf)
                EnemyBlood.gameObject.SetActive(false);
        }

    }
    private void LateUpdate()
    {
#if !AR_SYSTEM
        CameraRotation();
#endif
    }

    public override void Damage(float num, GameObject hitFromTarget)
    {
        if (blood != null)
        {
            blood.damage(num, hitFromTarget);
        }
        if (num >= 10)
        {
            OutSpaceCameraManager.Instance.shakeCamera();
        }
        else if (num >= 15)
        {
            OutSpaceCameraManager.Instance.shakeCamera(true);
        }
        // Logger.PrintColor("red", "==============Damage() num=" + num);
        OutSpaceCameraManager.Instance.showCameraDamage();
    }
    //void Update()
    //{
    //    Look();
    //    Move();
    //}
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED


    [Header("InputStartAssert鼠标移动相关")]
    private const float _threshold = 0.01f;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_input == null) {
            return;
        }
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch -= _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        this.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
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
#endif

    // 筛选出最近的敌人
    public EnemyShipBase GetEarestEnemy()
    {
        EnemyShipBase earEnemy= MonsterManager.Instance.SortDistance(m_distanceComparer);
        return earEnemy;
    }
}
