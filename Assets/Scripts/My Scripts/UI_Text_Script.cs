using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class UI_Text_Script : MonoBehaviour
{
    private TextMeshProUGUI m_Text;

    /// <summary>
    /// Gets the Text component and sets it to the passed value.
    /// </summary>
    public void InIt(string startText)
    {
        m_Text = gameObject.GetComponent<TextMeshProUGUI>();
        ChangeText(startText);
    }

    /// <summary>
    /// Changes Text to passed value.
    /// </summary>
    public void ChangeText(string score)
    {
        m_Text.text = score;
    }
}