using System.Collections.Generic;
using System.Linq;
using TastyCore.Patterns.Factory;
using UnityEngine;


[CreateAssetMenu(menuName = "Examples/Factory/Normal", fileName = "Factory")]
public class ExampleFactory : Factory<ExampleFactoryPoolObject>
{
    [SerializeField] private List<ExampleFactoryPoolObject> _examplePrefabs;
    
    public ExampleFactoryPoolObject GetExampleObject(ExampleFactoryObjectType type)
    {
        var  prefab = _examplePrefabs.
            FirstOrDefault(x => x.Type == type);
        
        // Potential null exception << Just testing 
        return Create(prefab);
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
