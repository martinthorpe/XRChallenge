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
    private int m_iRow;
    public event Action<GridSlotScript> OnClicked;

    public void InIt(Sprite sprite, int item, int row)
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = sprite;
        m_Item = item;
        m_iRow = row;
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    public int GetItem()
    {
        return m_Item;
    }

    public int GetYPoint()
    {
        return m_iRow;
    }

    public void ChangeItem(Sprite sprite, int item)
    {
        m_Image.sprite = sprite;
        m_Item = item;
    }

    private void ButtonOnClick()
    {
        OnClicked?.Invoke(this);
    }
}