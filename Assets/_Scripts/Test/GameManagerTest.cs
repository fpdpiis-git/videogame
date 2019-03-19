using ioc.IOCStudents.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ioc.IOCStudents.Test
{ 
    public class GameManagerTest : MonoBehaviour
    {
     
        protected void Start() {

            StartCoroutine(WaitToTransition());
        }

        private IEnumerator WaitToTransition()
        {
            // Wait 10s
            yield return new WaitForSeconds(5.0f);

            // Do Transition
            GameManager.Instance.ToTransitionScene2();
        }
    }
}
