using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private GameObject m_GOTimerUI;
    [SerializeField] private GameObject m_GOStarPickup;
    [SerializeField] private Transform m_tStarSpawn;
    [SerializeField] private List<Pickup> m_StarList;
    private int m_iStarCount;

    private void OnEnable()
    {
        m_StarList.Add(Instantiate(m_GOStarPickup, m_tStarSpawn).GetComponent<Pickup>());
    }

    private void OnDisable()
    {
        m_StarList[0].OnPickUp -= PickedUpStar;
    }

    private void Awake()
    {
        m_StarList = new List<Pickup>();
        m_iStarCount = m_StarList.Count;
    }

    private void Update()
    {

    }

    private void Start()
    {
        BuildMap();
    }

    private void UpdateTimer()
    {

    }

    private void BuildMap()
    {
        //m_StarList.Add(Instantiate(m_GOStarPickup, m_tStarSpawn).GetComponent<Pickup>());
        m_StarList[0].OnPickUp += PickedUpStar;
        print("added");
    }

    private void SpawnPlayer()
    {

    }

    private void PickedUpStar(Pickup up)
    {
        m_iStarCount--;
        print(m_iStarCount);
    }

    public void EnteredFinishedArea()
    {

    }
}