using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
//using Spine;
namespace UIWidget
{
    public abstract class UIBaseWidget : UIBehaviour, ICanvasRaycastFilter
    {

        public bool exportSign;

        public bool ignoreEventSign;
       // public List<string> addBehaviourName;


        private RectTransform m_rectTransform;

        //添加MonoBehaviour 对象脚本
        protected override void Awake()
        {
            base.Awake();
           // addMonoBehaviourScript();
        }

        private void addMonoBehaviourScript()
        {
            //if (addBehaviourName == null || addBehaviourName.Count <= 0)
            //{
            //    return;
            //}
            //for (int i = 0; i < addBehaviourName.Count; i++)
            //{
            //    string scriptName = addBehaviourName[i];
            //    Type type = Type.GetType(scriptName);//只能是自定义类
            //                                         //  Component script2 = Activator.CreateInstance(type) as Component;
            //    if (type != null)
            //    {
            //        this.gameObject.AddComponent(type);
            //    }
            //    else
            //    {
            //        Debug.LogError("AddComponent type==null typeName=" + scriptName);
            //    }
            //}
        }

        public RectTransform rectTransform
        {
            get
            {
                if (m_rectTransform == null)
                    m_rectTransform = GetComponent<RectTransform>();
                return m_rectTransform ?? this.transform as RectTransform;
            }
        }

        public DynamicUIWidget DynamicUI
        {
            get { return GetComponent<DynamicUIWidget>(); }
        }

        public abstract WidgetType GetWidgetType();


        protected bool IsInitSetFarBase;
        protected Vector3 FarAwayBaseVector3;
        [HideInInspector]
        public bool IsBaeFarAway;
        public void BaseSetFarAway(bool isFar)
        {
            if (!IsInitSetFarBase)
            {
                FarAwayBaseVector3 = transform.localPosition;
                IsInitSetFarBase = true;
            }
            if (!isFar && !gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            transform.localPosition = isFar ? Vector3.one * -10000 : FarAwayBaseVector3;
            IsBaeFarAway = isFar;
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return !ignoreEventSign;
        }

        public abstract bool AddEventListener(UIEvent eventType, Action<PointerEventData> onEventHandler);
        public abstract bool RemoveEventListener(UIEvent eventType, Action<PointerEventData> onEventHandler);


        public virtual bool AddCustomEventListener(UICustomEvent eventType, Action<object> onEventHandler)
        {
            return false;
        }

        public virtual bool AddMoreCustomEventListener(UIEvent eventType, Action<List<object>> onEventHandler)
        {
            return false;
        }

        public virtual bool AddSpineCustomEventListener(UISpineEvent eventType, Action<object> onEventHandler)
        {
            return false;
        }

    }
}