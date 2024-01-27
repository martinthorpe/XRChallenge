using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class LevelBuilderManagerScript : MonoBehaviour
{
    private int m_iInHandItem;

    [Header("References")]
    [SerializeField] private MapManagerScript m_MapManagerScript;
    [SerializeField] private Sprite[] m_Sprites;
    [SerializeField] private Transform m_ItemChoiceButtonPanel;
    [SerializeField] private Transform m_EditMapSizePanel;
    [SerializeField] private ItemChoiceButtonScript m_ItemChoiceButtonPrefab;
    [SerializeField] private EditMapSizeButtonScript m_EditMapSizeButtonScriptPrefab;
    private List<ItemChoiceButtonScript> m_ListOfItemChoiceButton;
    private List<EditMapSizeButtonScript> m_ListOfEditMapSizeButton;

    /// <summary>
    /// Instantiates and calls their init and sets up their events for all the Item Choice Button Scripts, then stores them in a list.
    /// Instantiates and calls their init and sets up their events for all the Edit Map Size Button Scripts, then stores them in a list.
    /// Gets map from a text document.
    /// Checks if it is valid, which if it isn't sets it to the default map template.
    /// Calls CheckMapIsFull to make sure map has rows of the same length and only valid numbers within, setting the returned value to map.
    /// Then passes the map value to the map manager script as well as setting up its event connections.
    /// Then calls ConnectGridSlotConnections.
    /// </summary>
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
        m_MapManagerScript.AddingGridSlots += ConnectGridSlotConnections;
        m_MapManagerScript.RemovingGridSlots += DisconnectGridSLotConnections;

        ConnectGridSlotConnections(m_MapManagerScript.GetAllGridSlots());
    }

    /// <summary>
    /// Sets InHandItem to passed value.
    /// </summary>
    private void SetInHandItem(int value)
    {
        m_iInHandItem = value;
    }

    /// <summary>
    /// Disconnects all events connected to a function within this class.
    /// </summary>
    private void OnDisable()
    {
        m_MapManagerScript.AddingGridSlots -= ConnectGridSlotConnections;
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

    /// <summary>
    /// Passes Hand Item and the correct sprite for it to the passed grid slot script.
    /// </summary>
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

    /// <summary>
    /// Calls map managers script add column function, passing the floor sprite.
    /// </summary>
    private void AddColumn()
    {
        m_MapManagerScript.AddColumn(m_Sprites[0]);
    }

    /// <summary>
    /// Calls map managers script script remove column function.
    /// </summary>
    private void RemoveColumn()
    {
        m_MapManagerScript.RemoveColumn();
    }

    /// <summary>
    /// Calls map managers script add row function, passing the floor sprite.
    /// </summary>
    private void AddRow()
    {
        m_MapManagerScript.AddRow(m_Sprites[0]);
    }

    /// <summary>
    /// Calls map managers script remove row function.
    /// </summary>
    private void RemoveRow()
    {
        m_MapManagerScript.RemoveRow();
    }

    /// <summary>
    /// Connects the event in the grid slot script to ChangeGridSlotValue function.
    /// </summary>
    private void ConnectGridSlotConnections(List<GridSlotScript> listOfGridSlotScript)
    {
        foreach(GridSlotScript gss in listOfGridSlotScript)
        {
            gss.OnClicked += ChangeGridSlotValue;
        }
    }

    /// <summary>
    /// Disconnects the event in the grid slot script to ChangeGridSlotValue function.
    /// </summary>
    private void DisconnectGridSLotConnections(List<GridSlotScript> listOfGridSlotScript)
    {
        foreach (GridSlotScript gss in listOfGridSlotScript)
        {
            gss.OnClicked -= ChangeGridSlotValue;
            Destroy(gss.gameObject);
        }
    }

    /// <summary>
    /// Gets each grid slot from the scene and passes its item value into a string list.
    /// Each string presenting a row.
    /// </summary>
    /// <returns>The map list.</returns>
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

    /// <summary>
    /// Gets the map from the MakeMap function.
    /// Then uploads it to a text document.
    /// </summary>
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