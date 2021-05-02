using UnityEngine;

namespace Framework.Tools.Misc
{
    public class ActivatableMonoBehaviour : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public virtual void Activate(bool isActive)
        {
            IsActive = isActive;
        }
    }
}