using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.UI.Play
{
    [RequireComponent(typeof(Toggle))]
    public class ActionToggleView : MonoBehaviour
    {
        private Toggle _toggle;
        private Action _action;
        private bool _isOn;

        [SerializeField] private TextMeshProUGUI _text;

        public Toggle Toggle => _toggle;

        public event System.Action<Toggle, Action> ToggleOn;

        public void Initialize(Action action)
        {
            _action = action;
            _text.text = action.ToString();
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnToggle(bool isOn)
        {
            if (isOn)
            {
                ToggleOn?.Invoke(_toggle, _action);
            }
            else
            {
                if (_isOn)
                {
                    _toggle.isOn = true;
                }
            }

            _isOn = _toggle.isOn;
        }
    }
}