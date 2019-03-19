using ioc.IOCStudents.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ioc.IOCStudents.Test
{
    public class EventsUITest : MonoBehaviour, IOCEventListener<IOCGameEvent>
    {
        [SerializeField]
        private Text m_textField = null;

        void OnEnable()
        {
            this.IOCEventStartListening<IOCGameEvent>();
        }

        void OnDisable()
        {
            this.IOCEventStopListening<IOCGameEvent>();
        }

        public void OnIOCEvent(IOCGameEvent gameEvent)
        {
            if (gameEvent.m_eventName == "NEW_SCORE")
            {
                m_textField.text = gameEvent.m_eventData;
            }
        }
    }
}