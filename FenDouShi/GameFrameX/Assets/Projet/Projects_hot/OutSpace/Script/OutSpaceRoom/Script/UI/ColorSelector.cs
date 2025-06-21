
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    public Color baseColor;
    public Color trimColor;

    public ColorPicker colorPick;
    public Image shipBaseColor;
    public Image shipTrimColor;
    public Toggle isMotionToggle;

    public CinemachineBrain cinemachineBrain;
    private Action<Color, bool> _colorSelectFun;

    private void Awake()
    {

        if (shipBaseColor == null)
        {
            shipBaseColor = this.transform.Find("Base/Option").GetComponent<Image>();
        }
        if (shipTrimColor == null)
        {
            shipTrimColor = this.transform.Find("Trim/Option").GetComponent<Image>();
        }
        if (isMotionToggle == null)
        {
            isMotionToggle = this.transform.Find("isMotion/Toggle").GetComponent<Toggle>();
        }
        if (colorPick == null)
        {
            Debug.LogError("did not set ColorPicker!can't set colors");
        }
        else
        {
            colorPick.OnColorChange += ChageColorCallBack;
        }
    }
    private void Start()
    {
        if (colorPick != null)
        {
            colorPick.OnColorChange += ChageColorCallBack;
        }

        isMotionToggle.onValueChanged.AddListener(IsMotionToggle);
    }
    private void IsMotionToggle(bool isOn)
    {
        if (cinemachineBrain)
            cinemachineBrain.enabled = isOn;
    }
    private void ChageColorCallBack(Color currColor, bool isBase)
    {
        if (isBase)
        {
            shipBaseColor.color = currColor;
            baseColor = currColor;
        }
        else
        {
            shipTrimColor.color = currColor;
            trimColor = currColor;
        }
        if (_colorSelectFun != null)
        {
            _colorSelectFun(currColor, isBase);
        }

    }
    public void AddColorChangeFun(Action<Color, bool> colorSelectFun)
    {
        _colorSelectFun = colorSelectFun;
    }
}
