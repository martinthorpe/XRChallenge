using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Potion_Script : MonoBehaviour, IHealthAffector
{
    [Header("Config")]
    [SerializeField, Range(0, 1000)] private float m_fHealth;

	/// <returns>The amount of health the potion gives.</returns>
    public float ChangedPlayerHealthValue()
    {
        return m_fHealth;
    }
}