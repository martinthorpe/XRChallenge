using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMesh))]
public class Score_UI_Script : MonoBehaviour
{
    private TextMesh m_Text;

    public void InIt()
    {
        m_Text = gameObject.GetComponent<TextMesh>();
        m_Text.text = "SCORE: 0";
    }

    public void ChangeText(string score)
    {
        print(score);
        m_Text.text = "SCORE: " + score;
    }
}