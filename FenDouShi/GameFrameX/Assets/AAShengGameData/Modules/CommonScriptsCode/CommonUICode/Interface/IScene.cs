using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScene
{

    string getSceneName();

    //是否初始化 加载完成
    bool getIsInit();

    void onEnter();

    void onReset();

    void onLeave();


}
