using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderManagerScript : MonoBehaviour
{
    [SerializeField] private MapManagerScript m_MapManagerScript;

    [SerializeField] private Sprite[] m_Sprites;

    [SerializeField] private Transform m_ItemChoiceButtonPanel;
    [SerializeField] private Transform m_EditMapSizePanel;

    [SerializeField] private ItemChoiceButtonScript m_ItemChoiceButtonPrefab;
    private List<ItemChoiceButtonScript> m_ListOfItemChoiceButton;

    [SerializeField] private EditMapSizeButtonScript m_EditMapSizeButtonScriptPrefab;
    private List<EditMapSizeButtonScript> m_ListOfEditMapSizeButton;

    private int m_iInHandItem;

    private void Start()
    {
        m_ListOfItemChoiceButton = new List<ItemChoiceButtonScript>();
        for (int i = 0; i < m_Sprites.Length; i++)
        {
            m_ListOfItemChoiceButton.Add(Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel));
            m_ListOfItemChoiceButton[m_ListOfItemChoiceButton.Count - 1].InIt(m_Sprites[i], i);
            m_ListOfItemChoiceButton[m_ListOfItemChoiceButton.Count - 1].OnClicked += SetInHandItem;
        }

        m_ListOfEditMapSizeButton = new List<EditMapSizeButtonScript>();
        for (int i = 0; i < 4; i++)
        {
            m_ListOfEditMapSizeButton.Add(Instantiate(m_EditMapSizeButtonScriptPrefab, m_EditMapSizePanel));
        }
        m_ListOfEditMapSizeButton[0].InIt("Add Column");
        m_ListOfEditMapSizeButton[0].OnClicked += AddColumn;
        m_ListOfEditMapSizeButton[1].InIt("Remove Column");
        m_ListOfEditMapSizeButton[1].OnClicked += RemoveColumn;
        m_ListOfEditMapSizeButton[2].InIt("Add Row");
        m_ListOfEditMapSizeButton[2].OnClicked += AddRow;
        m_ListOfEditMapSizeButton[3].InIt("Remove Row");
        m_ListOfEditMapSizeButton[3].OnClicked += RemoveRow;

        m_iInHandItem = -1;
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
        m_MapManagerScript.InIt(map, m_Sprites[0], m_Sprites[1], m_Sprites[2], m_Sprites[3], m_Sprites[4], m_Sprites[5], m_Sprites[6]);
    }

    private void SetInHandItem(int value)
    {
        m_iInHandItem = value;
    }

    private void OnDisable()
    {
        for (int i = 0; i < m_ListOfItemChoiceButton.Count; i++)
        {
            m_ListOfItemChoiceButton[i].OnClicked -= SetInHandItem;
        }
        m_ListOfEditMapSizeButton[0].OnClicked -= AddColumn;
        m_ListOfEditMapSizeButton[1].OnClicked -= RemoveColumn;
        m_ListOfEditMapSizeButton[2].OnClicked -= AddRow;
        m_ListOfEditMapSizeButton[3].OnClicked -= RemoveRow;
    }

    private void ChangeGridSlotValue(GridSlotScript gridSlot)
    {
        switch (m_iInHandItem)
        {
            case 0:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 1:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 2:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 3:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 4:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 5:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
            case 6:
                gridSlot.ChangeItem(m_Sprites[m_iInHandItem], m_iInHandItem);
                break;
        }
    }

    private void AddColumn()
    {
        m_MapManagerScript.AddColumn(m_Sprites[0]);
    }

    private void RemoveColumn()
    {
        m_MapManagerScript.RemoveColumn();
    }

    private void AddRow()
    {
        m_MapManagerScript.AddRow(m_Sprites[0]);
    }

    private void RemoveRow()
    {
        m_MapManagerScript.RemoveRow();
    }
}