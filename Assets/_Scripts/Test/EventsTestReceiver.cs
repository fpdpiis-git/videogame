using ioc.IOCStudents.Core;
using UnityEngine;

namespace ioc.IOCStudents.Test
{
    public class EventsTestReceiver : MonoBehaviour, IOCEventListener<IOCGameEvent>
    {
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
            Debug.Log(gameEvent.m_eventName);

            if (gameEvent.m_eventName == "1de3")
            {
                Debug.Log("Aquest es un esdeveniment del tipus 1de3");
            }
        }
    }
}