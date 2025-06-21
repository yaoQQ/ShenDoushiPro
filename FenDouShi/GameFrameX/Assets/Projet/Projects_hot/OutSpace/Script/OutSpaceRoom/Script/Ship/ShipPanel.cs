using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.TMP_Dropdown;
using System;

public class ShipPanel : MonoBehaviour
{
    public TMP_Dropdown shipTypeOption;
    public TMP_Dropdown shipMatTypeOption;
    public ColorSelector colorSelect;
    // Start is called before the first frame update

    private Action<ShipType> _changeShipFun;
    private Action<ShipSkinType> _changeShipSkinFun;

    void Start()
    {
        shipTypeOption.onValueChanged.AddListener(ShipTypeOptionFun);
        shipMatTypeOption.onValueChanged.AddListener(ShipMatTypeOptionFun);
        ShipTypeOptionFun(0);
    }
    public void AddChangeShipFun(Action<ShipType> changeShipCallBackFun)
    {
        _changeShipFun = changeShipCallBackFun;
    }
    public void AddChangeSkinFun(Action<ShipSkinType> changeSkinCallBackFun)
    {
        _changeShipSkinFun = changeSkinCallBackFun;
    }
    private void ShipTypeOptionFun(int index)
    {
        ShipType shipType = (ShipType)index;
        Debug.Log("MatTypeOptionFun ShipType=" + shipType);
        if (_changeShipFun != null)
        {
            _changeShipFun(shipType);
        }
    }
    private void ShipMatTypeOptionFun(int index)
    {
        ShipSkinType ShipSkinType = (ShipSkinType)index;
        Debug.Log("MatTypeOptionFun ShipSkinType=" + ShipSkinType);
        if (_changeShipSkinFun != null)
        {
            _changeShipSkinFun(ShipSkinType);
        }
    }
    //private void AnimalEyesChange(int index)
    //{


    //    rest();
    //    object obj = main_mid.AnimnalEyes.GetOptionCurrData();
    //    int ViewIndex = int.Parse(obj.ToString());
    //    AnamalEnum viewEnum = (AnamalEnum)ViewIndex;
    //    Logger.PrintDebug("AnimalEyesChange() viewEnum=" + viewEnum);
    //    switch (viewEnum)
    //    {
    //        case AnamalEnum.Dog:
    //            showYellowLeaf();
    //            break;
    //        case AnamalEnum.Cat:

    //            CatEye();
    //            break;
    //        case AnamalEnum.Shark:

    //            ShakeEye();
    //            break;
    //        case AnamalEnum.Fish:

    //            FishEye();
    //            break;
    //        case AnamalEnum.Tortoise:

    //            TortoiseEye();
    //            break;
    //        case AnamalEnum.Snake:

    //            SnakeEye();
    //            break;

    //        case AnamalEnum.Hourse:
    //            HourseEye();
    //            break;
    //        case AnamalEnum.Cow:
    //            CowEye();
    //            break;
    //    }

    //}
    //public object GetOptionCurrData()
    //{
    //    string key = Drop.options[Drop.value].text;
    //    if (optionData.ContainsKey(key))
    //    {
    //        return optionData[key];
    //    }
    //    else
    //    {
    //        Debug.Log("没有获取到Value=" + key);
    //        return null;
    //    }
    //}
}
