using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Script : MonoBehaviour, IHealthAffector
{
    [Header("Config")]
    [SerializeField] private float m_fDamage;

	/// <returns>The amount of damage it deals.</returns>
    public float AddedPlayerHealthValue()
    {
        return m_fDamage * -1.0f;
    }
}