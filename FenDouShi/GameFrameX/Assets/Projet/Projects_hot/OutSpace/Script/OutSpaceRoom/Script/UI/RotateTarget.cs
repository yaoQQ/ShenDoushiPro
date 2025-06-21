using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//UI to Rotate Ship in Room
public class RotateTarget : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Transform _rotateTarget=null;
    public float rotateSpeed = 0.5f;
    private bool isRotating = false;
    private Vector3 lastPosition;

    public Transform rotateTarget
    {
        set
        {
            _rotateTarget = value;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isRotating = true;
        lastPosition = eventData.position;
        Debug.Log("RotateTarget OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_rotateTarget && isRotating)
        {
            float rotateX = (eventData.position.x - lastPosition.x) * rotateSpeed;
            // float rotateY = (eventData.position.y - lastPosition.y) * rotateSpeed;
            _rotateTarget.Rotate(Vector3.up, -rotateX, Space.World);
            //_rotateTarget.Rotate(Vector3.right, rotateY, Space.World);
            lastPosition = eventData.position;
        }
    }

}
