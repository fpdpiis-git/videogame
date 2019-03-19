using UnityEngine;
using System.Collections;
using ioc.IOCStudents.Core;

public class PlayerInput : Singleton<PlayerInput>
{
    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;

    public Vector2 MoveInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera;
        }
    }

    public bool JumpInput
    {
        get { return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Attack
    {
        get { return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Pause
    {
        get { return m_Pause; }
    }

    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;

    const float k_AttackInputDuration = 0.03f;

    protected override void Awake()
    {
        base.Awake();
        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);
    }


    void Update()
    {
        m_Movement = InputManager.Instance.PrimaryMovement;
        m_Camera = InputManager.Instance.SecondaryMovement;
        m_Jump = InputManager.Instance.JumpButton.State == IMButton.ButtonStates.ButtonDown;
        m_Pause = InputManager.Instance.PauseButton.State == IMButton.ButtonStates.ButtonDown;
        if (InputManager.Instance.ShootButton.State == IMButton.ButtonStates.ButtonDown)
        {
            if (m_AttackWaitCoroutine != null)
                StopCoroutine(m_AttackWaitCoroutine);

            m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;

        yield return m_AttackInputWait;

        m_Attack = false;
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }
}



//m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
//m_Jump = Input.GetButton("Jump");
//m_Pause = Input.GetButtonDown("Pause");


//if (Input.GetButtonDown("Fire1"))
//{
//    if (m_AttackWaitCoroutine != null)
//        StopCoroutine(m_AttackWaitCoroutine);

//    m_AttackWaitCoroutine = StartCoroutine(AttackWait());
//}