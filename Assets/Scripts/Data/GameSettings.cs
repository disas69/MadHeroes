using System;
using Framework.Utils.Math;

namespace MadHeroes.Data
{
    [Serializable]
    public class GameSettings
    {
        public float DefaultMoveVelocity;
        public MinMaxFloatValue MoveVelocity;
    }
}