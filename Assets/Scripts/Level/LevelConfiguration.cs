using System;
using UnityEngine.AddressableAssets;

namespace MadHeroes.Level
{
    [Serializable]
    public class LevelConfiguration
    {
        public int Level;
        public AssetReference Scene;
    }
}