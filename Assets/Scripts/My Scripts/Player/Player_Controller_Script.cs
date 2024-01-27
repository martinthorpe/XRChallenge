using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player_Controller_Script : MonoBehaviour
{
    [Header("Config")]
    [SerializeField, Range (0.1f, 4.0f)] private float m_fSpeed;
    [SerializeField, Range(2.0f, 10.0f)] private float m_fMaxVelocity;
    [SerializeField, Range(50.0f, 500.0f)] private float m_fJumpForce;
    [SerializeField, Range(0.01f, 1.0f)] private float m_fSensitivity;

    [Header("References")]
    [SerializeField] private IsGrounded_Script m_Grounded;
    private PlayerInput m_Input;
    private Rigidbody m_RB;

    private Coroutine m_CRMove;
    private Vector2 m_VMove;
    private bool m_bIsMoving;
    private bool m_bIsJumping;
    private bool m_bIsGrounded;
    private Coroutine m_CRTurn;
    private bool m_bIsTurning;
    private float m_fCurrentY;
    private float m_fTurnAmount;

    /// <summary>
    /// Removes the connection to the Input Actions and Ground Script events to functions in this script.
    /// </summary>
    private void OnDisable()
    {
        m_Input.currentActionMap.FindAction("Move").performed -= Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled -= Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed -= Handle_JumpPerformed;
        m_Input.currentActionMap.FindAction("Camera").performed -= Handle_CameraPerformed;
        m_Input.currentActionMap.FindAction("Camera").canceled -= Handle_CameraCanceled;
        m_Grounded.OnHitGround -= HitGround;
        m_Grounded.OnLeftGround -= LeftGround;
    }

    /// <summary>
    /// Gets Input and Rigidbody components.
    /// Resets varibles.
    /// Assigns the connections of the Input Actions and Ground Script events to functions in this script.
    /// </summary>
    public void InIt()
    {
        m_Input = GetComponent<PlayerInput>();
        m_RB = GetComponent<Rigidbody>();

        m_CRMove = null;
        m_CRTurn = null;
        m_VMove = new Vector2(0.0f, 0.0f);
        m_fCurrentY = 0.0f;
        m_fTurnAmount = 0.0f;
        m_bIsMoving = false;
        m_bIsJumping = false;
        m_bIsTurning = false;

        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPerformed;
        m_Input.currentActionMap.FindAction("Camera").performed += Handle_CameraPerformed;
        m_Input.currentActionMap.FindAction("Camera").canceled += Handle_CameraCanceled;

        if (m_Grounded != null)
        {
            m_Grounded.InIt();
            m_Grounded.OnHitGround += HitGround;
            m_Grounded.OnLeftGround += LeftGround;
        }
    }

    /// <summary>
    /// Sets the context value being passed to the Turn float value and Turning varible to true.
    /// Starts the turn coroutine.
    /// </summary>
    private void Handle_CameraPerformed(InputAction.CallbackContext context)
    {
        m_fTurnAmount = context.ReadValue<float>();
        m_bIsTurning = true;
        if (m_CRTurn == null)
        {
            m_CRTurn = StartCoroutine(C_TurnUpdate());
        }
    }

    /// <summary>
    /// Sets the context value being passed to the Turn float value and sets Turning to false.
    /// Checks if coroutine is active, which if it is it stops.
    /// </summary>
    private void Handle_CameraCanceled(InputAction.CallbackContext context)
    {
        m_fTurnAmount = context.ReadValue<float>();
        m_bIsTurning = false;
        if (m_CRTurn != null)
        {
            StopCoroutine(m_CRTurn);
            m_CRTurn = null;
        }
    }

    /// <summary>
    /// While Is Turning is true, rotates the player.
    /// </summary>
    private IEnumerator C_TurnUpdate()
    {
        while (m_bIsTurning)
        {
            m_fCurrentY += (m_fSensitivity * m_fTurnAmount);
            transform.localRotation = Quaternion.Euler(0.0f, m_fCurrentY, 0.0f);
            yield return null;
        }
    }

    /// <summary>
    /// Sets the context value being passed to the move vector2 value and moving varible to true.
    /// Starts the move coroutine.
    /// </summary>
    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        m_VMove = context.ReadValue<Vector2>();
        m_bIsMoving = true;
        if (m_CRMove == null)
        {
            m_CRMove = StartCoroutine(C_MoveUpdate());
        }
    }

    /// <summary>
    /// Sets the context value being passed to the Move vector2 value and sets Is Moving to false.
    /// Checks if coroutine is active, which if it is it stops.
    /// </summary>
    private void Handle_MoveCancelled(InputAction.CallbackContext context)
    {
        m_VMove = context.ReadValue<Vector2>();
        m_RB.velocity = new Vector3(m_VMove.x * m_fSpeed, m_RB.velocity[1], m_VMove.y * m_fSpeed);
        m_bIsMoving = false;
        if (m_CRMove != null)
        {
            StopCoroutine(m_CRMove);
            m_CRMove = null;
        }
    }

    /// <summary>
    /// Checks if isn't already jumping and is grounded, if so then applies force upwards and sets Is Jumping to true.
    /// </summary>
    private void Handle_JumpPerformed(InputAction.CallbackContext context)
    {
        if (!m_bIsJumping && m_bIsGrounded)
        {
            m_RB.AddForce(transform.up * m_fJumpForce);
            m_bIsJumping = true;
        }
    }

    /// <summary>
    /// While Is Moving is true, then adds force to the rigidbody attached to the player.
    /// </summary>
    private IEnumerator C_MoveUpdate()
    {
        while (m_bIsMoving)
        {
            m_RB.AddForce(transform.forward * m_VMove.y * m_fSpeed);
            m_RB.AddForce(transform.right * m_VMove.x * m_fSpeed);
            if (m_RB.velocity[2] >= m_fMaxVelocity)
            {
                m_RB.velocity = new Vector3(m_RB.velocity[0], m_RB.velocity[1], m_fMaxVelocity);
            }
            else if (m_RB.velocity[2] <= m_fMaxVelocity * -1.0f)
            {
                m_RB.velocity = new Vector3(m_RB.velocity[0], m_RB.velocity[1], m_fMaxVelocity * -1.0f);
            }
            if (m_RB.velocity[0] >= m_fMaxVelocity)
            {
                m_RB.velocity = new Vector3(m_fMaxVelocity, m_RB.velocity[1], m_RB.velocity[2]);
            }
            else if (m_RB.velocity[0] <= m_fMaxVelocity * -1.0f)
            {
                m_RB.velocity = new Vector3(m_fMaxVelocity * - 1.0f, m_RB.velocity[1], m_RB.velocity[2]);
            }
            yield return null;
        }
    }

    /// <summary>
    /// Sets Is Grounded to true and sets Is Jumping to false when hit the ground. 
    /// </summary>
    public void HitGround()
    {
        m_bIsGrounded = true;
        m_bIsJumping = false;
    }

    /// <summary>
    /// Sets Is Grounded to false when left the ground.
    /// </summary>
    public void LeftGround()
    {
        m_bIsGrounded = false;
    }
}