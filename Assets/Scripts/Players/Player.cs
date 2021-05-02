using System;
using Cinemachine;
using UnityEngine;
using MadHeroes.Heroes;
using System.Collections.Generic;
using MadHeroes.Configuration;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace MadHeroes.Players
{
    [Serializable]
    public class HeroEntry
    {
        public string Hero;
        public Vector3 Position;
        public Vector3 Rotation;
    }

    public class Player : MonoBehaviour, IDisposable
    {
        private List<Hero> _heroes;
        private List<AsyncOperationHandle<GameObject>> _handles;

        [SerializeField] private List<HeroEntry> _heroEntries;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public List<Hero> Heroes => _heroes;
        public bool IsDefeated => _heroes.Count == 0;
        public CinemachineVirtualCamera Camera => _camera;

        private void Awake()
        {
            _camera.Priority = 0;
            _camera.enabled = false;
        }

        public void Initialize()
        {
            _heroes = new List<Hero>(_heroEntries.Count);
            _handles = new List<AsyncOperationHandle<GameObject>>(_heroEntries.Count);

            for (var i = 0; i < _heroEntries.Count; i++)
            {
                LoadHero(_heroEntries[i], hero =>
                {
                    _heroes.Add(hero);
                });
            }
        }

        public void RemoveDeadHeroes()
        {
            for (var i = _heroes.Count - 1; i >= 0; i--)
            {
                var hero = _heroes[i];
                if (hero.Health <= 0f)
                {
                    _heroes.Remove(hero);
                    hero.Destroy();
                }
            }
        }

        private void LoadHero(HeroEntry entry, Action<Hero> callback)
        {
            var configuration = GameConfiguration.GetHeroConfiguration(entry.Hero);
            if (configuration != null)
            {
                var asyncOperation = Addressables.InstantiateAsync(configuration.Prefab.RuntimeKey, new InstantiationParameters(transform.position + entry.Position, Quaternion.Euler(entry.Rotation), transform));

                void OnHeroLoaded(AsyncOperationHandle<GameObject> handle)
                {
                    _handles.Add(handle);
                    handle.Completed -= OnHeroLoaded;

                    var hero = handle.Result.GetComponent<Hero>();
                    if (hero != null)
                    {
                        hero.Initialize(configuration);
                        callback?.Invoke(hero);
                    }
                }

                asyncOperation.Completed += OnHeroLoaded;
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _heroes.Count; i++)
            {
                Destroy(_heroes[i].gameObject);
            }

            for (var i = 0; i < _handles.Count; i++)
            {
                Addressables.Release(_handles[i]);
            }
        }

        private void OnDrawGizmos()
        {
            for (var i = 0; i < _heroEntries.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position + _heroEntries[i].Position, 0.5f);
            }
        }
    }
}
