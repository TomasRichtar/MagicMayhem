using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TastyCore.Patterns.Factory
{
    public abstract class FactoryWithObjectPooling<T>: ScriptableObject where T : MonoBehaviour
    {
        private Scene _scene;
        private Dictionary<string, List<T>> _pools;

        protected T Create(string poolInstanceId)
        {
            T instance = null;
            
            if (_pools == null)
                _pools = new Dictionary<string, List<T>>();

            if (!_pools.ContainsKey(poolInstanceId))
                _pools.Add(poolInstanceId, new List<T>());

            var pool = _pools[poolInstanceId];
            var lastIndex = pool.Count - 1;

            if (lastIndex >= 0)
            {
                instance = pool[lastIndex];
                instance.gameObject.SetActive(true);
                pool.RemoveAt(lastIndex);
            }

            return instance != null
                ? instance
                : CreateNewInstance(GetPrefab(poolInstanceId));
        }
        
        private T CreateNewInstance(T prefab)
        {
            if (!_scene.isLoaded)
            {
                if (Application.isEditor)
                {
                    _scene = SceneManager.GetSceneByName(name);
                    if (!_scene.isLoaded)
                    {
                        _scene = SceneManager.CreateScene(name);
                    }
                }
                else
                {
                    _scene = SceneManager.CreateScene(name);
                }
            }

            T instance = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
            
            return instance;
        }
        
        public virtual void Reclaim(T instance, float delay = 0)
        {
            if (_pools == null)
                _pools = new Dictionary<string, List<T>>();

            var poolInstanceId = GetInstanceId(instance);
            
            if(!_pools.ContainsKey(poolInstanceId))
                 _pools.Add(poolInstanceId,new List<T>());
                
            _pools[poolInstanceId].Add(instance);
            instance.gameObject.SetActive(false);
        }

        protected abstract string GetInstanceId(T instance);
        protected abstract T GetPrefab(string instanceId);

        #region Object Pooling

        /*
        Why use lists instead of stacks?

        Because lists survive recompilation in play mode, while stacks don't. 
        Unity doesn't serialize stacks. 
        You could use stacks instead, but lists work just fine.
        */

        // TODO
        // Maximum number of occurrences in pool

        #endregion
    }
}