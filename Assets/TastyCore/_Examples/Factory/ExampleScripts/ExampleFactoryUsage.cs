using System.Collections.Generic;
using System.Linq;
using TastyCore.Extensions;
using TMPro;
using UnityEngine;

public class ExampleFactoryUsage : MonoBehaviour
{
    // Just example code for testing purposes
    
    private enum FactoryType
    {
        Normal,
        WithPooling
    }
    
    [Header("Factories")]
    [SerializeField] private ExampleFactory _normalFactory;
    [SerializeField] private ExampleFactoryWithObjectPooling _poolingFactory;
    
    [Header("Factory type")]
    [SerializeField] private FactoryType _type = FactoryType.Normal;
    [SerializeField] private KeyCode _changeType;
    [SerializeField] private TextMeshProUGUI _debugText;
    
    [Header("Spawning control")]
    [SerializeField] private KeyCode _spawnCubeCode;
    [SerializeField] private KeyCode _despawnCubeCode;
    
    [SerializeField] private KeyCode _spawnCubeSphere;
    [SerializeField] private KeyCode _despawnCubeSphere;
    
    private List<ExampleFactoryPoolObject> _normalCubes = new List<ExampleFactoryPoolObject>();
    private List<ExampleFactoryPoolObject> _normalSpheres = new List<ExampleFactoryPoolObject>();

    private List<ExampleFactoryPoolObject> _pooledCubes = new List<ExampleFactoryPoolObject>();
    private List<ExampleFactoryPoolObject> _pooledSpheres = new List<ExampleFactoryPoolObject>();

    private void Start()
    {
        _debugText.text = _type.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(_changeType))
        {
            _type = _type.Next();
            _debugText.text = _type.ToString();
        }

        switch (_type)
        {
            case FactoryType.Normal:
                NormalUpdate();
                break;
            
            case FactoryType.WithPooling:
                PoolingUpdate();
                break;
            default: break;
        }
        
        void NormalUpdate()
        {
            if (Input.GetKeyDown(_spawnCubeCode))
            {
                var cube = _normalFactory.GetExampleObject(ExampleFactoryObjectType.Cube);
                cube.name += "cube_normal";
                _normalCubes.Add(cube);
            }
        
            if (Input.GetKeyDown(_despawnCubeCode))
            {
                if (_normalCubes.Count == 0) return;
                var lastObject = _normalCubes.Last();
                _normalCubes.Remove(lastObject);
            
                _normalFactory.Reclaim(lastObject);
            }
        
            if (Input.GetKeyDown(_spawnCubeSphere))
            {
                var sphere = _normalFactory.GetExampleObject(ExampleFactoryObjectType.Sphere);
                sphere.name += "sphere_normal";
                _normalSpheres.Add(sphere);
            }
        
            if (Input.GetKeyDown(_despawnCubeSphere))
            {
                if (_normalSpheres.Count == 0) return;
                var lastObject = _normalSpheres.Last();
                _normalSpheres.Remove(lastObject);
            
                _normalFactory.Reclaim(lastObject);
            }
        }
        void PoolingUpdate()
        {
            if (Input.GetKeyDown(_spawnCubeCode))
            {
                var cube = _poolingFactory.GetExampleObject(ExampleFactoryObjectType.Cube);
                cube.name = "cube_pooled";
                _pooledCubes.Add(cube);
            }
        
            if (Input.GetKeyDown(_despawnCubeCode))
            {
                if (_pooledCubes.Count == 0) return;
                var lastObject = _pooledCubes.Last();
                _pooledCubes.Remove(lastObject);
            
                _poolingFactory.Reclaim(lastObject);
            }
        
            if (Input.GetKeyDown(_spawnCubeSphere))
            {
                var sphere = _poolingFactory.GetExampleObject(ExampleFactoryObjectType.Sphere);
                sphere.name += "sphere_pooled";
                _pooledSpheres.Add(sphere);
            }
        
            if (Input.GetKeyDown(_despawnCubeSphere))
            {
                if (_pooledSpheres.Count == 0) return;
                var lastObject = _pooledSpheres.Last();
                _pooledSpheres.Remove(lastObject);
            
                _poolingFactory.Reclaim(lastObject);
            }
        }
    }
}
