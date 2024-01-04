using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded_Script : MonoBehaviour
{
    public event Action OnHitGround;
    public event Action OnLeftGround;

    [SerializeField] private LayerMask m_LMGroundLayer;

    private void OnTriggerEnter(Collider col)
    {
        //If enters ground collider then triggers event
        if ((m_LMGroundLayer.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            OnHitGround?.Invoke();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //If enters ground collider then triggers event
        if ((m_LMGroundLayer.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            OnLeftGround?.Invoke();
        }
    }
}