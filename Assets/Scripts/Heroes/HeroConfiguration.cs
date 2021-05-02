using System;
using UnityEngine.AddressableAssets;

namespace MadHeroes.Heroes
{
    [Serializable]
    public class HeroConfiguration
    {
        public string Name;
        public AssetReference Prefab;
        public AssetReference Icon;
        public float Health;
    }
}