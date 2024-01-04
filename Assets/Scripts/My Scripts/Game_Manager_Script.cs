using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private GameObject m_GOTimerUI;
    [SerializeField] private Pickup m_StarList;// = new List<Pickup>();
    private int m_iStarCount;

    private void OnEnable()
    {
        print("l");
        m_StarList.OnPickUp += PickedUpStar;
    }

    private void OnDisable()
    {
        print("o");
        m_StarList.OnPickUp -= PickedUpStar;
    }

    private void Awake()
    {
        //m_StarList = new List<Pickup>();
        m_iStarCount = 1;// m_StarList.Count;
    }

    private void Update()
    {

    }

    private void UpdateTimer()
    {

    }

    private void BuildMap()
    {

    }

    private void SpawnPlayer()
    {

    }

    private void PickedUpStar(Pickup pu)
    {
        m_iStarCount--;
        print(m_iStarCount);
    }

    public void EnteredFinishedArea()
    {

    }
}