
using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace UIWidget
{
    public class PointerClickListener : MonoBehaviour, IPointerClickHandler
    {

        public Action<PointerEventData> onHandler;



        static public PointerClickListener Get(GameObject go)
        {
            PointerClickListener listener = go.GetComponent<PointerClickListener>();
            if (listener == null) listener = go.AddComponent<PointerClickListener>();
            return listener;
        }



        public void OnPointerClick(PointerEventData eventData)
        {

            if (onHandler != null) onHandler(eventData);

        }





    }
}