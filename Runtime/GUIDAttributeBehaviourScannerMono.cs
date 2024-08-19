using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;



public class GUIDAttributeBehaviourScannerMono : MonoBehaviour
{
 
    public MonoBehaviour m_targetMonoBehaviour;
    public List<DebugGUIDAttribute> m_methodeInScript;
    public List<DebugGUIDAttribute> m_fieldInScript;


    private void Reset()
    {
        if(gameObject)
        m_targetMonoBehaviour = gameObject.GetComponent<MonoBehaviour>();
        
    }

    [ContextMenu("Refresh Attribute List")]
    public void Refresh()
    {
        if (m_targetMonoBehaviour == null)
        {
            return;
        }

        Type type = m_targetMonoBehaviour.GetType();
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        m_methodeInScript.Clear();
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(GuidAttributeMethod), true);
            foreach (var attribute in attributes)
            {
                GuidAttributeMethod guidAttribute = (GuidAttributeMethod)attribute;
                m_methodeInScript.Add(
                    new DebugGUIDAttribute(guidAttribute.GUID, method.Name));
            }
        }
        m_fieldInScript.Clear();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attributes = field.GetCustomAttributes(typeof(GuidAttributeField), true);
            foreach (var attribute in attributes)
            {
                GuidAttributeField guidAttribute = (GuidAttributeField)attribute;
                m_fieldInScript.Add(
                    new DebugGUIDAttribute(guidAttribute.GUID, field.Name));
            }
        }
    }
}



public class GUIDAttributeStaticScanner : MonoBehaviour
{

    public MonoBehaviour m_targetMonoBehaviour;
    public List<DebugGUIDAttribute> m_methodeInScript;
    public List<DebugGUIDAttribute> m_fieldInScript;


    [ContextMenu("Refresh Attribute List")]
    public void Refresh()
    {
        if (m_targetMonoBehaviour == null)
        {
            Debug.LogError("Target MonoBehaviour is not assigned!");
            return;
        }

        Type type = m_targetMonoBehaviour.GetType();
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        m_methodeInScript.Clear();
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(GuidAttributeMethod), true);
            foreach (var attribute in attributes)
            {
                GuidAttributeMethod guidAttribute = (GuidAttributeMethod)attribute;
                m_methodeInScript.Add(
                    new DebugGUIDAttribute(guidAttribute.GUID, method.Name));
            }
        }
        m_fieldInScript.Clear();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attributes = field.GetCustomAttributes(typeof(GuidAttributeField), true);
            foreach (var attribute in attributes)
            {
                GuidAttributeField guidAttribute = (GuidAttributeField)attribute;
                m_fieldInScript.Add(
                    new DebugGUIDAttribute(guidAttribute.GUID, field.Name));
            }
        }
    }
}


public class GuidAttributeMethodCallUtility {

    public static void GetActionFrom(MonoBehaviour monoBehaviour, string guid, Action toInvoke)
    {
        Type targetType = monoBehaviour.GetType();

        var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(GuidAttributeMethod), true);
            var attribute = attributes.FirstOrDefault() as GuidAttributeMethod;

            if (attribute != null && attribute.GUID == guid)
            {

                toInvoke = (Action)Delegate.CreateDelegate(typeof(Action), monoBehaviour, method);

            }
        }

    }

    public static void GetActionFrom<T>(MonoBehaviour monoBehaviour, string guid, out Action<T> toInvoke)
    {
        Type targetType = monoBehaviour.GetType();
        toInvoke = null;
        var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(GuidAttributeMethod), true);
            var attribute = attributes.FirstOrDefault() as GuidAttributeMethod;

            if (attribute != null && attribute.GUID == guid)
            {
                if (method.ReturnType == typeof(void) && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(T))
                {
                    toInvoke = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), monoBehaviour, method);
                }
            }
        }
    }

    public static void GetActionFrom<TA, TB>(MonoBehaviour monoBehaviour, string guid, out Action<TA, TB> toInvoke)
    {
        Type targetType = monoBehaviour.GetType();
        toInvoke = null;
        var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(GuidAttributeMethod), true);
            var attribute = attributes.FirstOrDefault() as GuidAttributeMethod;

            if (attribute != null && attribute.GUID == guid)
            {
                if (method.ReturnType == typeof(void) &&
                    method.GetParameters().Length == 2 &&
                    method.GetParameters()[0].ParameterType == typeof(TA) &&
                    method.GetParameters()[1].ParameterType == typeof(TB))
                    toInvoke = (Action<TA, TB>)Delegate.CreateDelegate(typeof(Action<TA, TB>), monoBehaviour, method);
            }
        }
    }


}