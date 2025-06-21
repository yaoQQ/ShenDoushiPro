// 该脚本用于控制Unity中天空盒相机的位置和旋转。
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCam : MonoBehaviour
{
    // 跟随天空盒相机的玩家相机。
    public Transform playerCam;

    // 玩家相机位置和天空盒相机位置之间的比例因子。
    public float proportionality;

    // 天空盒相机的初始位置。
    public Vector3 startPos;

    // 天空盒相机相对于其初始位置移动的位置。
    private Vector3 bgMovePos = Vector3.zero;

    // 天空盒相机移动的速度。
    public float moveSpeed = 1;

    // 根据玩家相机位置重置的对象。
    [Header("根据摄像机位置重置的对象")]
    public Transform resetContent;

    // 玩家相机移动多少距离后重置。
    [Header("摄像机位置移动多少距离后重置")]
    public int moveTargetPosToRest = 60;

    // 重置对象的初始位置。
    private Vector3 initResetContentPos;

    // 重置对象已移动的次数。
    [Header("只读：重置的偏移量")]
    [SerializeField]
    private int moveRestCount = 0;

    // 重置对象再次移动之前的延迟时间。
    private float delayTime = 5;

    // 天空盒相机是否自动移动。
    public bool isAutoMove = false;

    //缓动位置距离速度
    public int camaraLearpSpeed = 2;
    private Transform _transform;
    public Action MoveCallBack;


    void Start()
    {
        // Cache the transform component.
        _transform = transform;
        // 设置天空盒相机的初始位置。
        startPos = transform.position;

        // 设置重置对象的初始位置。
        if (resetContent)
        {
            initResetContentPos = resetContent.transform.localPosition;
        }
#if !AR_SYSTEM
        playerCam = GameObject.FindGameObjectWithTag(OutSpaceTags.mainCamera).transform;
#endif

    }

    // Update is called once per frame
    void Update()
    {
        // 如果天空盒相机自动移动，则更新其位置和旋转。
        if (isAutoMove)
        {
            bgMovePos += moveSpeed * _transform.forward * Time.deltaTime;
            Vector3 targetPos = startPos + playerCam.position * proportionality + bgMovePos;
            _transform.position = Vector3.Lerp(_transform.position, targetPos, Time.deltaTime * camaraLearpSpeed);
           // this.transform.position = targetPos;
        }
        else//否则与ARcamara位置保持一致
        {
            Vector3 targetPos = playerCam.position ;
            _transform.position =Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime* camaraLearpSpeed);
        }
        _transform.rotation = playerCam.rotation;

        RestChildContent();
    }
    // 重置对象的子对象，重新循环。
    private void RestChildContent()
    {
        if (initResetContentPos == null)
        {
            return;
        }
        delayTime -= Time.deltaTime;
        if (delayTime > 0)
        {
            return;
        }
        int moveNum = (int)(Mathf.Abs(bgMovePos.z)) / moveTargetPosToRest;
        if (moveNum != moveRestCount)
        {
            moveRestCount = moveNum;
            playerCam.position = Vector3.zero;
             resetContent.transform.localPosition = bgMovePos + initResetContentPos;
            delayTime = 5;
            if (MoveCallBack != null)
            {
                MoveCallBack();
            }
        }
    }
}