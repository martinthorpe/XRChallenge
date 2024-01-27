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

    /// <summary>
	/// Assigns pasted values to itself.
    /// As well as sets up the button attached to the function below.
	/// </summary>
    public void InIt(string phrase)
    {
        OnClicked = null;
        GetComponent<Text>().text = phrase;
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
	/// Once called invokes an event.
	/// </summary>
    private void ButtonOnClick()
    {
        OnClicked?.Invoke();
    }
}