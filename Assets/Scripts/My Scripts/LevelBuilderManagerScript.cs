using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderManagerScript : MonoBehaviour
{
    [SerializeField] private MapManagerScript m_MapManagerScript;

    [SerializeField] private Sprite m_FloorSprite;
    [SerializeField] private Sprite m_WallSprite;
    [SerializeField] private Sprite m_StarSprite;
    [SerializeField] private Sprite m_HealthPotionSprite;
    [SerializeField] private Sprite m_HazardSprite;
    [SerializeField] private Sprite m_FinishedAreaSprite;
    [SerializeField] private Sprite m_PlayerStartSprite;


    [SerializeField] private Transform m_ItemChoiceButtonPanel;

    [SerializeField] private ItemChoiceButtonScript m_ItemChoiceButtonPrefab;

    private int m_iInHandItem;

    private void Start()
    {
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_FloorSprite, 0);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_WallSprite, 1);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_StarSprite, 2);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_HealthPotionSprite, 3);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_HazardSprite, 4);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_FinishedAreaSprite, 5);
        Instantiate(m_ItemChoiceButtonPrefab, m_ItemChoiceButtonPanel).InIt(m_PlayerStartSprite, 6);
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
        m_MapManagerScript.InIt(map, m_FloorSprite, m_WallSprite, m_StarSprite, m_HealthPotionSprite, m_HazardSprite, m_FinishedAreaSprite, m_PlayerStartSprite);
    }

    private void SetInHandItem(int value)
    {
        m_iInHandItem = value;
    }

    private void ChangeGridSlotValue(GridSlotScript gridSlot)
    {
        switch (m_iInHandItem)
        {
            case 0:
                gridSlot.ChangeItem(m_FloorSprite, m_iInHandItem);
                break;
            case 1:
                gridSlot.ChangeItem(m_WallSprite, m_iInHandItem);
                break;
            case 2:
                gridSlot.ChangeItem(m_StarSprite, m_iInHandItem);
                break;
            case 3:
                gridSlot.ChangeItem(m_HealthPotionSprite, m_iInHandItem);
                break;
            case 4:
                gridSlot.ChangeItem(m_HazardSprite, m_iInHandItem);
                break;
            case 5:
                gridSlot.ChangeItem(m_FinishedAreaSprite, m_iInHandItem);
                break;
            case 6:
                gridSlot.ChangeItem(m_PlayerStartSprite, m_iInHandItem);
                break;
        }
    }
}