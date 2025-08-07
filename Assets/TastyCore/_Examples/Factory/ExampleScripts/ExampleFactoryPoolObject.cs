using System;
using UnityEngine;

[Serializable]
public enum ExampleFactoryObjectType
{
    Cube,
    Sphere 
}

public class ExampleFactoryPoolObject : MonoBehaviour
{
    [SerializeField]
    private ExampleFactoryObjectType _objectType;

    public ExampleFactoryObjectType Type => _objectType;
}
