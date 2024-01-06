using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player_Controller_Script : MonoBehaviour
{
    [SerializeField] private float m_fMaxHealth;
    private float m_fCurrentHealth;
    private PlayerInput m_Input;
    private Rigidbody m_RB;
    public event Action Death;
    [SerializeField] private float m_fSpeed;
    [SerializeField] private float m_fJumpForce;
    private Coroutine m_CRMove;
    private Vector2 m_VMove;
    private bool m_bIsMoving;
    private bool m_bIsJumping;
    private bool m_bIsGrounded;
    [SerializeField] private IsGrounded_Script m_Grounded;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        m_Input.currentActionMap.FindAction("Move").performed -= Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled -= Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed -= Handle_JumpPerformed;
        m_Grounded.GetComponent<IsGrounded_Script>().OnHitGround -= HitGround;
        m_Grounded.GetComponent<IsGrounded_Script>().OnLeftGround -= LeftGround;
    }

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

    public void GetHealth()
    {

    }

    public float SetHealth()
    {
        return m_fCurrentHealth;
    }

    private void UpdateHealthUI()
    {

    }

    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        m_VMove = context.ReadValue<Vector2>();
        m_bIsMoving = true;
        if (m_CRMove == null)
        {
            m_CRMove = StartCoroutine(C_MoveUpdate());
        }
    }

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

    private void Handle_JumpPerformed(InputAction.CallbackContext context)
    {
        if (!m_bIsJumping && m_bIsGrounded)
        {
            m_RB.velocity = new Vector3(0.0f, m_fJumpForce, 0.0f);
            m_bIsJumping = true;
        }
    }

    private IEnumerator C_MoveUpdate()
    {
        while (m_bIsMoving)
        {
            m_RB.velocity = new Vector3(m_VMove.x * m_fSpeed, m_RB.velocity.y, m_VMove.y * m_fSpeed);
            yield return null;
        }
    }

    public void HitGround()
    {
        m_bIsGrounded = true;
        m_bIsJumping = false;
    }

    public void LeftGround()
    {
        m_bIsGrounded = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Pickup>(out Pickup pick))
        {
            int lol = collider.gameObject.GetComponent<Pickup>().GetPickedUp();
        }
    }
}