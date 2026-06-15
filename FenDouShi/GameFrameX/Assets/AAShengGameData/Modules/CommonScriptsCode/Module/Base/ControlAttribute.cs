using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ControlAttribute : Attribute
{
    public ControlAttribute()
    {
    }
}