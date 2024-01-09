using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Potion_Script : MonoBehaviour, IHealthAffector
{
    [SerializeField] private float m_fHealth;


    public float AddedPlayerHealthValue()
    {
        return m_fHealth;
    }
}