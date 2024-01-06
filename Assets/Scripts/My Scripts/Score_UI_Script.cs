using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class Score_UI_Script : MonoBehaviour
{
    private TextMeshProUGUI m_Text;

    public void InIt()
    {
        m_Text = gameObject.GetComponent<TextMeshProUGUI>();
        m_Text.text = "SCORE: 0";
    }

    public void ChangeText(string score)
    {
        m_Text.text = "SCORE: " + score;
    }
}