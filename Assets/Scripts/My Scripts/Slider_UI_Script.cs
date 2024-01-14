using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Slider_UI_Script : MonoBehaviour
{
    private Slider m_sSliderScroll;

    /// <summary>
    /// Gets the Slider component as well as calls ChangeValue function,
    /// </summary>
    public void InIt(float value)
    {
        m_sSliderScroll = gameObject.GetComponent<Slider>();
        ChangeValue(value);
    }

    /// <summary>
    /// Changes the Slider value to the passed value.
    /// </summary>
    public void ChangeValue(float value)
    {
        m_sSliderScroll.value = value;
    }
}