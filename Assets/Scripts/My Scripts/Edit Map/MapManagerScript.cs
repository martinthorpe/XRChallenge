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
    private List<RowManagerScript> m_ListOfRowManagers;
    [SerializeField] private GameObject m_RowPrefab;
    [SerializeField] private GameObject m_GridSlotPrefab;
    private int m_IRowLength;

    public void InIt(List<string> map, Sprite[] sprites)
    {
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