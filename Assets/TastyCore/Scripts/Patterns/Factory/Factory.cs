using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TastyCore.Patterns.Factory
{
    public abstract class Factory<T> : ScriptableObject where T : MonoBehaviour
    {
        private Scene _scene;
        
        protected T Create(T prefab)
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
        
        public virtual void Reclaim(T instance, float delay = 0f)
        {
            Destroy(instance.gameObject ,delay);
        }
    }
}