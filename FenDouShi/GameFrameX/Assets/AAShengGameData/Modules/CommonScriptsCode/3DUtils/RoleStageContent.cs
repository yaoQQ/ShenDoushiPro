using FairyGUI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoleStageContent:MonoBehaviour {

    GoWrapper _modelWrapper;
    [SerializeField]
    private Camera modelCamera;
    private void Start()
    {
        modelCamera = this.transform.Find("RoleCamera").GetComponent<Camera>();
        if (modelCamera == null)
        {
            Logger.PrintError("场景的RoleCamera 为空");
            return;
        }
        GameObject uiCamera = GameObject.FindGameObjectWithTag("UICamera");
        if (uiCamera == null)
        {
            Logger.PrintError("场景的UICamera 为空");
            return;
        }
        else
        {
            Camera uICamera = uiCamera.GetComponent<Camera>();
            // 设置主相机为Base类型
            UniversalAdditionalCameraData mainCameraData = modelCamera.GetComponent<UniversalAdditionalCameraData>();
            mainCameraData.renderType = CameraRenderType.Base;

            // 设置叠加摄像机为Overlay类型
            UniversalAdditionalCameraData overlayCameraData = uICamera.GetComponent<UniversalAdditionalCameraData>();
            overlayCameraData.renderType = CameraRenderType.Overlay;
            // 将Overlay摄像机添加到主相机堆栈
            mainCameraData.cameraStack.Add(uICamera);
            //// 可选：设置Overlay摄像机的清除深度（建议勾选）
            //overlayCameraData.clearDepth = true;
        }
    }

    public  void SetRoleStageContent(GGraph content)
    {
        _modelWrapper = new GoWrapper();
        content.asGraph.SetNativeObject(_modelWrapper);
        this.gameObject.transform.localPosition = new Vector3(0, 0, 1000);
        this.transform.localScale = new Vector3(120, 120, 120);
        this.transform.localEulerAngles = new Vector3(0, 100, 0); 

        _modelWrapper.SetWrapTarget(this.gameObject, true);
    }
}