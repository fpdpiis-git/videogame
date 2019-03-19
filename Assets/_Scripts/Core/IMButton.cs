namespace ioc.IOCStudents.Core
{
    public class IMButton
    {
        public enum ButtonStates { Off, ButtonDown, ButtonPressed, ButtonUp }

        public ButtonStates State { get; set; }
        public string m_buttonID;

        public delegate void ButtonDownMethodDelegate();
        public delegate void ButtonPressedMethodDelegate();
        public delegate void ButtonUpMethodDelegate();

        public ButtonDownMethodDelegate m_buttonDownMethod;
        public ButtonPressedMethodDelegate m_buttonPressedMethod;
        public ButtonUpMethodDelegate m_buttonUpMethod;

        public IMButton(string playerID, string buttonID, ButtonDownMethodDelegate btnDown, ButtonPressedMethodDelegate btnPressed, ButtonUpMethodDelegate btnUp)
        {            
            m_buttonID = playerID + "_" + buttonID;
            m_buttonDownMethod = btnDown;
            m_buttonUpMethod = btnUp;
            m_buttonPressedMethod = btnPressed;
            State = ButtonStates.Off;
        }

        public IMButton(string buttonID, ButtonDownMethodDelegate btnDown, ButtonPressedMethodDelegate btnPressed, ButtonUpMethodDelegate btnUp)
        {
            m_buttonID = buttonID;
            m_buttonDownMethod = btnDown;
            m_buttonUpMethod = btnUp;
            m_buttonPressedMethod = btnPressed;
            State = ButtonStates.Off;
        }

        public virtual void TriggerButtonDown()
        {
            m_buttonDownMethod();
        }

        public virtual void TriggerButtonPressed()
        {
            m_buttonPressedMethod();
        }

        public virtual void TriggerButtonUp()
        {
            m_buttonUpMethod();
        }
    }
}