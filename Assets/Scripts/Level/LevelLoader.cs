using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace MadHeroes.Level
{
    public class LevelLoader
    {
        private static SceneInstance _scene;

        public static bool IsLoaded { get; private set; }

        public static void Load(LevelConfiguration configuration, Action<LevelController> callback)
        {
            if (configuration.Scene != null)
            {
                void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
                {
                    _scene = handle.Result;
                    
                    IsLoaded = true;
                    handle.Completed -= OnSceneLoaded;

                    SceneManager.SetActiveScene(_scene.Scene);

                    var roots = _scene.Scene.GetRootGameObjects();
                    for (var i = 0; i < roots.Length; i++)
                    {
                        var level = roots[i].GetComponent<LevelController>();
                        if (level != null)
                        {
                            callback?.Invoke(level);
                            break;
                        }
                    }
                }

                var asyncOperation = Addressables.LoadSceneAsync(configuration.Scene.RuntimeKey, LoadSceneMode.Additive);
                asyncOperation.Completed += OnSceneLoaded;
            }
            else
            {
                Debug.LogError($"Scene for level {configuration.Level} is not assigned!");
            }
        }

        public static void Unload(Action callback)
        {
            if (IsLoaded)
            {
                void OnSceneUnloaded(AsyncOperationHandle<SceneInstance> handle)
                {
                    IsLoaded = false;
                    handle.Completed -= OnSceneUnloaded;
                    callback?.Invoke();
                }

                var asyncOperation = Addressables.UnloadSceneAsync(_scene);
                asyncOperation.Completed += OnSceneUnloaded;
            }
        }
    }
}