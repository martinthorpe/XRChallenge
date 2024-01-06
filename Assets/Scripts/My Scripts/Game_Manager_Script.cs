using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private GameObject m_GOTimerUI;
    [SerializeField] private Score_UI_Script m_GOScoreUI;
    [SerializeField] private GameObject m_GOPlayerPrefab;
    [SerializeField] private GameObject m_GOStarPickupPrefab;
    [SerializeField] private Transform m_tPlayerSpawn;
    [SerializeField] private Transform m_tStarSpawn;
    private Player_Controller_Script m_GOPlayerCharacter;
    private List<Pickup> m_StarList;
    private bool m_bHasSetUpLinks;
    private int m_iStarCount;
    private int m_iPlayerScore;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        m_StarList[0].OnPickUp -= PickedUpStar;
    }

    private void Awake()
    {
        m_StarList = new List<Pickup>();
        m_bHasSetUpLinks = false;
        if (m_GOScoreUI != null)
        {
            m_GOScoreUI.InIt();
        }
    }

    private void Update()
    {
        if (!m_bHasSetUpLinks)
        {
            SetUpLinks();
            m_bHasSetUpLinks = true;
        }
    }

    private void Start()
    {
        BuildMap();
        m_iStarCount = m_StarList.Count;
    }

    private void UpdateTimer()
    {

    }

    private void SetUpLinks()
    {
        m_StarList[0].OnPickUp += PickedUpStar;
    }

    private void BuildMap()
    {
        m_StarList.Add(Instantiate(m_GOStarPickupPrefab, m_tStarSpawn).GetComponent<Pickup>());
        m_StarList[0].gameObject.transform.parent = null;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        m_GOPlayerCharacter = Instantiate(m_GOPlayerPrefab, m_tPlayerSpawn).GetComponent<Player_Controller_Script>();
        m_GOPlayerCharacter.gameObject.transform.parent = null;
        m_GOPlayerCharacter.InIt();
    }

    private void PickedUpStar(Pickup up)
    {
        m_iStarCount--;
        if (m_GOScoreUI != null)
        {
            m_iPlayerScore += up.ScoreValue;
            m_GOScoreUI.ChangeText(m_iPlayerScore.ToString());
        }
    }

    public void EnteredFinishedArea()
    {

    }
}