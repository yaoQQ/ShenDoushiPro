using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UITools2
{
    public class CanvasInfo
    {
        public Canvas canvas;
        public CanvasScaler canvasScaler;
        public GraphicRaycaster graphicRaycaster;

        public void SetCanvasParam(int orderValue)
        {
            canvas.sortingOrder = orderValue;
            canvas.planeDistance = orderValue * -1;
        }
    }
    public static CanvasInfo SetCanvasToUIGo(GameObject go)
    {
        CanvasInfo info = new CanvasInfo();
        go.layer = LayerMask.NameToLayer("UI");
        info.canvas = go.GetComponent<Canvas>();
        if (info.canvas == null)
        {
            info.canvas = go.AddComponent<Canvas>();
        }
      //test@@@
        //info.canvas.renderMode = RenderMode.ScreenSpaceCamera;info.canvas.worldCamera = UIManager.Instance.UICamera;
        info.canvas.sortingLayerName = "UIP";
        //info.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        info.canvasScaler = go.AddComponent<CanvasScaler>();
        info.canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        info.canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        //tett@@@
        //info.canvasScaler.referenceResolution = new Vector2(UIManager.GlobalUIWidth, UIManager.GlobalUIHigh);
        info.canvasScaler.matchWidthOrHeight = 0.5f;
        info.canvas.planeDistance = 0f;
        info.graphicRaycaster = go.AddComponent<GraphicRaycaster>();
        return info;
    }

    public static void SwitchScreenOrientation(GameObject go, bool isHorizontal)
    {
        CanvasScaler cs = go.GetComponent<CanvasScaler>();
        if (isHorizontal)
        {
            //test@@@
         //   cs.referenceResolution = new Vector2(UIManager.GlobalUIHigh, UIManager.GlobalUIWidth);
            cs.matchWidthOrHeight = 1;
        }
        else
        {
            //test@@@
            //cs.referenceResolution = new Vector2(UIManager.GlobalUIWidth, UIManager.GlobalUIHigh);
            cs.matchWidthOrHeight = 0;
        }
    }

    public static void SetCanvasMatch(GameObject go, float value)
    {
        CanvasScaler cs = go.GetComponent<CanvasScaler>();
        cs.matchWidthOrHeight = value;
    }

    /// <summary>
    /// 设置父容器和对齐;
    /// </summary>
    public static void SetParentAndAlign(GameObject child, GameObject parent)
    {
        child.transform.SetParent(parent.transform);
        if (child.transform.parent as RectTransform)
        {
            child.transform.localScale = Vector3.one;
            RectTransform rect = child.transform as RectTransform;
            //rect.anchorMin = Vector2.zero;
            //rect.anchorMax = Vector2.one;
            //rect.anchoredPosition = Vector2.zero;

            if (rect != null)
            {
                rect.anchoredPosition3D = new Vector3(0, 0, 0);

                if (rect.anchorMin == new Vector2(0f, 0f) && rect.anchorMax == new Vector2(1f, 1f) && rect.anchorMax == new Vector2(1f, 1f))
                {
                    rect.sizeDelta = new Vector2(0, 0);
                }
            }

        }
    }
    /// <summary>
    /// 根据名字排序用;
    /// </summary>
    /// <param name="go"></param>
    public static void SortChildrenByName(GameObject go)
    {
        //
        //        List<Transform> children = new List<Transform>();
        //        for (int i = go.transform.childCount - 1; i >= 0; i--)
        //        {
        //            Transform child = go.transform.GetChild(i);
        //            children.Add(child);
        //            child.SetParent(null);
        //        }
        //        children.Sort((Transform t1, Transform t2) => { return t1.name.CompareTo(t2.name); });
        //        foreach (Transform child in children)
        //        {
        //            //测BUG用的。以后会删;
        //            try
        //            {
        //                child.SetParent(go.transform);
        //            }
        //            catch (Exception e)
        //            {
        //                Debug.LogError("go.transform.position:" + go.transform.position);
        //                Debug.LogError("child.transform.position:" + go.transform.position);
        //            }
        //
        //
        //        }
    }
    public static Color IntToColor(int val)
    {
        float inv = 1f / 255f;
        Color c = Color.black;
        c.r = inv * ((val >> 24) & 0xFF);
        c.g = inv * ((val >> 16) & 0xFF);
        c.b = inv * ((val >> 8) & 0xFF);
        c.a = inv * (val & 0xFF);
        return c;
    }



    public static Vector3 GetUguiPosByOffset(Vector3 nowPos, float xOffset, float yOffset)
    {
        //test@@@
        //Vector3 vpPos = UIManager.Instance.UICamera.WorldToViewportPoint(nowPos);
        //float xOffsetPer = xOffset / UIManager.GlobalUIWidth;
        //float yOffsetPer = yOffset / UIManager.GlobalUIHigh;
        //vpPos = new Vector3(vpPos.x + xOffsetPer, vpPos.y + yOffsetPer, vpPos.z);
        //vpPos = UIManager.Instance.UICamera.ViewportToWorldPoint(vpPos);
        //return vpPos;
        return Vector3.zero;
    }

    public static void SetUIScale(GameObject go, Vector2 scaleValue)
    {
        RectTransform rt = go.transform as RectTransform;
        if (rt != null)
        {
            rt.localScale = scaleValue;
        }
    }

    public static void DOTweenMatAttribute(Material mat, string attrName, float value, float time)
    {
        float tweenValue = 0;
        //Tweener t = DOTween.To(() => tweenValue, x => tweenValue = x, value, time);
        //t.OnUpdate(() => mat.SetFloat(attrName, tweenValue));
    }
    //TOOls Res
    public static UIAlign2 GetWidgetAlign(RectTransform rt)
    {
        UIAlign2 align = UIAlign2.Stretch_Stretch;
        if (rt.anchorMin == new Vector2(0f, 1f) && rt.anchorMax == new Vector2(0f, 1f))
            align = UIAlign2.Top_Left;
        else if (rt.anchorMin == new Vector2(0.5f, 1f) && rt.anchorMax == new Vector2(0.5f, 1f))
            align = UIAlign2.Top_Center;
        else if (rt.anchorMin == new Vector2(1f, 1f) && rt.anchorMax == new Vector2(1f, 1f))
            align = UIAlign2.Top_Right;
        else if (rt.anchorMin == new Vector2(0f, 1f) && rt.anchorMax == new Vector2(1f, 1f))
            align = UIAlign2.Top_Stretch;
        else if (rt.anchorMin == new Vector2(0f, 0.5f) && rt.anchorMax == new Vector2(0f, 0.5f))
            align = UIAlign2.Middle_Left;
        else if (rt.anchorMin == new Vector2(0.5f, 0.5f) && rt.anchorMax == new Vector2(0.5f, 0.5f))
            align = UIAlign2.Middle_Center;
        else if (rt.anchorMin == new Vector2(1f, 0.5f) && rt.anchorMax == new Vector2(1f, 0.5f))
            align = UIAlign2.Middle_Right;
        else if (rt.anchorMin == new Vector2(0f, 0.5f) && rt.anchorMax == new Vector2(1f, 0.5f))
            align = UIAlign2.Middle_Stretch;
        else if (rt.anchorMin == new Vector2(0f, 0f) && rt.anchorMax == new Vector2(0f, 0f))
            align = UIAlign2.Bottom_Left;
        else if (rt.anchorMin == new Vector2(0.5f, 0f) && rt.anchorMax == new Vector2(0.5f, 0f))
            align = UIAlign2.Bottom_Center;
        else if (rt.anchorMin == new Vector2(1f, 0f) && rt.anchorMax == new Vector2(1f, 0f))
            align = UIAlign2.Bottom_Right;
        else if (rt.anchorMin == new Vector2(0f, 0f) && rt.anchorMax == new Vector2(1f, 0f))
            align = UIAlign2.Bottom_Stretch;
        else if (rt.anchorMin == new Vector2(0f, 0f) && rt.anchorMax == new Vector2(0f, 1f))
            align = UIAlign2.Stretch_Left;
        else if (rt.anchorMin == new Vector2(0.5f, 0f) && rt.anchorMax == new Vector2(0.5f, 1f))
            align = UIAlign2.Stretch_Center;
        else if (rt.anchorMin == new Vector2(1f, 0f) && rt.anchorMax == new Vector2(1f, 1f))
            align = UIAlign2.Stretch_Right;
        else if (rt.anchorMin == new Vector2(0f, 0f) && rt.anchorMax == new Vector2(1f, 1f))
            align = UIAlign2.Stretch_Stretch;
        return align;
    }
    public static void SetWidgetAlign(RectTransform rf, UIAlign2 align)
    {
        switch (align)
        {
            case UIAlign2.Top_Left:
                rf.anchorMin = new Vector2(0f, 1f);
                rf.anchorMax = new Vector2(0f, 1f);
                rf.pivot = new Vector2(0f, 1f);
                break;
            case UIAlign2.Top_Center:
                rf.anchorMin = new Vector2(0.5f, 1f);
                rf.anchorMax = new Vector2(0.5f, 1f);
                rf.pivot = new Vector2(0.5f, 1f);
                break;
            case UIAlign2.Top_Right:
                rf.anchorMin = new Vector2(1f, 1f);
                rf.anchorMax = new Vector2(1f, 1f);
                rf.pivot = new Vector2(1f, 1f);
                break;
            case UIAlign2.Top_Stretch:
                rf.anchorMin = new Vector2(0f, 1f);
                rf.anchorMax = new Vector2(1f, 1f);
                rf.pivot = new Vector2(0.5f, 1f);
                break;
            case UIAlign2.Middle_Left:
                rf.anchorMin = new Vector2(0f, 0.5f);
                rf.anchorMax = new Vector2(0f, 0.5f);
                rf.pivot = new Vector2(0f, 0.5f);
                break;
            case UIAlign2.Middle_Center:
                rf.anchorMin = new Vector2(0.5f, 0.5f);
                rf.anchorMax = new Vector2(0.5f, 0.5f);
                rf.pivot = new Vector2(0.5f, 0.5f);
                break;
            case UIAlign2.Middle_Right:
                rf.anchorMin = new Vector2(1f, 0.5f);
                rf.anchorMax = new Vector2(1f, 0.5f);
                rf.pivot = new Vector2(1f, 0.5f);
                break;
            case UIAlign2.Middle_Stretch:
                rf.anchorMin = new Vector2(0f, 0.5f);
                rf.anchorMax = new Vector2(1f, 0.5f);
                rf.pivot = new Vector2(0.5f, 0.5f);
                break;
            case UIAlign2.Bottom_Left:
                rf.anchorMin = new Vector2(0f, 0f);
                rf.anchorMax = new Vector2(0f, 0f);
                rf.pivot = new Vector2(0f, 0f);
                break;
            case UIAlign2.Bottom_Center:
                rf.anchorMin = new Vector2(0.5f, 0f);
                rf.anchorMax = new Vector2(0.5f, 0f);
                rf.pivot = new Vector2(0.5f, 0f);
                break;
            case UIAlign2.Bottom_Right:
                rf.anchorMin = new Vector2(1f, 0f);
                rf.anchorMax = new Vector2(1f, 0f);
                rf.pivot = new Vector2(1f, 0f);
                break;
            case UIAlign2.Bottom_Stretch:
                rf.anchorMin = new Vector2(0f, 0f);
                rf.anchorMax = new Vector2(1f, 0f);
                rf.pivot = new Vector2(0.5f, 0f);
                break;
            case UIAlign2.Stretch_Left:
                rf.anchorMin = new Vector2(0f, 0f);
                rf.anchorMax = new Vector2(0f, 1f);
                rf.pivot = new Vector2(0f, 0.5f);
                break;
            case UIAlign2.Stretch_Center:
                rf.anchorMin = new Vector2(0.5f, 0f);
                rf.anchorMax = new Vector2(0.5f, 1f);
                rf.pivot = new Vector2(0.5f, 0.5f);
                break;
            case UIAlign2.Stretch_Right:
                rf.anchorMin = new Vector2(1f, 0f);
                rf.anchorMax = new Vector2(1f, 1f);
                rf.pivot = new Vector2(1f, 0.5f);
                break;
            case UIAlign2.Stretch_Stretch:
                rf.anchorMin = new Vector2(0f, 0f);
                rf.anchorMax = new Vector2(1f, 1f);
                rf.pivot = new Vector2(0.5f, 0.5f);
                break;
        }
    }
}
