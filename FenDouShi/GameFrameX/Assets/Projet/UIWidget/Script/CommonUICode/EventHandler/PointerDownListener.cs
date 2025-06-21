using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace UIWidget
{
    public class PointerDownListener : MonoBehaviour, IPointerDownHandler
    {

        public Action<PointerEventData> onHandler;

        static public PointerDownListener Get(GameObject go)
        {
            PointerDownListener listener = go.GetComponent<PointerDownListener>();
            if (listener == null) listener = go.AddComponent<PointerDownListener>();
            return listener;
        }



        public void OnPointerDown(PointerEventData eventData)
        {

            if (onHandler != null) onHandler(eventData);
        }

    }
}