using UnityEngine;

public class CameraManager:Singleton<CameraManager>
{
    private Camera _camera;
    private Camera _stageCamera;
    public Camera MainCamera
    {
        get
        {
            if (_camera == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
                if (obj)
                {
                    _camera = obj.GetComponent<Camera>();
                }
                else
                {
                    Logger.PrintError("没有发现Tag=MainCamera");
                }
            }
            return _camera;
        }
    }
    public Camera StageCamera
    {
        get
        {
            if (_stageCamera == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("UICamera");
                if (obj)
                {
                    _camera = obj.GetComponent<Camera>();
                }
                else
                {
                    Logger.PrintError("没有发现Tag=UICamera");
                }
            }
            return _camera;
        }
    }
}
