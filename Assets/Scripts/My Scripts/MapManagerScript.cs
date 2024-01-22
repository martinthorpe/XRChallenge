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
    private int m_IRowLength;

    public void InIt(List<string> map)
    {
        m_ListOfRowManagers = new List<RowManagerScript>();
        m_IRowLength = map[0].Length;
        for (int i = 0; i < map.Count; i++)
        {
            m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
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

    void Start()
    {
        string lineOne = "111111";
        string lineTwo = "106201";
        string lineThree = "120041";
        string lineFour = "130501";
        string linefi = "130501";
        string lineser = "130501";
        string lineseve = "111111";
        List<string> map = new List<string>();
        map.Add(lineOne);
        map.Add(lineTwo);
        map.Add(lineThree);
        map.Add(lineFour);
        map.Add(linefi);
        map.Add(lineser);
        map.Add(lineseve);
        InIt(map);
    }

    public void AddColumn()
    {
        for (int i = 0; i < m_ListOfRowManagers.Count; i++)
        {
            m_ListOfRowManagers[i].AddRowItem(m_FloorSprite, 0, i, m_GridSlotPrefab);
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

    public void AddRow()
    {
        m_ListOfRowManagers.Add(Instantiate(m_RowPrefab, gameObject.transform).GetComponent<RowManagerScript>());
        m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].InIt();
        for (int i = 0; i < m_IRowLength; i++)
        {
            m_ListOfRowManagers[m_ListOfRowManagers.Count - 1].AddRowItem(m_FloorSprite, 0, m_ListOfRowManagers.Count - 1, m_GridSlotPrefab);
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