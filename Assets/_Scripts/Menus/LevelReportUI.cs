using TMPro;
using UnityEngine;

namespace ioc.IOCStudents.Menus
{
    public class LevelReportUI : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI m_ScoreText = null;

        [SerializeField]
        protected TextMeshProUGUI m_enmiesKilledText = null;

        [SerializeField]
        protected TextMeshProUGUI m_PlayedTime = null;


        protected void Start()
        {
            m_ScoreText.text = "0000";
            m_enmiesKilledText.text = "0";
            m_PlayedTime.text = "0m 0s";
        }
    }
}