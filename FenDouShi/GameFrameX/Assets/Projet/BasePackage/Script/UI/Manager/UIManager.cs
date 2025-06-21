using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem.UI;
public class UIManager : Singleton<UIManager>
{
    public bool isInit = false;

    public GameObject UIRoot;

    private EventSystem esys;

    public Camera UICamera;

    public const float GlobalUIWidth = 1080f;
    public const float GlobalUIHigh = 1920f;

    public float UIRootWidthValue
    {
        get
        {
            return GlobalUIWidth;
        }
    }


    public float UIRootHighValue
    {
        get
        {
            return GlobalUIWidth * Screen.height / Screen.width;

        }
    }



    public void Init(GameObject root)
    {
        Logger.PrintColor("blue", "8.17 res UIManager Init()1");
        
        UIRoot = AddUICanvas(root);
        Logger.PrintColor("blue", "8.17 res UIManager Init()2");
        CreateLayerPanel();
        Logger.PrintColor("red", "res @@@@@@CreateLayerPanel Init()");
    }

    #region 代码创建主UIRoot

    GameObject AddUICanvas(GameObject root)
    {
        Logger.PrintColor("blue", "res AddUICanvas root=" + root);
        if (isInit) return UIRoot;
        isInit = true;
        var go = CreateNewUI(root);
        go.transform.SetParent(root.transform);
        return go;
    }
    GameObject CreateNewUI(GameObject parent)
    {
        // Root for the UI
        Logger.PrintColor("blue", "res CreateNewUI parent=" + parent.name);
        GameObject rootGo = new GameObject("UIRoot");
      
        rootGo.layer = LayerMask.NameToLayer("UI");
        Logger.PrintColor("blue", "res CreateNewUI rootGo.layey");
        CreateEventSystem(rootGo);
        CreateUICamera(rootGo);
       
        return rootGo;
    }
    void CreateEventSystem(GameObject root)
    {
        Logger.PrintColor("blue", "CreateEventSystem ");
        if (esys == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.layer = LayerMask.NameToLayer("UI");
            eventSystem.transform.SetParent(root.transform);
            esys = eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<InputSystemUIInputModule>();
            //TouchInputModule touchInputModule = eventSystem.AddComponent<TouchInputModule>();
            //touchInputModule.forceModuleActive = true;
        }
    }
    void CreateUICamera(GameObject root)
    {
        Logger.PrintColor("blue", "CreateUICamera ");
        GameObject uiCameraGo = new GameObject("UICamera");
        uiCameraGo.transform.SetParent(root.transform);
        UICamera = uiCameraGo.AddComponent<Camera>();

       
        UICamera.depth = 50;
        UICamera.orthographic = true;
        UICamera.clearFlags = CameraClearFlags.Depth;
        UICamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
        UICamera.cullingMask |= 1 << LayerMask.NameToLayer("UI_Effect");
        UICamera.nearClipPlane = -10000f;
        UICamera.farClipPlane = 2000f;

     
        UICamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;
        addUICamera(CameraManager.Instance.MainCamera);

    }
    public void addUICamera(Camera sceneCamera)
    {
        sceneCamera.GetUniversalAdditionalCameraData().cameraStack.Add(UICamera);
    }
    private void CreateLayerPanel()
    {
        Logger.PrintColor("blue", "Res CreateLayerPanel begain");
        //test@@@
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Loading_View, "LayerPanel_Loading");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Global_View, "LayerPanel_Global");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Map_View, "LayerPanel_Map");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.MapFloat_View, "LayerPanel_MapFloat");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Main_view, "LayerPanel_Main");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Platform_Second_View, "LayerPanel_Platform_Second");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Platform_Help_View, "LayerPanel_Platform_Help");
         GameObject game1=  UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Game_1, "LayerPanel_Game_1");
        CanvasScaler canvasScaler = game1.GetComponent<CanvasScaler>();
        canvasScaler.matchWidthOrHeight =0f;
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Game_2, "LayerPanel_Game_2");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Game_3, "LayerPanel_Game_3");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Game_4, "LayerPanel_Game_4");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Pop_view, "LayerPanel_Pop");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Alert_box, "LayerPanel_Alert");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Feedback_Tip, "LayerPanel_Feedback");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Mask_View, "LayerPanel_Mask");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.NoviceGuide_View, "LayerPanel_NoviceGuide");
        UIViewManager.Instance.CreateUILayerPanel((int)UIViewType.Plot_View, "LayerPanel_Plot");
        
        Logger.PrintColor("blue", "Res  CreateLayerPanel end");
    }
    //test@@@
    public GameObject getCanvasByType(UIViewType viewType)
    {
    
        //test@@@
         return UIViewManager.Instance.getCanvasByType(viewType);
    }
    #endregion


    #region public



    IEnumerator Preload(Action OnLoadEnd)
    {
        List<String> loadList = new List<string>();
        int loadedValue = 0;
        for (int i = 0; i < loadList.Count; i++)
        {
            string value = loadList[i];
            //UILoadTool.Instance.CreateUI(value, (g) => { loadedValue++; }, false);
        }
        while (loadedValue != loadList.Count)
        {
            yield return 0;
        }

        if (OnLoadEnd != null)
        {
            OnLoadEnd.Invoke();
        }

    }


    //public void OnSceneChangeBefore(Action OnLoadEnd)
    //{
    //    //UIManager.Instance.GC();
    //    MainThread.Instance.StartCoroutine(Preload(OnLoadEnd));

    //}
    public Texture2D GetSnapshot(int x, int y, int w, int h)
    {
        if (UICamera == null) return null;

        float _xScale = Screen.width / 1080;
        float _yScale = Screen.height / 1920;
        float x_pix = _xScale * x;
        float y_pix = _yScale * y;

        RenderTexture tempRt = RenderTexture.GetTemporary(Screen.width, Screen.height);
        UICamera.targetTexture = tempRt;
        UICamera.Render();
        Texture2D tex2D = new Texture2D(w, h, TextureFormat.RGBA32, false);
        RenderTexture.active = UICamera.targetTexture;
        tex2D.ReadPixels(new Rect(x_pix, y_pix, w, h), 0, 0);
        tex2D.Apply();
        UICamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tempRt);
        tempRt = null;
        return tex2D;
    }


    public Texture2D GetSnapshot(int w)
    {
        if (UICamera == null) return null;
        int h = Mathf.FloorToInt((float)w / UICamera.aspect);
        RenderTexture tempRt = RenderTexture.GetTemporary(w, h);
        UICamera.targetTexture = tempRt;
        UICamera.Render();
        Texture2D tex2d = new Texture2D(w, h, TextureFormat.RGBA32, false);
        RenderTexture.active = UICamera.targetTexture;
        tex2d.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        tex2d.Apply();
        UICamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tempRt);
        tempRt = null;
        return tex2d;
    }

    public Texture2D GetSnapshot(Vector2 size)
    {
        if (UICamera == null) return null;
        int w = Mathf.CeilToInt(size.x);
        int h = Mathf.CeilToInt(size.y);
        RenderTexture tempRt = RenderTexture.GetTemporary(w, h);
        UICamera.targetTexture = tempRt;
        UICamera.Render();
        Texture2D tex2D = new Texture2D(w, h, TextureFormat.RGBA32, false);
        RenderTexture.active = UICamera.targetTexture;
        tex2D.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        tex2D.Apply();
        UICamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tempRt);
        tempRt = null;
        return tex2D;
    }
    public Texture2D GetSnapshot()
    {
        if (UICamera == null) return null;
        int w = Mathf.CeilToInt(UIRootWidthValue * 0.25f);
        int h = Mathf.CeilToInt(UIRootHighValue * 0.25f);
        RenderTexture tempRt = RenderTexture.GetTemporary(w, h);
        UICamera.targetTexture = tempRt;
        UICamera.Render();
        Texture2D tex2D = new Texture2D(w, h, TextureFormat.RGBA32, false);
        RenderTexture.active = UICamera.targetTexture;
        tex2D.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        tex2D.Apply();
        UICamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tempRt);
        tempRt = null;
        return tex2D;
    }






    #endregion



    public void GC()
    {
        //UILoadTool.Instance.GetUIPoolProxy().GC();
        //HudManager.Instance.ClearPool();
    }
    public void Destroy() {
        isInit = false;
        UIRoot = null;
        UIViewManager.Instance.Dispose();
    }

}
