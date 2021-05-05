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
        public float Damage;
        public float AttackRadius;
        public float SpecialRadius;
    }
}