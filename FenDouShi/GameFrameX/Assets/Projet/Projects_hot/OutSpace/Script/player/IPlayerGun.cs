using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IPlayerGun
{
    //按钮事件 按下
    void BtnPressFun();

    //按钮事件 弹起
    void BtnUptFun();


    //枪对应按钮
    PlayerGunBtnsEnum GetGunBtnPos { get; }
}

