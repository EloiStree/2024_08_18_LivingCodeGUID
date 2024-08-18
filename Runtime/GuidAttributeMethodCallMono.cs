using System;
using UnityEngine;

public class GuidAttributeMethodCallMono : MonoBehaviour {

    [Tooltip("The GUID of the method to call, starting by method name \\n guid")]
    public DebugGUIDAttribute m_guidAttribute;
    public MonoBehaviour m_targetMonoBehaviour;
    public Action m_loadedAction;
    private void OnEnable()
    {
        if (m_targetMonoBehaviour == null)
        {
            m_loadedAction = null;
        }
        else
        {

        GuidAttributeMethodCallUtility.GetActionFrom(m_targetMonoBehaviour, m_guidAttribute.GetGuid(), m_loadedAction);
        }
    }
    public void Invoke() {

        if(m_loadedAction != null)
        {
            m_loadedAction.Invoke();
        }
    }
}
