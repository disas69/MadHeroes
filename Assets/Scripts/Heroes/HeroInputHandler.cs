using UnityEngine;
using Framework.Tools.Misc;
using UnityEngine.EventSystems;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Hero))]
    public class HeroInputHandler : ActivatableMonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Hero _hero;
        private Vector2 _touchPosition;

        [SerializeField] private LineRenderer _pointer;

        public bool IsTouching { get; private set; }
        public Vector3 Direction { get; private set; }
        public float Velocity { get; private set; }

        private void Awake()
        {
            _hero = GetComponent<Hero>();
            _pointer.gameObject.SetActive(false);
            _pointer.SetColors(Color.green, Color.green);
        }

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);
            _pointer.gameObject.SetActive(isActive);

            if (isActive)
            {
                UpdatePointer();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsActive)
            {
                IsTouching = true;
                Debug.Log($"Pointer down {_hero.GetType().Name}");

                _touchPosition = eventData.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsActive && IsTouching)
            {

                var delta = eventData.position - _touchPosition;
                Direction = delta.normalized;

                delta.x /= Screen.width;
                delta.y /= Screen.height;

                Velocity = delta.magnitude * 25f;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsActive)
            {
                IsTouching = false;
                Debug.Log($"Pointer up {_hero.GetType().Name}");
            }
        }

        private void Update()
        {
            if (IsActive)
            {
                UpdatePointer();
            }
        }

        private void UpdatePointer()
        {
            _pointer.SetPosition(0, _hero.transform.position);
            _pointer.SetPosition(1, _hero.transform.position + _hero.transform.forward * _hero.Velocity * 0.75f);
        }
    }
}
