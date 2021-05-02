using System;
using Framework.Spawn;
using Framework.Tools.Misc;
using UnityEngine;

namespace MadHeroes.Level
{
    public class LevelController : ActivatableMonoBehaviour, IDisposable
    {
        private LevelConfiguration _configuration;

        public Transform Start;
        public Transform Finish;
        public Transform Elements;

        public int Level { get; private set; }

        public virtual void Initialize(int level, LevelConfiguration configuration)
        {
            _configuration = configuration;
            Level = level;
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }
        }

        public virtual void Dispose()
        {
            Activate(false);

            var spawnables = Elements.GetComponentsInChildren<SpawnableObject>(true);

            for (var i = 0; i < spawnables.Length; i++)
            {
                spawnables[i].Deactivate();
            }
        }
    }
}