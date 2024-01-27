using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class RowManagerScript : MonoBehaviour
{
    private List<GridSlotScript> m_ListOfGridSlots;
    /// <summary>
	/// Resets the grid slot script list.
	/// </summary>
    public void InIt()
    {
        m_ListOfGridSlots = new List<GridSlotScript>();
    }
    /// <summary>
	/// Instantiate a new grid slot.
	/// </summary>
	/// <returns>New grid slot script.</returns>
    public GridSlotScript AddRowItem(Sprite sprite, int itemNum, int y, GameObject slotGridPrefab)
    {
        m_ListOfGridSlots.Add(Instantiate(slotGridPrefab, gameObject.transform).GetComponent<GridSlotScript>());
        m_ListOfGridSlots[m_ListOfGridSlots.Count - 1].InIt(sprite, itemNum, y);
        return m_ListOfGridSlots[m_ListOfGridSlots.Count - 1];
    }
    /// <summary>
	/// Removes the end grid slot script from the list.
	/// </summary>
	/// <returns>The removed grid slot.</returns>
    public GridSlotScript RemoveRowItem()
    {
        GridSlotScript gridSlotObject = m_ListOfGridSlots[m_ListOfGridSlots.Count - 1];
        m_ListOfGridSlots.RemoveAt(m_ListOfGridSlots.Count - 1);
        return gridSlotObject;
    }

	/// <returns>The grid slot script list.</returns>
    public List<GridSlotScript> GetGridSlotList()
    {
        return m_ListOfGridSlots;
    }
}