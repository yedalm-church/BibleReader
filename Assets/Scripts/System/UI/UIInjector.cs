using System;
using System.Reflection;
using UnityEngine;

public static class UIInjector
{
    public static void Inject(MonoBehaviour InTarget)
    {
        var targetType = InTarget.GetType();

        FieldInfo[] fields =
            targetType.GetFields(
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public);

        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<UIInjectAttribute>();

            if (attribute == null)
                continue;

            var child = FindChildRecursive(InTarget.transform, attribute.ObjectName);

            if (child == null)
            {
                Debug.LogError($"UIInject 실패 : {attribute.ObjectName}");
                continue;
            }

            object value = null;

            if (field.FieldType == typeof(GameObject))
            {
                value = child.gameObject;
            }
            else if (field.FieldType == typeof(Transform))
            {
                value = child;
            }
            else if (typeof(Component).IsAssignableFrom(field.FieldType))
            {
                value =
                    child.GetComponent(field.FieldType);
            }

            if (value == null)
            {
                Debug.LogError($"UIInject 실패 : {attribute.ObjectName}에서 {field.FieldType.Name}을 찾을 수 없음");
                continue;
            }

            field.SetValue(InTarget, value);
        }
    }

    private static Transform FindChildRecursive(Transform InParent, string InObjectName)
    {
        foreach (Transform child in InParent)
        {
            if (child.name == InObjectName)
                return child;

            var result = FindChildRecursive(child, InObjectName);
            if (result != null)
                return result;
        }

        return null;
    }
}