using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Map_Generator_Script))]
public class Game_Manager_Script : MonoBehaviour
{
    private List<Pickup> m_StarList;
    private bool m_bHasSetUpLinks;
    private int m_iStarCount;
    private int m_iPlayerScore;
    private float m_fTimer;
    private int[] m_aMapArray;

    [Header("References")]
    [SerializeField] private UI_Text_Script m_GOTimerUI;
    [SerializeField] private UI_Text_Script m_GOScoreUI;
    private Map_Generator_Script m_Map_Generator;
    private Player_Manager_Script m_GOPlayerCharacter;
    private Finished_Area_Script m_GOFinishedArea;
    private PlayerInput m_Input;

    /// <summary>
    /// Removes the connection to the Pickup, Finished Area and Player Character Script events to functions in this script.
    /// Removes the exit button action connection.
    /// </summary>
    private void OnDisable()
    {
        foreach(Pickup pu in m_StarList)
        {
            pu.OnPickUp -= PickedUpStar;
        }
        m_GOFinishedArea.EnteredArea -= EnteredFinishedArea;
        m_GOPlayerCharacter.Death -= SpawnPlayer;
        m_Input.currentActionMap.FindAction("Exit").performed -= Handle_ExitPerformed;
    }

    /// <summary>
    /// Gets the Map Generator Script.
    /// Sets up Star List, sets HasSetUpLinks to false, and Timer to 0.0.
    /// Sets up the exit button action connection.
    /// If TimerUI and ScoreUI aren't null, then call both their InIt functions.
    /// </summary>
    private void Awake()
    {
        m_Map_Generator = GetComponent<Map_Generator_Script>();
        m_Input = GetComponent<PlayerInput>();
        m_Input.currentActionMap.FindAction("Exit").performed += Handle_ExitPerformed;
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

    /// <summary>
    /// If HasSetUpLinks is false; then call SetUpLinks function and set HasSetUpLinks to false.
    /// Calls UpdateTimer function.
    /// </summary>
    private void Update()
    {
        if (!m_bHasSetUpLinks)
        {
            SetUpStarLinks();
            m_bHasSetUpLinks = true;
        }
        UpdateTimer();
    }

    /// <summary>
    /// Calls SetUpLevel function.
    /// </summary>
    private void Start()
    {
        SetUpLevel();
    }

    /// <summary>
    /// Gets the map layout from the Map Text Document.
    /// Calls Map Generators BuildMap function and returns with objects in the game world within a list.
    /// Looping through that list checks if any has one of these scripts to figure out what it is.
    /// If it is a Star Pickup; adds it to StarList.
    /// If it is the Player, sets it to Player Character, calls its InIt function and assigns it Death event to SpawnPlayer function.
    /// If it is the Finished Area, sets it to Finished Area, calls its InIt function and assigns it EnteredArea event to EnteredFinishedArea function.
    /// Sets StarCount to m_StarLists count.
    /// </summary>
    private void SetUpLevel()
    {
        string path = Application.dataPath + "/Map.txt";
        List<GameObject> listOfGameObjects = m_Map_Generator.BuildMap(File.ReadAllLines(path).ToList());
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
        m_iStarCount = m_StarList.Count;
    }

    /// <summary>
    /// Increases the Timer.
    /// If Timer UI isn't null, then calls its ChangeText function passing the Timer value.
    /// </summary>
    private void UpdateTimer()
    {
        m_fTimer += Time.deltaTime;
        if (m_GOTimerUI != null)
        {
            m_GOTimerUI.ChangeText("TIMER: " + ((int)m_fTimer).ToString());
        }
    }

    /// <summary>
    /// Assigns the connections of the Pickup Script event to the function PickedUpStar.
    /// </summary>
    private void SetUpStarLinks()
    {
        foreach (Pickup pu in m_StarList)
        {
            pu.OnPickUp += PickedUpStar;
        }
    }

    /// <summary>
    /// Reduces Star Count.
    /// If Star Count is 0 or below then calls Finished Area function SetEnterable passing true.
    /// Increases Player Score with the picked up star score value.
    /// If Score UI isn't null then updates it to the new Player Score.
    /// </summary>
    private void PickedUpStar(Pickup up)
    {
        m_iStarCount--;
        if (m_iStarCount <= 0)
        {
            m_GOFinishedArea.SetEnterable(true);
        }
        m_iPlayerScore += up.ScoreValue;
        if (m_GOScoreUI != null)
        {
            m_GOScoreUI.ChangeText("SCORE: " + m_iPlayerScore.ToString());
        }
    }

    /// <summary>
    /// If Player Character isn't null then destroys it.
    /// Spawns the Player and sets it to Player Character, calls its InIt function and assigns it Death event to SpawnPlayer function.
    /// </summary>
    private void SpawnPlayer()
    {
        if (m_GOPlayerCharacter != null)
        {
            Destroy(m_GOPlayerCharacter.gameObject);
        }
        m_GOPlayerCharacter = (m_Map_Generator.SpawnPlayer()).GetComponent<Player_Manager_Script>();
        m_GOPlayerCharacter.InIt();
        m_GOPlayerCharacter.Death += SpawnPlayer;
    }

    /// <summary>
    /// Uploads the timer value if it is lower than the one already in the text document.
    /// Uploads the score value if it is higher than the one already in the text document.
    /// Loads Scene 0, the main menu.
    /// </summary>
    private void EnteredFinishedArea()
    {
        string Path = Application.dataPath + "/Score.txt";
        List<string> fileLines = File.ReadAllLines(Path).ToList();
        if (fileLines.Count < 2)
        {
            fileLines.Add(((int)m_fTimer).ToString());
            fileLines.Add(((int)m_iPlayerScore).ToString());
            File.WriteAllLines(Path, fileLines);
        }
        else
        {
            if (int.Parse(fileLines[0]) > (int)m_fTimer || int.Parse(fileLines[0]) == 0)
            {
                fileLines[0] = ((int)m_fTimer).ToString();
            }
            if (int.Parse(fileLines[1]) < m_iPlayerScore)
            {
                fileLines[1] = m_iPlayerScore.ToString();
            }
            File.WriteAllLines(Path, fileLines);
        }
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Loads Scene 0, the main menu.
    /// </summary>
    private void Handle_ExitPerformed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
    }
}