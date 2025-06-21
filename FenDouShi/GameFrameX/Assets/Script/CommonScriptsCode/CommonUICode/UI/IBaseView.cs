using System.Collections.Generic;
using UnityEngine;

public interface IBaseView
{
    List<string> getLoadOrders();
    UIViewEnum getViewEnum();
    bool getIsLoaded();

    void executeLoadUIEnd(string uiName, GameObject obj);
}