using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Map_Generator_Script : MonoBehaviour
{
    [SerializeField] private GameObject m_GOPlayerPrefab;
    [SerializeField] private GameObject m_GOStarPickupPrefab;
    [SerializeField] private GameObject m_GOFinishedAreaPrefab;
    [SerializeField] private GameObject m_GOWallPrefab;
    [SerializeField] private GameObject m_GOHealthPotionPrefab;
    [SerializeField] private GameObject m_GOHazardPrefab;
    [SerializeField] private Vector3 m_vPlayerSpawn;


    public List<GameObject> BuildMap()
    {
        List<GameObject> listOfGameObejcts = new List<GameObject>();
        int sizeLimit = 10;
        string path = Application.dataPath + "/Map.txt";
        List<string> fileLines = File.ReadAllLines(path).ToList();
        if (!CheckMapIsValid(fileLines))
        {
            return SpawnDefaultMap();
            
        }
        for (int i = 0; i < fileLines.Count; i++)
        {
            if (sizeLimit == i)
            {
                break;
            }
            for (int x = 0; x < fileLines[i].Length; x++)
            {
                if (sizeLimit == x)
                {
                    break;
                }
                switch (fileLines[i][x])
                {
                    case '1':
                        listOfGameObejcts.Add(Instantiate(m_GOWallPrefab));
                        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        break;
                    case '2':
                        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
                        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        break;
                    case '3':
                        Instantiate(m_GOHealthPotionPrefab).transform.position = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        break;
                    case '4':
                        Instantiate(m_GOHazardPrefab).transform.position = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        break;
                    case '5':
                        listOfGameObejcts.Add(Instantiate(m_GOFinishedAreaPrefab));
                        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        break;
                    case '6':
                        m_vPlayerSpawn = new Vector3((i - 5.0f) + 0.5f, 0.5f, (x - 5.0f) + 0.5f);
                        listOfGameObejcts.Add(SpawnPlayer());
                        break;
                }
            }
        }
        return listOfGameObejcts;
    }

    private bool CheckMapIsValid(List<string> fileLines)
    {
        int starCount = 0;
        int hasPlayerSpawn = 0;
        int hasFinishArea = 0;
        for (int i = 0; i < fileLines.Count; i++)
        {
            for (int x = 0; x < fileLines[i].Length; x++)
            {
                switch (fileLines[i][x])
                {
                    case '2':
                        starCount++;
                        break;
                    case '5':
                        hasFinishArea++;
                        break;
                    case '6':
                        hasPlayerSpawn++;
                        break;
                }
            }
        }
        if (starCount < 5 || hasPlayerSpawn != 1 || hasFinishArea != 1)
        {
            return false;
        }
        return true;
    }

    private List<GameObject> SpawnDefaultMap()
    {
        List<GameObject> listOfGameObejcts = new List<GameObject>();
        m_vPlayerSpawn = new Vector3(0.0f, 0.5f, 0.0f);
        listOfGameObejcts.Add(SpawnPlayer());
        listOfGameObejcts.Add(Instantiate(m_GOFinishedAreaPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(3.5f, 0.5f, 3.5f);
        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(-1.5f, 0.5f, -3.5f);
        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(-2.5f, 0.5f, 3.5f);
        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(0.5f, 0.5f, 1.5f);
        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(3.5f, 0.5f, 2.5f);
        listOfGameObejcts.Add(Instantiate(m_GOStarPickupPrefab));
        listOfGameObejcts[listOfGameObejcts.Count - 1].transform.position = new Vector3(-2.5f, 0.5f, -3.5f);
        for (float x = -4.5f; x <= 4.5f; x++)
        {
            Instantiate(m_GOWallPrefab).transform.position = new Vector3(x, 0.5f, -4.5f);
        }
        for (float x = -4.5f; x <= 4.5f; x++)
        {
            Instantiate(m_GOWallPrefab).transform.position = new Vector3(x, 0.5f, 4.5f);
        }
        for (float y = -4.5f; y <= 4.5f; y++)
        {
            Instantiate(m_GOWallPrefab).transform.position = new Vector3(-4.5f, 0.5f, y);
        }
        for (float y = -4.5f; y <= 4.5f; y++)
        {
            Instantiate(m_GOWallPrefab).transform.position = new Vector3(4.5f, 0.5f, y);
        }
        return listOfGameObejcts;
    }

    public GameObject SpawnPlayer()
    {
        GameObject player = Instantiate(m_GOPlayerPrefab);
        player.transform.position = m_vPlayerSpawn;
        return player;
    }
}