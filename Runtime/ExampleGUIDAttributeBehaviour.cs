using UnityEngine;
// Example MonoBehaviour class
public class ExampleGUIDAttributeBehaviour : MonoBehaviour
{

    [GUIDAttributeField("123e4567-e89b-12d3-a456-426614174000")]
    public string m_playerName;

    [GUIDAttributeField("789e1234-b89c-12d3-a456-426614174001")]
    public int m_playerScore;


    [GUIDAttributeMethod("123e4567-e89b-12d3-a456-426614174000")]
    public void ExampleMethod1()
    {
        // Some code
    }

    [GUIDAttributeMethod("789e1234-b89c-12d3-a456-426614174001")]
    private void ExampleMethod2()
    {
        // Some code
    }

    public void NormalMethod()
    {
        // Some code
    }
}