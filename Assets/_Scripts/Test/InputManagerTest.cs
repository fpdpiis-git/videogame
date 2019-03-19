using ioc.IOCStudents.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ioc.IOCStudents.Test
{
    public class InputManagerTest : MonoBehaviour
    {
        protected void Start()
        {
            InputManager.Instance.ShootButton.m_buttonDownMethod += OnFireButtonDown;
            InputManager.Instance.ShootButton.m_buttonUpMethod += OnFireButtonUp;
            InputManager.Instance.ShootButton.m_buttonPressedMethod += OnFireButtonPressed;
        }

        //protected void OnDisable()
        //{
        //    if (InputManager.Instance.ShootButton != null )
        //    {
        //        InputManager.Instance.ShootButton.m_buttonDownMethod -= OnFireButtonDown;
        //        InputManager.Instance.ShootButton.m_buttonUpMethod -= OnFireButtonUp;
        //        InputManager.Instance.ShootButton.m_buttonPressedMethod -= OnFireButtonPressed;
        //    }            
        //}

        private void OnFireButtonDown()
        {
            Debug.Log("Fire Button Down");
        }

        private void OnFireButtonUp()
        {
            Debug.Log("Fire Button Up");
        }
        private void OnFireButtonPressed()
        {
            Debug.Log("Fire Button Pressed");
        }

        protected void Update()
        {
            if ( InputManager.Instance.JumpButton.State == IMButton.ButtonStates.ButtonDown )
            {
                Debug.Log("Jump Button Down");
            }

            if (InputManager.Instance.JumpButton.State == IMButton.ButtonStates.ButtonPressed)
            {
                Debug.Log("Jump Button Pressed");
            }

            if (InputManager.Instance.JumpButton.State == IMButton.ButtonStates.ButtonUp)
            {
                Debug.Log("Jump Button Up");
            }

        }
    }
}
