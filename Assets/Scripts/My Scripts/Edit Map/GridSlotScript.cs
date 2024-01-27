using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class GridSlotScript : MonoBehaviour
{
    private int m_Item;
    private int m_iRow;
    public event Action<GridSlotScript> OnClicked;

    private Image m_Image;

    /// <summary>
    /// Resets values and sets passed values to itself.
    /// As well as sets up the button attached to the function below.
    /// </summary>
    public void InIt(Sprite sprite, int item, int row)
    {
        OnClicked = null;
        m_Image = GetComponent<Image>();
        m_Image.sprite = sprite;
        m_Item = item;
        m_iRow = row;
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    /// <returns>Item value.</returns>
    public int GetItem()
    {
        return m_Item;
    }

    /// <returns>Row value.</returns>
    public int GetYPoint()
    {
        return m_iRow;
    }

    /// <summary>
    /// Changes values to the passed ones.
    /// </summary>
    public void ChangeItem(Sprite sprite, int item)
    {
        m_Image.sprite = sprite;
        m_Item = item;
    }

    /// <summary>
	/// Once called invokes an event passing a reference to itself.
	/// </summary>
    private void ButtonOnClick()
    {
        OnClicked?.Invoke(this);
    }
}