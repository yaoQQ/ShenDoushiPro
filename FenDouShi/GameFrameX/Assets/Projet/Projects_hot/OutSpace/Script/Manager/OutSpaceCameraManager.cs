using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using StarterAssets;

public class OutSpaceCameraManager : Singleton<OutSpaceCameraManager>
{
    private Camera _Camera;
    private Transform _player;
    private Camera _LayerParentCamera;
    private OutSpaceStarterAssetsInputs _startInput;

    public void Init()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(OutSpaceTags.mainCamera);
        if (obj != null)
        {
            _Camera = obj.GetComponent<Camera>();
        }
    }
    public Camera MainCamera
    {
        get
        {
            if (_Camera == null)
            {
               GameObject obj= GameObject.FindGameObjectWithTag(OutSpaceTags.mainCamera);
                if(obj != null)
                {
                    _Camera = obj.GetComponent<Camera>();
                }
            }
            return _Camera;
        }
    }
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag(OutSpaceTags.mainCamera).transform;
            }
            return _player;
        }
    }
    public OutSpaceStarterAssetsInputs StartInput
    {
        get
        {
            if (_startInput == null)
            {
                _startInput= MainCamera.gameObject.GetComponent<OutSpaceStarterAssetsInputs>();
                Logger.PrintColor("yellow", "@@@@@@@@_startInput=" + _startInput);
            }
            return _startInput;
        }
    }

    public void isSwitchSkybox()
    {
        if (MainCamera == null) return;
        CameraClearFlags falg = MainCamera.clearFlags;
        MainCamera.clearFlags = (falg== CameraClearFlags.Depth)? CameraClearFlags.Skybox : CameraClearFlags.Depth;
    }

    public bool isShake = true;
    public void isSwitchShake()
    {
        isShake = !isShake;
    }
    public void shakeCamera(bool isSmall=true)
    {
        if (!isShake)
        {
            return;
        }
        if (isSmall)
        {
           iTween.ShakePosition(OutSpaceCameraManager.Instance.MainCamera.gameObject, new Vector3(0.02f, 0.02f, 0), 0.5f);
        }
        else
        {

            iTween.ShakePosition(OutSpaceCameraManager.Instance.MainCamera.gameObject, new Vector3(0.1f, 0.1f, 0), 1);
        }
    }

    CameraFilterPack_Vision_Blood blood;
    public ParticleSystem damageEffect;
    public void showCameraDamage()
    {
        //blood = MainCamera.GetComponent<CameraFilterPack_Vision_Blood>();
        //blood.show();
        //if (damageEffect== null)
        //{
        //   GameObject obj= MyUtils.LoadEffectPrefab("DamageEffect");
        //    if (obj == null) return;
        //    damageEffect = obj.GetComponent<ParticleSystem>();
        //    damageEffect.transform.parent = Player;
        //    damageEffect.transform.localPosition = Vector3.zero;
        //}
        //if (damageEffect != null)
        //{
        //    damageEffect.gameObject.SetActive(true);
        //}
    }

}

