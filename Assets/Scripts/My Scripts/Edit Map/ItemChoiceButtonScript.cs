using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ItemChoiceButtonScript : MonoBehaviour
{
    private int m_iItemType;
    public event Action<int> OnClicked;

    public void InIt(Sprite sprite, int item)
    {
        GetComponent<Image>().sprite = sprite;
        m_iItemType = item;
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    private void ButtonOnClick()
    {
        OnClicked?.Invoke(m_iItemType);
    }
}