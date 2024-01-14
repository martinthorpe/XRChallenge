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
    [SerializeField] private float m_fSpeed;
    [SerializeField] private float m_fJumpForce;

    [Header("References")]
    [SerializeField] private IsGrounded_Script m_Grounded;
    private PlayerInput m_Input;
    private Rigidbody m_RB;

    private Coroutine m_CRMove;
    private Vector2 m_VMove;
    private bool m_bIsMoving;
    private bool m_bIsJumping;
    private bool m_bIsGrounded;

    /// <summary>
    /// Removes the connection to the Input Actions and Ground Script events to functions in this script.
    /// </summary>
    private void OnDisable()
    {
        m_Input.currentActionMap.FindAction("Move").performed -= Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled -= Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed -= Handle_JumpPerformed;
        m_Grounded.GetComponent<IsGrounded_Script>().OnHitGround -= HitGround;
        m_Grounded.GetComponent<IsGrounded_Script>().OnLeftGround -= LeftGround;
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
        m_VMove = new Vector2(0.0f, 0.0f);
        m_bIsMoving = false;
        m_bIsJumping = false;

        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPerformed;
        m_Grounded.GetComponent<IsGrounded_Script>().OnHitGround += HitGround;
        m_Grounded.GetComponent<IsGrounded_Script>().OnLeftGround += LeftGround;
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
        m_RB.velocity = new Vector2(m_VMove.x * m_fSpeed, m_VMove.y * m_fSpeed);
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
            m_RB.velocity = new Vector3(0.0f, m_fJumpForce, 0.0f);
            m_bIsJumping = true;
        }
    }

    /// <summary>
    /// While Is Moving is true, then sets the rigidbody attached to the player velocity a new value using the Move and Speed varibles.
    /// </summary>
    private IEnumerator C_MoveUpdate()
    {
        while (m_bIsMoving)
        {
            m_RB.velocity = new Vector3(m_VMove.x * m_fSpeed, m_RB.velocity.y, m_VMove.y * m_fSpeed);
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