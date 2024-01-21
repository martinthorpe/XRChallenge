using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class GridSlotScript : MonoBehaviour
{
    private Image m_Image;
    private int m_Item;
    private Vector2 m_MapCoords;
    public event Action<GridSlotScript> OnClicked;

    public void InIt(Sprite sprite, int item, Vector2 coords)
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = sprite;
        m_Item = item;
        m_MapCoords = coords;
    }

    public int GetItem()
    {
        return m_Item;
    }

    public Vector2 GetMapCoords()
    {
        return m_MapCoords;
    }

    public void ChangeItem(Sprite sprite, int item)
    {
        m_Image.sprite = sprite;
        m_Item = item;
    }

    public void ClickedGrid()
    {
        OnClicked?.Invoke(this);
    }
}