using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class UIBinderAttribute : Attribute
{
    public string PackageName;

    public UIBinderAttribute(string packageName = "")
    {
        PackageName = packageName;
    }
}