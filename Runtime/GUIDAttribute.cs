using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
sealed class GuidAttributeMethod : Attribute
{
    readonly string m_guidValue;


    public GuidAttributeMethod(string guid)
    {
        this.m_guidValue = guid;
    }
    public void GetGuid(out string guid)
    {
        guid = m_guidValue;
    }
    public string GetGuid()
    {
        return m_guidValue;
    }
    public string GUID
    {
        get { return m_guidValue; }
    }
}

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
sealed class GuidAttributeField : Attribute
{
    readonly string m_guidValue;


    public GuidAttributeField(string guid)
    {
        this.m_guidValue = guid;
    }

    public void GetGuid(out string guid)
    {
        guid = m_guidValue;
    }
    public string GetGuid()
    {
        return m_guidValue;
    }
    public string GUID
    {
        get { return m_guidValue; }
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
sealed class GuidAttributeClass : Attribute
{
    readonly string m_guidValue;
    public GuidAttributeClass(string guid)
    {
        this.m_guidValue = guid;
    }

    public void GetGuid(out string guid)
    {
        guid = m_guidValue;
    }
    public string GetGuid()
    {
        return m_guidValue;
    }
    public string GUID
    {
        get { return m_guidValue; }
    }
}


[System.Serializable]
public class DebugGUIDAttribute
{
    [TextArea(2, 2)]
    public string m_guidAndName;


    public DebugGUIDAttribute(string guid, string name)
    {
        Set(guid, name);
    }
    public DebugGUIDAttribute()
    {
        Set("", "");
    }

    public void Set(string guid, string name)
    {
        m_guidAndName = $"{name}\n{guid}";
    }

    public string GetGuid()
    {
        int index = m_guidAndName.IndexOf('\n');
        if (index < -1)
            return m_guidAndName;
        return m_guidAndName.Substring(index + 1);
    }
    public string GetName()
    {
        int index = m_guidAndName.IndexOf('\n');
        if (index < -1)
            return "";
        return m_guidAndName.Substring(0, index);
    }
}
    