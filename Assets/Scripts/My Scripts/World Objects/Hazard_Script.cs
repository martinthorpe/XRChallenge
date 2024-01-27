using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Script : MonoBehaviour, IHealthAffector
{
    [Header("Config")]
    [SerializeField, Range(0, 1000)] private float m_fDamage;

	/// <returns>The amount of damage it deals.</returns>
    public float ChangedPlayerHealthValue()
    {
        return m_fDamage * -1.0f;
    }
}