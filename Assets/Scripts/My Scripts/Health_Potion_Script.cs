using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Potion_Script : MonoBehaviour, IHealthAffector
{
    [Header("Config")]
    [SerializeField] private float m_fHealth;

	/// <returns>The amount of health the potion gives.</returns>
    public float AddedPlayerHealthValue()
    {
        return m_fHealth;
    }
}