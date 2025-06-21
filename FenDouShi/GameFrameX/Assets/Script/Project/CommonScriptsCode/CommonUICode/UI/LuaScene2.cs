using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if TOOL
using XLua;

[CSharpCallLua]
#endif
public interface LuaScene2
{

    string getSceneName();

    //是否初始化 加载完成
    bool getIsInit();

    void onEnter();

    void onReset();

    void onLeave();


}
