using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class MapManagerScript : MonoBehaviour
{
    private List<RowManagerScript> m_ListOfRowManagers;
    [SerializeField] private GameObject m_RowPrefab;
    [SerializeField] private GameObject m_GridSlotPrefab;
    private int m_IRowLength;

    public void InIt(List<string> map, Sprite floorSprite, Sprite wallSprite, Sprite starSprite, Sprite healthPotionSprite, Sprite hazardSprite, Sprite finishedAreaSprite, Sprite playerStartSprite)
    {
        m_ListOfRowManagers = new List<RowManagerScript>();
        m_IRowLength = map[0].Length;
        for (int i = 0; i < map.Count; i++)
        {
            m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
            for (int t = 0; t < map[i].Length; t++)
            {
                Sprite sprite = null;
                switch (map[i][t])
                {
                    case '0':
                        sprite = floorSprite;
                        break;
                    case '1':
                        sprite = wallSprite;
                        break;
                    case '2':
                        sprite = starSprite;
                        break;
                    case '3':
                        sprite = healthPotionSprite;
                        break;
                    case '4':
                        sprite = hazardSprite;
                        break;
                    case '5':
                        sprite = finishedAreaSprite;
                        break;
                    case '6':
                        sprite = playerStartSprite;
                        break;
                }
                m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(sprite, int.Parse(map[i][t].ToString()), i, m_GridSlotPrefab);
            }
        }
    }

    public void AddColumn(Sprite floorSprite)
    {
        for (int i = 0; i < m_ListOfRowManagers.Count; i++)
        {
            m_ListOfRowManagers[i].AddRowItem(floorSprite, 0, i, m_GridSlotPrefab);
        }
        m_IRowLength++;
    }

    public void RemoveColumn()
    {
        if (m_IRowLength > 1)
        {
            for (int i = 0; i < m_ListOfRowManagers.Count; i++)
            {
                m_ListOfRowManagers[i].RemoveRowItem();
            }
            m_IRowLength--;
        }
    }

    public void AddRow(Sprite floorSprite)
    {
        m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
        m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
        for (int i = 0; i < m_IRowLength; i++)
        {
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(floorSprite, 0, m_ListOfRowManagers.Count - 1, m_GridSlotPrefab);
        }
    }

    public void RemoveRow()
    {
        if (m_ListOfRowManagers.Count > 1)
        {
            RowManagerScript rowObject = m_ListOfRowManagers[m_ListOfRowManagers.Count - 1];
            m_ListOfRowManagers.RemoveAt(m_ListOfRowManagers.Count - 1);
            Destroy(rowObject.gameObject);
        }
    }
}