using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class GuidAttributeMethodInProject: MonoBehaviour
{
    public DebugGUIDAttribute m_guid;

    [TextArea(1,10)]
    public string m_information;
    [ContextMenu("Look for GUID")]
    public void LookForGUID()
    {

        GetInfoOnTheGuidMethodGiven(m_guid.GetGuid(), out m_information);
    }

    public void GetInfoOnTheGuidMethodGiven(string targetGuid, out string information)
    {
        information = "";
        if (string.IsNullOrEmpty(targetGuid))
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        bool methodFound = false;

        // Get all assemblies loaded in the current AppDomain
        IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Iterate over all assemblies
        foreach (Assembly assembly in assemblies)
        {
            try
            {
                // Iterate over all types in the assembly
                foreach (Type type in assembly.GetTypes())
                {
                    // Iterate over all methods of the type
                    foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        // Check if the method has the GUIDAttributeMethod attribute
                        foreach (var attr in method.GetCustomAttributes(typeof(GuidAttributeMethod), false))
                        {
                            GuidAttributeMethod guidAttr = (GuidAttributeMethod)attr;
                            if (guidAttr.GUID == targetGuid)
                            {
                                // Display information about the method
                                sb.AppendLine($"Method with GUID '{targetGuid}' found in assembly '{assembly.FullName}', class '{type.FullName}':");
                                sb.AppendLine($"Name: {method.Name}");
                                sb.AppendLine($"Return Type: {method.ReturnType}");
                                sb.AppendLine("Parameters:");
                                foreach (var param in method.GetParameters())
                                {
                                    sb.AppendLine($" - {param.ParameterType} {param.Name}");
                                }
                                sb.AppendLine();
                                methodFound = true;
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Handle the case where some types in the assembly cannot be loaded
                sb.AppendLine($"Failed to load types from assembly '{assembly.FullName}': {ex.Message}");
            }
        }

        if (methodFound)
        {
            information = sb.ToString();
        }
        else
        {
            information = $"Method with GUID '{targetGuid}' not found.";
        }
    }
}