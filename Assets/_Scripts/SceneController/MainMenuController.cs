using UnityEngine;
using ioc.IOCStudents.Core;

namespace ioc.IOCStudents.SceneController
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        protected RectTransform m_fadeButtonFrame = null;

        protected void Start()
        {
            InputManager.Instance.JumpButton.m_buttonDownMethod += OnStartLevelPressed;

            LeanTween.alpha(m_fadeButtonFrame, 0.0f, 2.0f).setLoopPingPong();
        }

        private void OnStartLevelPressed()
        {
            LeanTween.cancel(m_fadeButtonFrame);
            m_fadeButtonFrame.gameObject.SetActive(false);

            //GameManager.Instance.ToGame();
            GameManager.Instance.ToTransitionScene2();
        }

        protected void OnDisable()
        {
            if (InputManager.Instance.JumpButton == null)
            {
                return;
            }

            InputManager.Instance.JumpButton.m_buttonDownMethod -= OnStartLevelPressed;
        }
    }
}
