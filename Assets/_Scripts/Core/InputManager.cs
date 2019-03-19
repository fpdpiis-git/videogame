using UnityEngine;
using System.Collections.Generic;

namespace ioc.IOCStudents.Core
{
    /// <summary>
    /// IMPORTANT : this script's Execution Order MUST be -100.
    /// You can define a script's execution order by clicking on the script's file and then clicking on the Execution Order button at the bottom right of the script's inspector.
    /// See https://docs.unity3d.com/Manual/class-ScriptExecution.html for more details
    /// </summary>    
    public class InputManager : Singleton<InputManager>
    {
        public bool m_inputDetectionActive = true;
        public string m_playerID = "Player1";

        /// If set to true, acceleration / deceleration will take place when moving / stopping
        public bool m_smoothMovement = true;
        /// the minimum horizontal and vertical value you need to reach to trigger movement on an analog controller (joystick for example)
        public Vector2 m_threshold = new Vector2(0.1f, 0.4f);

        public IMButton JumpButton { get; protected set; }
        public IMButton ShootButton { get; protected set; }
        public IMButton PauseButton { get; protected set; }

        public Vector2 PrimaryMovement { get { return m_primaryMovement; } }
        public Vector2 SecondaryMovement { get { return m_secondaryMovement; } }

        protected List<IMButton> m_buttonList;
        protected Vector2 m_primaryMovement = Vector2.zero;
        protected Vector2 m_secondaryMovement = Vector2.zero;
        protected string m_axisHorizontal;
        protected string m_axisVertical;
        protected string m_axisSecondaryHorizontal;
        protected string m_axisSecondaryVertical;

        protected virtual void Start()
        {
            InitializeButtons();
            InitializeAxis();
        }

        protected virtual void InitializeButtons()
        {
            m_buttonList = new List<IMButton>();
            m_buttonList.Add(JumpButton = new IMButton(m_playerID, "Jump", JumpButtonDown, JumpButtonPressed, JumpButtonUp));
            m_buttonList.Add(ShootButton = new IMButton(m_playerID, "Shoot", ShootButtonDown, ShootButtonPressed, ShootButtonUp));
            m_buttonList.Add(PauseButton = new IMButton(m_playerID, "Pause", PauseButtonDown, PauseButtonPressed, PauseButtonUp));
        }

        protected virtual void InitializeAxis()
        {
            m_axisHorizontal = m_playerID + "_Horizontal";
            m_axisVertical = m_playerID + "_Vertical";
            m_axisSecondaryHorizontal = m_playerID + "_SecondaryHorizontal";
            m_axisSecondaryVertical = m_playerID + "_SecondaryVertical";
        }

        protected virtual void LateUpdate()
        {
            ProcessButtonStates();
        }

        protected virtual void Update()
        {
            SetMovement();
            SetSecondaryMovement();
            GetInputButtons();
        }

        protected virtual void GetInputButtons()
        {
            foreach (IMButton button in m_buttonList)
            {
                if (Input.GetButton(button.m_buttonID))
                {
                    button.TriggerButtonPressed();
                }
                if (Input.GetButtonDown(button.m_buttonID))
                {
                    button.TriggerButtonDown();
                }
                if (Input.GetButtonUp(button.m_buttonID))
                {
                    button.TriggerButtonUp();
                }
            }
        }

        public virtual void ProcessButtonStates()
        {
            foreach (IMButton button in m_buttonList)
            {
                if (button.State == IMButton.ButtonStates.ButtonDown)
                {
                    button.State = IMButton.ButtonStates.ButtonPressed;
                }
                if (button.State == IMButton.ButtonStates.ButtonUp)
                {
                    button.State = IMButton.ButtonStates.Off;
                }
            }
        }

        public virtual void SetMovement()
        {
            if (m_inputDetectionActive)
            {
                if (m_smoothMovement)
                {
                    m_primaryMovement.x = Input.GetAxis(m_axisHorizontal);
                    m_primaryMovement.y = Input.GetAxis(m_axisVertical);
                }
                else
                {
                    m_primaryMovement.x = Input.GetAxisRaw(m_axisHorizontal);
                    m_primaryMovement.y = Input.GetAxisRaw(m_axisVertical);
                }
            }
        }

        public virtual void SetSecondaryMovement()
        {
            if (m_inputDetectionActive)
            {
                if (m_smoothMovement)
                {
                    m_secondaryMovement.x = Input.GetAxis(m_axisSecondaryHorizontal);
                    m_secondaryMovement.y = Input.GetAxis(m_axisSecondaryVertical);
                }
                else
                {
                    m_secondaryMovement.x = Input.GetAxisRaw(m_axisSecondaryHorizontal);
                    m_secondaryMovement.y = Input.GetAxisRaw(m_axisSecondaryVertical);
                }
            }
        }

        public virtual void JumpButtonDown() { JumpButton.State = IMButton.ButtonStates.ButtonDown; }
        public virtual void JumpButtonPressed() { JumpButton.State = IMButton.ButtonStates.ButtonPressed; }
        public virtual void JumpButtonUp() { JumpButton.State = IMButton.ButtonStates.ButtonUp; }

        public virtual void ShootButtonDown() { ShootButton.State = IMButton.ButtonStates.ButtonDown; }
        public virtual void ShootButtonPressed() { ShootButton.State = IMButton.ButtonStates.ButtonPressed; }
        public virtual void ShootButtonUp() { ShootButton.State = IMButton.ButtonStates.ButtonUp; }

        public virtual void PauseButtonDown() { PauseButton.State = IMButton.ButtonStates.ButtonDown; }
        public virtual void PauseButtonPressed() { PauseButton.State = IMButton.ButtonStates.ButtonPressed; }
        public virtual void PauseButtonUp() { PauseButton.State = IMButton.ButtonStates.ButtonUp; }
    }
}