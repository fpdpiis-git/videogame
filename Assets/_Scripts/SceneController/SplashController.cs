using ioc.IOCStudents.Core;
using System.Collections;
using UnityEngine;

namespace ioc.IOCStudents.SceneController
{
    public class SplashController : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_logo = null;

        
        protected void Start()
        {
            LeanTween.alpha(m_logo, 1.0f, 2.0f).setOnComplete(OnAlphaInEnded);
        }

        private void OnAlphaInEnded()
        {
            StartCoroutine(OnWait());
        }

        private IEnumerator OnWait()
        {
            yield return new WaitForSeconds(2.0f);
            LeanTween.alpha(m_logo, 0.0f, 2.0f).setOnComplete(OnAlphaOutEnded);
        }

        private void OnAlphaOutEnded()
        {
            // Aqui aniara la transicio al menu principal
            GameManager.Instance.ToTransitionScene2();
        }        
    }
}