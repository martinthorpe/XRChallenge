using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    private Image m_Image;
    private int m_Item;
    public event Action<GridSlot> OnClicked;

    public void InIt(Sprite sprite, int item)
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = sprite;
        m_Item = item;
    }
}