using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private GameObject m_GOTimerUI;
    [SerializeField] private Score_UI_Script m_GOScoreUI;
    [SerializeField] private GameObject m_GOStarPickup;
    [SerializeField] private Transform m_tStarSpawn;
    [SerializeField] private List<Pickup> m_StarList;
    private bool m_bHasSetUpLinks;
    private int m_iStarCount;

    private void OnEnable()
    {
        //m_StarList.Add(Instantiate(m_GOStarPickup, m_tStarSpawn).GetComponent<Pickup>());
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
        m_StarList.Add(Instantiate(m_GOStarPickup, m_tStarSpawn).GetComponent<Pickup>());
    }

    private void SpawnPlayer()
    {

    }

    private void PickedUpStar(Pickup up)
    {
        m_iStarCount--;
        print(m_iStarCount);
        if (m_GOScoreUI != null)
        {
            m_GOScoreUI.ChangeText(up.ScoreValue.ToString());
        }
    }

    public void EnteredFinishedArea()
    {

    }
}