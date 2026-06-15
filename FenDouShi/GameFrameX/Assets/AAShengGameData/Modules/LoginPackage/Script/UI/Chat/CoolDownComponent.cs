using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager : SingleMonobehaviour<CoolDownManager>
{
    public List<CoolDownComponent> coms = new();

    void Update()
    {
        for (int i = 0; i < coms.Count; i++)
        {
            coms[i].Update();
        }
    }

    public void AddCom(CoolDownComponent com)
    {
        coms.Add(com);
    }

    public void Remove(CoolDownComponent coolDownComponent)
    {
        coms.Remove(coolDownComponent);
    }
}

public class CoolDownComponent
{
    GImage imgMask;
    GTextField secondText;

    public uint startSeconds;
    public float nowSeconds;
    public uint finishSeconds;
    public Action onStart;
    public Action onUpdate;
    public Action onComplete;

    public void Init(GImage mask, GTextField second)
    {
        startSeconds = finishSeconds = 0;
        nowSeconds = 0;

        imgMask = mask;
        imgMask.visible = false;
        secondText = second;
        secondText.visible = false;

        CoolDownManager.Instance.AddCom(this);
    }

    public void Update()
    {
        if (nowSeconds > 0)
        {
            nowSeconds -= Time.deltaTime;
            onUpdate?.Invoke();

            imgMask.fillAmount = Mathf.Clamp01(nowSeconds / (finishSeconds - startSeconds));
            secondText.text = string.Format(ChatString.coolDownTextFormat, Mathf.FloorToInt(nowSeconds));

            if (nowSeconds <= 0)
            {
                Stop();
            }
        }
    }

    // [start,target]
    public void StartCoolDown(uint startTimeStamp, uint targetTimeStamp)
    {
        uint nowTs = TimeManager.GetlocalTimeStamp();
        if (startTimeStamp == targetTimeStamp && startTimeStamp == 0 || nowTs >= targetTimeStamp)
        {
            Stop();
            return;
        }

        startSeconds = startTimeStamp;
        targetTimeStamp += 1;
        nowSeconds = Mathf.Max(0, targetTimeStamp - nowTs);
        finishSeconds = targetTimeStamp;

        imgMask.visible = true;
        imgMask.fillAmount = 1;
        secondText.visible = true;
        secondText.text = string.Format(ChatString.coolDownTextFormat, Mathf.FloorToInt(nowSeconds));

        onStart?.Invoke();
    }

    public void Stop()
    {
        startSeconds = finishSeconds = 0;
        nowSeconds = 0;

        imgMask.visible = false;
        secondText.visible = false;
        onComplete?.Invoke();
    }

    public void Destroy()
    {
        onUpdate = null;
        onComplete = null;
        CoolDownManager.Instance.Remove(this);
    }
}