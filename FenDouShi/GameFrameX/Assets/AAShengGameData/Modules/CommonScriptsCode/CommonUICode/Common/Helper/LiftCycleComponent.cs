using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class LiftCycleComponent : FGUIComponent
{
    private BaseRender mBaseRender;

    public void setBaseRender(BaseRender baseRender)
    {
        mBaseRender = baseRender;
    }

    void OnDestroy()
    {
        if (mBaseRender != null)
        {
            mBaseRender.Dispose();
        }
    }

    void OnEnable()
    {
        if (mBaseRender != null)
        {
            mBaseRender.OnEnable();
        }
    }

    void OnDisable()
    {
        if (mBaseRender != null)
        {
            mBaseRender.OnDisable();
        }
    }

    void Update()
    {
        if (mBaseRender != null)
        {
            mBaseRender.Update();
        }
    }
}
