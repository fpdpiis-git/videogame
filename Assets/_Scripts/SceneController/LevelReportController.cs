using ioc.IOCStudents.Core;
using UnityEngine;

namespace ioc.IOCStudents.SceneController
{
    public class LevelReportController : MonoBehaviour
    {
        private bool m_isActive;

        protected void Start()
        {
            m_isActive = true;
        }

        protected void Update()
        {
            if (!m_isActive)
            {
                return;
            }

            if (InputManager.Instance.JumpButton.State == IMButton.ButtonStates.ButtonDown
                || InputManager.Instance.ShootButton.State == IMButton.ButtonStates.ButtonDown)
            {
                GameManager.Instance.ToManinMenu();
                m_isActive = false;
            }

        }
    }
}