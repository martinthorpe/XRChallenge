using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finished_Area_Script : MonoBehaviour
{
    private bool m_bIsEnterable;

    public event Action EnteredArea;

    public void InIt(bool b)
    {
        SetEnterable(b);
    }

    public void SetEnterable(bool b)
    {
        m_bIsEnterable = b;
    }

    public void Entered()
    {
        if (m_bIsEnterable)
        {
            EnteredArea?.Invoke();
        }
    }
}