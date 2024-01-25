using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class Main_Menu_Manager_Script : MonoBehaviour
{
    [SerializeField] private UI_Text_Script m_BestTime;
    [SerializeField] private UI_Text_Script m_BestScore;

    private void Awake()
    {
        string path = Application.dataPath + "/Score.txt";
        List<string> fileLines = File.ReadAllLines(path).ToList();
        string bestTime = "N/A";
        string bestScore = "N/A";
        if (fileLines.Count > 2)
        {
            bestTime = fileLines[0];
            bestScore = fileLines[1];
        }
        if (m_BestTime != null)
        {
            m_BestTime.InIt("BEST TIME: " + bestTime);
        }
        if (m_BestScore != null)
        {
            m_BestScore.InIt("BEST SCORE: " + bestScore);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EditMap()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
       Application.Quit();
    }
}