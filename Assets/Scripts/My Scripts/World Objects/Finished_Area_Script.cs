using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finished_Area_Script : MonoBehaviour
{
    private bool m_bIsEnterable;
    public event Action EnteredArea;

    /// <summary>
    /// Calls the SetEnterable function with the value it was passed.
    /// </summary>
    public void InIt(bool b)
    {
        SetEnterable(b);
    }

    /// <summary>
    /// Sets IsEnterable value to what it was passed.
    /// </summary>
    public void SetEnterable(bool b)
    {
        m_bIsEnterable = b;
    }

    /// <summary>
    /// Checks if IsEnterable is true.
    /// If so calls EnteredArea event
    /// </summary>
    public void Entered()
    {
        if (m_bIsEnterable)
        {
            EnteredArea?.Invoke();
        }
    }
}