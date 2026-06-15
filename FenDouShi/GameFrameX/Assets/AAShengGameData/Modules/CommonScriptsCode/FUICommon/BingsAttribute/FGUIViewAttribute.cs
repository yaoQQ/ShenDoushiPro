using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FGUIViewAttribute : Attribute
{

    public UIViewEnum uiViewEnum = UIViewEnum.None;
    public Type viewType;
    public string viewName;

    public FGUIViewAttribute(UIViewEnum viewId, Type viewType)
    {
        this.uiViewEnum = viewId;
        this.viewType = viewType;
    }
    
    public FGUIViewAttribute(string viewName, Type viewType)
    {
        this.viewName = viewName;
        this.viewType = viewType;
    }
}