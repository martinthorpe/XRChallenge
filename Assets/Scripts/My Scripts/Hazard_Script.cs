using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Script : MonoBehaviour, IHealthAffector
{
    [SerializeField] private float m_fDamage;


    public float AddedPlayerHealthValue()
    {
        return m_fDamage * -1.0f;
    }
}