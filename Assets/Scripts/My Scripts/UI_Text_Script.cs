using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class UI_Text_Script : MonoBehaviour
{
    private TextMeshProUGUI m_Text;

    public void InIt(string startText)
    {
        m_Text = gameObject.GetComponent<TextMeshProUGUI>();
        m_Text.text = startText;
    }

    public void ChangeText(string score)
    {
        m_Text.text = score;
    }
}