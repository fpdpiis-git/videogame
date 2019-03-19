using ioc.IOCStudents.Core;
using System.Collections;
using UnityEngine;

namespace ioc.IOCStudents.Test
{
    public class EventsTestSender : MonoBehaviour
    {
        private int m_numEvtSended;
        private int m_score;

        protected void Start()
        {
            m_numEvtSended = 0;
            m_score = 0;

            StartCoroutine(GenerateEvent());
        }

        private IEnumerator GenerateEvent()
        {
            while (true)
            {
                yield return new WaitForSeconds(2.0f);

                ++m_numEvtSended;

                if (m_numEvtSended == 3)
                {
                    IOCGameEvent.Trigger("1de3", "");
                    IOCGameEvent.Trigger("NEW_SCORE", "" + (m_score + 1000));

                    m_score += 1000;
                    m_numEvtSended = 0;
                }
                else
                {
                    IOCGameEvent.Trigger(Time.realtimeSinceStartup + " New Event", "");
                }
            }
        }
    }
}
