using System;

[AttributeUsage(AttributeTargets.Field)]
public class UIInjectAttribute : Attribute
{
    public string ObjectName { get; }

    public UIInjectAttribute(string objectName)
    {
        ObjectName = objectName;
    }
}