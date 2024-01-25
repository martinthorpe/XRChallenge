using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

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

        string path = Application.dataPath + "/Map.txt";
        List<string> map = File.ReadAllLines(path).ToList();
        if (!MapScript.CheckMapIsValid(map))
        {
            map.Clear();
            map = MapScript.ReturnDefaultMapTemplate();
        }
        map = MapScript.CheckMapIsFull(map, "0123456".ToArray());
        m_MapManagerScript.InIt(map, m_Sprites);
        m_MapManagerScript.AddingGridSlots += ConnectGridSLotConnections;
        m_MapManagerScript.RemovingGridSlots += DisconnectGridSLotConnections;

        ConnectGridSLotConnections(m_MapManagerScript.GetAllGridSlots());
    }

    private void SetInHandItem(int value)
    {
        m_iInHandItem = value;
    }

    private void OnDisable()
    {
        m_MapManagerScript.AddingGridSlots -= ConnectGridSLotConnections;
        m_MapManagerScript.RemovingGridSlots -= DisconnectGridSLotConnections;

        for (int i = 0; i < m_ListOfItemChoiceButton.Count; i++)
        {
            m_ListOfItemChoiceButton[i].OnClicked -= SetInHandItem;
        }
        m_ListOfEditMapSizeButton[0].OnClicked -= AddColumn;
        m_ListOfEditMapSizeButton[1].OnClicked -= RemoveColumn;
        m_ListOfEditMapSizeButton[2].OnClicked -= AddRow;
        m_ListOfEditMapSizeButton[3].OnClicked -= RemoveRow;

        List<GridSlotScript> listOfGridSlotScript = m_MapManagerScript.GetAllGridSlots();
        foreach (GridSlotScript gss in listOfGridSlotScript)
        {
            gss.OnClicked -= ChangeGridSlotValue;;
        }
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

    private void ConnectGridSLotConnections(List<GridSlotScript> listOfGridSlotScript)
    {
        foreach(GridSlotScript gss in listOfGridSlotScript)
        {
            gss.OnClicked += ChangeGridSlotValue;
        }
    }

    private void DisconnectGridSLotConnections(List<GridSlotScript> listOfGridSlotScript)
    {
        foreach (GridSlotScript gss in listOfGridSlotScript)
        {
            gss.OnClicked -= ChangeGridSlotValue;
            Destroy(gss.gameObject);
        }
    }

    private List<string> MakeMap()
    {
        List<string> map = new List<string>();
        List<GridSlotScript> listOfGridSlotScript = m_MapManagerScript.GetAllGridSlots();
        string line = "";
        int currentRow = listOfGridSlotScript[0].GetYPoint();
        for (int i = 0; i < listOfGridSlotScript.Count; i++)
        {
            if (currentRow < listOfGridSlotScript[i].GetYPoint())
            {
                map.Add(line);
                line = "";
                currentRow = listOfGridSlotScript[i].GetYPoint();
            }
            line = line + listOfGridSlotScript[i].GetItem().ToString();
        }
        map.Add(line);
        return map;
    }

    public void Exit()
    {
        List<string> map = MakeMap();
        if (MapScript.CheckMapIsValid(map))
        {
            string Path = Application.dataPath + "/Map.txt";
            File.WriteAllLines(Path, map);
            SceneManager.LoadScene(0);
        }
    }
}