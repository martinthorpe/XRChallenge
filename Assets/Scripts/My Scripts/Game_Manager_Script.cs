using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private UI_Text_Script m_GOTimerUI;
    [SerializeField] private UI_Text_Script m_GOScoreUI;
    [SerializeField] private GameObject m_GOPlayerPrefab;
    [SerializeField] private GameObject m_GOStarPickupPrefab;
    [SerializeField] private Transform m_tPlayerSpawn;
    [SerializeField] private Transform m_tStarSpawn;
    private Player_Manager_Script m_GOPlayerCharacter;
    [SerializeField] private Finished_Area_Script m_GOFinishedArea;
    private List<Pickup> m_StarList;
    private bool m_bHasSetUpLinks;
    private int m_iStarCount;
    private int m_iPlayerScore;
    private float m_fTimer;

    private void OnDisable()
    {
        foreach(Pickup pu in m_StarList)
        {
            pu.OnPickUp -= PickedUpStar;
        }
        m_GOFinishedArea.EnteredArea -= EnteredFinishedArea;
        m_GOPlayerCharacter.Death -= SpawnPlayer;
    }

    private void Awake()
    {
        m_StarList = new List<Pickup>();
        m_bHasSetUpLinks = false;
        m_fTimer = 0.0f;
        if (m_GOTimerUI != null)
        {
            m_GOTimerUI.InIt("TIMER: 00");
        }
        if (m_GOScoreUI != null)
        {
            m_GOScoreUI.InIt("SCORE: 0");
        }
        if (m_GOFinishedArea != null)
        {
            m_GOFinishedArea.InIt(false);
            m_GOFinishedArea.EnteredArea += EnteredFinishedArea;
        }
    }

    private void Update()
    {
        if (!m_bHasSetUpLinks)
        {
            SetUpLinks();
            m_bHasSetUpLinks = true;
        }
        UpdateTimer();
    }

    private void Start()
    {
        BuildMap();
        m_iStarCount = m_StarList.Count;
    }

    private void UpdateTimer()
    {
        m_fTimer += Time.deltaTime;
        m_GOTimerUI.ChangeText("TIMER: " + ((int)m_fTimer).ToString());
    }

    private void SetUpLinks()
    {
        foreach (Pickup pu in m_StarList)
        {
            pu.OnPickUp += PickedUpStar;
        }
    }

    private void BuildMap()
    {
        m_StarList.Add(Instantiate(m_GOStarPickupPrefab, m_tStarSpawn).GetComponent<Pickup>());
        m_StarList[0].gameObject.transform.parent = null;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (m_GOPlayerCharacter != null)
        {
            Destroy(m_GOPlayerCharacter.gameObject);
        }
        m_GOPlayerCharacter = Instantiate(m_GOPlayerPrefab, m_tPlayerSpawn).GetComponent<Player_Manager_Script>();
        m_GOPlayerCharacter.gameObject.transform.parent = null;
        m_GOPlayerCharacter.InIt();
        m_GOPlayerCharacter.Death += SpawnPlayer;
    }

    private void PickedUpStar(Pickup up)
    {
        m_iStarCount--;
        if (m_iStarCount <= 0)
        {
            m_GOFinishedArea.SetEnterable(true);
        }
        if (m_GOScoreUI != null)
        {
            m_iPlayerScore += up.ScoreValue;
            m_GOScoreUI.ChangeText("SCORE: " + m_iPlayerScore.ToString());
        }
    }

    private void EnteredFinishedArea()
    {
        print("LOL");
    }
}