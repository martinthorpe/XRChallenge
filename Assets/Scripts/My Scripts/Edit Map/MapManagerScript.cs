using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class MapManagerScript : MonoBehaviour
{
    public event Action<List<GridSlotScript>> AddingGridSlots;
    public event Action<List<GridSlotScript>> RemovingGridSlots;
    private int m_IRowLength;

    [Header("References")]
    [SerializeField] private GameObject m_RowPrefab;
    [SerializeField] private GameObject m_GridSlotPrefab;
    private List<RowManagerScript> m_ListOfRowManagers;

    /// <summary>
    /// Resets values.
    /// Instantiates and adds row obejcts to row script to list.
    /// Then passes map values to the correct row.
	/// </summary>
    public void InIt(List<string> map, Sprite[] sprites)
    {
        AddingGridSlots = null;
        RemovingGridSlots = null;
        m_ListOfRowManagers = new List<RowManagerScript>();
        m_IRowLength = map[0].Length;
        for (int i = 0; i < map.Count; i++)
        {
            m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
            for (int t = 0; t < map[i].Length; t++)
            {
                int index = 0;
                if (int.Parse(map[i][t].ToString()) <= sprites.Length)
                {
                    index = int.Parse(map[i][t].ToString());
                }
                m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(sprites[index], int.Parse(map[i][t].ToString()), i, m_GridSlotPrefab);
            }
        }
    }

    /// <summary>
	/// Makes a new grid slot script list.
    /// This is used to hold the new grid slot script being added.
    /// Then it is passed through an event.
	/// </summary>
    public void AddColumn(Sprite floorSprite)
    {
        List<GridSlotScript> listOfGridSlotScript = new List<GridSlotScript>();
        for (int i = 0; i < m_ListOfRowManagers.Count; i++)
        {
            listOfGridSlotScript.Add(m_ListOfRowManagers[i].AddRowItem(floorSprite, 0, i, m_GridSlotPrefab));
        }
        m_IRowLength++;
        AddingGridSlots?.Invoke(listOfGridSlotScript);
    }

    /// <summary>
    /// Makes a new grid slot script list.
    /// This is used to hold all the grid slot script being removed.
    /// Then it is passed through an event.
    /// </summary>
    public void RemoveColumn()
    {
        List<GridSlotScript> listOfGridSlotScript = new List<GridSlotScript>();
        if (m_IRowLength > 1)
        {
            for (int i = 0; i < m_ListOfRowManagers.Count; i++)
            {
                listOfGridSlotScript.Add(m_ListOfRowManagers[i].RemoveRowItem());
            }
            m_IRowLength--;
        }
        RemovingGridSlots?.Invoke(listOfGridSlotScript);
    }

    /// <summary>
    /// Makes a new grid slot script list.
    /// A new row manager is added to the row list.
    /// Then grid slots are instantiate and added to that row manager and the new grid slot script list.
    /// Then that list is passed through an event.
    /// </summary>
    public void AddRow(Sprite floorSprite)
    {
        List<GridSlotScript> listOfGridSlotScript = new List<GridSlotScript>();
        m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
        m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
        for (int i = 0; i < m_IRowLength; i++)
        {
            listOfGridSlotScript.Add(m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(floorSprite, 0, m_ListOfRowManagers.Count - 1, m_GridSlotPrefab));
        }
        AddingGridSlots?.Invoke(listOfGridSlotScript);
    }

    /// <summary>
    /// Makes a new grid slot script list.
    /// All the grid slot script in the final row manager is added to that list.
    /// Then that final row manager is removed from its list.
    /// Finally the new grid slot script list is passed through an event.
    /// </summary>
    public void RemoveRow()
    {
        List<GridSlotScript> listOfGridSlotScript = new List<GridSlotScript>();
        if (m_ListOfRowManagers.Count > 1)
        {
            RowManagerScript rowObject = m_ListOfRowManagers[m_ListOfRowManagers.Count - 1];
            while (0 < m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].GetGridSlotList().Count)
            {
                listOfGridSlotScript.Add(m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].RemoveRowItem());
                listOfGridSlotScript[listOfGridSlotScript.Count - 1].gameObject.transform.parent = null;
            }
            m_ListOfRowManagers.RemoveAt(m_ListOfRowManagers.Count - 1);
            Destroy(rowObject.gameObject);
        }
        RemovingGridSlots?.Invoke(listOfGridSlotScript);
    }

    /// <summary>
    /// Makes a new grid slot script list.
    /// Goes through all row managers getting their grid slot scripts adding it to that new list.
    /// </summary>
    /// <returns>The list of grid slot scripts.</returns>
    public List<GridSlotScript> GetAllGridSlots()
    {
        List<GridSlotScript> listOfGridSlotScript = new List<GridSlotScript>();
        foreach (RowManagerScript rms in m_ListOfRowManagers)
        {
            List<GridSlotScript> tempList = rms.GetGridSlotList();
            for (int i = 0; i < tempList.Count; i++)
            {
                listOfGridSlotScript.Add(tempList[i]);
            }
        }
        return listOfGridSlotScript;
    }
}