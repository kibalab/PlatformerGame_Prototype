using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ComponentUtill
{
    public static T SetComponent<T>(GameObject self) where T : Component
    {
        var component = self.GetComponent<T>();
        if (!component)
        {
            component = self.AddComponent<T>();
        }

        return component;
    }

    public static T CopyComponent<T>(GameObject target, T origin) where T : Component
    {
        var copied = origin.AddComponent<T>();
        foreach (var field in typeof(T).GetFields())
        {
            field.SetValue(copied, field.GetValue(origin));
        }

        return copied;
    }
}
