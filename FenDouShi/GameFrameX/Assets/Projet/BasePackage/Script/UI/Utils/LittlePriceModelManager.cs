using UnityEngine;
using System.Collections.Generic;
using System;

public class LittlePriceModelManager : Singleton<LittlePriceModelManager>
{
    private Dictionary<string,GameObject> modelDic = new Dictionary<string, GameObject>();

    public void createModel(string packName, string modelName, Action<GameObject> onLoadend)
    {
        Logger.PrintColor("blue", "LittlePriceModelManager.Instance.createModel()");
        if (modelDic.ContainsKey(modelName))
        {
            if (onLoadend != null)
            {
                onLoadend(modelDic[modelName]);
            }
            return;
        }

        ModelManager.Instance.CreateModel(packName, modelName, (obj) =>
        {
            if (obj == null)
            {
                Logger.PrintError("加载错误 packName"+ packName+ " modelName="+ modelName + " obj="+ obj);
                return;
            }
            if (onLoadend != null)
            {
                Logger.PrintColor("blue", "ModelManager.Instance:CreateModel() 加载返回 obj=" + obj);
                modelDic[modelName] = obj;
                onLoadend(obj);
            }
        });


    }
    public void Clear()
    {
        modelDic.Clear();
    }
   
}
