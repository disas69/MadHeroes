using DG.Tweening;
using Framework.Extensions;
using Framework.Input;
using Framework.UI.Structure.Base.Model;
using Framework.UI.Structure.Base.View;
using UnityEngine;

namespace Source.UI.Pages
{
    public enum TutorialType
    {
        None,
        Touch,
        Drag,
        Release
    }

    public class PlayPage : Page<PageModel>
    {
        private bool _isTutorialFinished;
        private InputController _inputController;

        [SerializeField] private CanvasGroup _tutorial;
        [SerializeField] private TutorialType _tutorialType;
        [SerializeField] private float _tutorialFadeTime;

        public override void OnEnter()
        {
            base.OnEnter();

            if (_inputController == null)
            {
                _inputController = FindObjectOfType<InputController>();
            }

            if (_tutorialType != TutorialType.None)
            {
                _tutorial.alpha = 0f;

                if (!_isTutorialFinished)
                {
                    this.WaitForSeconds(0.5f, () =>
                    {
                        if (_tutorialType == TutorialType.Touch)
                        {
                            _inputController.TouchDown += OnTutorialFinished;
                        }
                        else if (_tutorialType == TutorialType.Drag)
                        {
                            _inputController.Drag += OnTutorialFinished;
                        }
                        else if (_tutorialType == TutorialType.Release)
                        {
                            _inputController.TouchUp += OnTutorialFinished;
                        }

                        _tutorial.DOFade(1f, _tutorialFadeTime);
                    });
                }
            }
            else
            {
                _tutorial.gameObject.SetActive(false);
            }
        }

        private void OnTutorialFinished(Vector2 position)
        {
            if (_tutorial.gameObject.activeSelf)
            {
                _tutorial.DOKill();
                _tutorial.DOFade(0f, _tutorialFadeTime)
                    .OnComplete(() =>
                    {
                        _isTutorialFinished = true;
                        _tutorial.gameObject.SetActive(false);
                    });
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (_inputController != null)
            {
                if (_tutorialType == TutorialType.Touch)
                {
                    _inputController.TouchDown -= OnTutorialFinished;
                }
                else if (_tutorialType == TutorialType.Drag)
                {
                    _inputController.Drag -= OnTutorialFinished;
                }
                else if (_tutorialType == TutorialType.Release)
                {
                    _inputController.TouchUp -= OnTutorialFinished;
                }
            }
        }
    }
}