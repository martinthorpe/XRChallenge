using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class EditMapSizeButtonScript : MonoBehaviour
{
    public event Action OnClicked;

    public void InIt(string phrase)
    {
        GetComponent<Text>().text = phrase;
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    private void ButtonOnClick()
    {
        OnClicked?.Invoke();
    }
}