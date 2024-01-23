using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class RowManagerScript : MonoBehaviour
{
    private List<GridSlotScript> m_ListOfGridSlots;
    public void InIt()
    {
        m_ListOfGridSlots = new List<GridSlotScript>();
    }

    public GridSlotScript AddRowItem(Sprite sprite, int itemNum, int y, GameObject slotGridPrefab)
    {
        m_ListOfGridSlots.Add(Instantiate(slotGridPrefab, gameObject.transform).GetComponent<GridSlotScript>());
        m_ListOfGridSlots[m_ListOfGridSlots.Count - 1].InIt(sprite, itemNum, y);
        return m_ListOfGridSlots[m_ListOfGridSlots.Count - 1];
    }

    public GridSlotScript RemoveRowItem()
    {
        GridSlotScript gridSlotObject = m_ListOfGridSlots[m_ListOfGridSlots.Count - 1];
        m_ListOfGridSlots.RemoveAt(m_ListOfGridSlots.Count - 1);
        return gridSlotObject;
    }

    public List<GridSlotScript> GetGridSlotList()
    {
        return m_ListOfGridSlots;
    }
}