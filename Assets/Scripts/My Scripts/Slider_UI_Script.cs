using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_UI_Script : MonoBehaviour
{
    private Slider m_sSliderScroll;

    public void InIt(float value)
    {
        m_sSliderScroll = gameObject.GetComponent<Slider>();
        ChangeValue(value);
    }

    public void ChangeValue(float value)
    {
        m_sSliderScroll.value = value;
    }
}