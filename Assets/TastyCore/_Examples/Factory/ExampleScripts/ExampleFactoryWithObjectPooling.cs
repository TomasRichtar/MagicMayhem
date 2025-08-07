using System.Collections.Generic;
using System.Linq;
using TastyCore.Extensions;
using TastyCore.Patterns.Factory;
using UnityEngine;


[CreateAssetMenu(menuName = "Examples/Factory/WithObjectPooling", fileName = "FactoryWithObjectPooling")]
public class ExampleFactoryWithObjectPooling : FactoryWithObjectPooling<ExampleFactoryPoolObject>
{
    [SerializeField] private List<ExampleFactoryPoolObject> _examplePrefabs;

    public ExampleFactoryPoolObject GetExampleObject(ExampleFactoryObjectType type)
    {
        return Create(type.ToString());
    }
    
    protected override string GetInstanceId(ExampleFactoryPoolObject instance)
    {
        // Here you will generate ID of instance it could be name
        // Enum string
        // Index in list
        // Etc..
        
        // For example purposes i used Enum string
        return instance.Type.ToString();
    }

    protected override ExampleFactoryPoolObject GetPrefab(string instanceId)
    {
        return _examplePrefabs.
            FirstOrDefault(x => x.Type == instanceId.ToEnumOrDefault(ExampleFactoryObjectType.Cube));
    }
    
    // We can also implement this
    /*
    public void Reclaim(T instance)
    {
        //.Assert(instance.OriginFactory == this, "Wrong factory reclaimed!");
        //Destroy(instance.gameObject, delay);
    }
    */
}











