using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class IsGrounded_Script : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private LayerMask m_LMGroundLayer;

    public event Action OnHitGround;
    public event Action OnLeftGround;

    /// <summary>
    /// Sets all the events on this script to null.
    /// </summary>
    public void InIt()
    {
        OnHitGround = null;
        OnLeftGround = null;
    }

    /// <summary>
    /// If entered object is of the layer set to the Ground Layer Mask value.
    /// Then calls OnHitGround event.
    /// </summary>
    private void OnTriggerEnter(Collider col)
    {
        if ((m_LMGroundLayer.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            OnHitGround?.Invoke();
        }
    }

    /// <summary>
    /// If exits object is of the layer set to the Ground Layer Mask value.
    /// Then calls OnLeftGround event.
    /// </summary>
    private void OnTriggerExit(Collider col)
    {
        if ((m_LMGroundLayer.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            OnLeftGround?.Invoke();
        }
    }
}