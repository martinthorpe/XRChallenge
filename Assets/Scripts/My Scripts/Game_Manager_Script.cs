using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;


[RequireComponent(typeof(Map_Generator_Script))]
public class Game_Manager_Script : MonoBehaviour
{
    [SerializeField] private UI_Text_Script m_GOTimerUI;
    [SerializeField] private UI_Text_Script m_GOScoreUI;
    private Map_Generator_Script m_Map_Generator;
    private Player_Manager_Script m_GOPlayerCharacter;
    private Finished_Area_Script m_GOFinishedArea;
    private List<Pickup> m_StarList;
    private bool m_bHasSetUpLinks;
    private int m_iStarCount;
    private int m_iPlayerScore;
    private float m_fTimer;
    private int[] m_aMapArray;

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
        m_Map_Generator = GetComponent<Map_Generator_Script>();
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
        SetUpLevel();
        m_iStarCount = m_StarList.Count;
    }

    private void SetUpLevel()
    {
        List<GameObject> listOfGameObjects = m_Map_Generator.BuildMap();
        for (int i = 0; i < listOfGameObjects.Count; i++)
        {
            if (listOfGameObjects[i].TryGetComponent<Pickup>(out Pickup star))
            {
                m_StarList.Add(star.GetComponent<Pickup>());
            }
            else if (listOfGameObjects[i].TryGetComponent<Player_Manager_Script>(out Player_Manager_Script player))
            {
                m_GOPlayerCharacter = player.GetComponent<Player_Manager_Script>();
                m_GOPlayerCharacter.InIt();
                m_GOPlayerCharacter.Death += SpawnPlayer;
            }
            else if (listOfGameObjects[i].TryGetComponent<Finished_Area_Script>(out Finished_Area_Script finishedArea))
            {
                m_GOFinishedArea = finishedArea.GetComponent<Finished_Area_Script>();
                m_GOFinishedArea.InIt(false);
                m_GOFinishedArea.EnteredArea += EnteredFinishedArea;
            }
        }
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

    private void SpawnPlayer()
    {
        if (m_GOPlayerCharacter != null)
        {
            Destroy(m_GOPlayerCharacter.gameObject);
        }
    }

    private void EnteredFinishedArea()
    {
        SceneManager.LoadScene(0);
    }
}