using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagerScript : MonoBehaviour
{
    private List<RowManagerScript> m_ListOfRowManagers;

    [SerializeField] private Sprite m_FloorSprite;
    [SerializeField] private Sprite m_WallSprite;
    [SerializeField] private Sprite m_StarSprite;
    [SerializeField] private Sprite m_HealthPotionSprite;
    [SerializeField] private Sprite m_HazardSprite;
    [SerializeField] private Sprite m_FinishedAreaSprite;
    [SerializeField] private Sprite m_PlayerStartSprite;
    [SerializeField] private GameObject m_RowPrefab;
    [SerializeField] private GameObject m_GridSlotPrefab;

    public void InIt(List<string> map)
    {
        m_ListOfRowManagers = new List<RowManagerScript>();
        for (int i = 0; i < map.Count; i++)
        {
            m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
            for (int t = 0; t < map[i].Length; t++)
            {
                Sprite sprite = m_FloorSprite;
                switch (map[i][t])
                {
                    case '0':
                        sprite = m_FloorSprite;
                        break;
                    case '1':
                        sprite = m_WallSprite;
                        break;
                    case '2':
                        sprite = m_StarSprite;
                        break;
                    case '3':
                        sprite = m_HealthPotionSprite;
                        break;
                    case '4':
                        sprite = m_HazardSprite;
                        break;
                    case '5':
                        sprite = m_FinishedAreaSprite;
                        break;
                    case '6':
                        sprite = m_PlayerStartSprite;
                        break;
                }
                m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(sprite, int.Parse(map[i][t].ToString()), i, m_GridSlotPrefab);
            }
        }
    }

    public void AddColumn()
    {
        for (int i = 0; i < m_ListOfRowManagers.Count; i++)
        {
            m_ListOfRowManagers[i].AddRowItem(m_FloorSprite, 0, i, m_GridSlotPrefab);
        }
    }

    public void RemoveColumn()
    {
        for (int i = 0; i < m_ListOfRowManagers.Count; i++)
        {
            m_ListOfRowManagers[i].AddRowItem(m_FloorSprite, 0, i, m_GridSlotPrefab);
        }
    }

    public void AddRow(int rowLength)
    {
        m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
        for (int i = 0; i < rowLength; i++)
        {
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(m_FloorSprite, 0, m_ListOfRowManagers.Count - 1, m_GridSlotPrefab);
        }
    }

    public void RemoveRow()
    {
        RowManagerScript rowObject = m_ListOfRowManagers[m_ListOfRowManagers.Count - 1];
        m_ListOfRowManagers.RemoveAt(m_ListOfRowManagers.Count - 1);
        Destroy(rowObject.gameObject);
    }
}