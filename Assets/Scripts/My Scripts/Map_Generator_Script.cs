using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Map_Generator_Script : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int m_iSizeLimit = 10;
    [SerializeField] private GameObject m_GOPlayerPrefab;
    [SerializeField] private GameObject m_GOStarPickupPrefab;
    [SerializeField] private GameObject m_GOFinishedAreaPrefab;
    [SerializeField] private GameObject m_GOWallPrefab;
    [SerializeField] private GameObject m_GOHealthPotionPrefab;
    [SerializeField] private GameObject m_GOHazardPrefab;
    [SerializeField] private Vector3 m_vPlayerSpawn;

    /// <summary>
    /// Gets the map layout from the Map Text Document.
    /// If it isn't valid then returns the default map.
    /// For each element it checks what the number is and spawns a certain prefab.
    /// Adding them to a list if they're either; star pickup, finished area, or player spawn.
    /// On Each row or column counting and stopping if incase it passes the maps physical limits.
    /// </summary>
    /// <returns>The list of game objects in the world.</returns>
    public List<GameObject> BuildMap()
    {
        List<GameObject> listOfGameObejcts = new List<GameObject>();
        string path = Application.dataPath + "/Map.txt";
        List<string> fileLines = File.ReadAllLines(path).ToList();
        if (!CheckMapIsValid(fileLines))
        {
            return SpawnDefaultMap();
        }
        for (int i = 0; i < fileLines.Count; i++)
        {
            if (m_iSizeLimit == i)
            {
                break;
            }
            for (int x = 0; x < fileLines[i].Length; x++)
            {
                if (m_iSizeLimit == x)
                {
                    break;
                }
                Vector3 pos = new Vector3((i - m_iSizeLimit / 2.0f) + 0.5f, 0.5f, (x - m_iSizeLimit / 2.0f) + 0.5f);
                switch (fileLines[i][x])
                {
                    case '1':
                        SpawnObject(m_GOWallPrefab, pos);
                        break;
                    case '2':
                        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, pos));
                        break;
                    case '3':
                        SpawnObject(m_GOHealthPotionPrefab, pos);
                        break;
                    case '4':
                        SpawnObject(m_GOHazardPrefab, pos);
                        break;
                    case '5':
                        listOfGameObejcts.Add(SpawnObject(m_GOFinishedAreaPrefab, pos));
                        break;
                    case '6':
                        m_vPlayerSpawn = pos;
                        listOfGameObejcts.Add(SpawnPlayer());
                        break;
                }
            }
        }
        return listOfGameObejcts;
    }

    /// <summary>
    /// Checks if the list it was passed contains at least 5 stars and only one player spawn and one finished area.
    /// </summary>
    /// <returns>True if the map is valid, and false if it isn't.</returns>
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

    /// <summary>
    /// Spawns the player, finished area, star pickups, and the walls.
    /// </summary>
    /// <returns>The list of objects in the map.</returns>
    private List<GameObject> SpawnDefaultMap()
    {
        List<GameObject> listOfGameObejcts = new List<GameObject>();

        m_vPlayerSpawn = new Vector3(0.0f, 0.5f, 0.0f);
        listOfGameObejcts.Add(SpawnPlayer());
        listOfGameObejcts.Add(SpawnObject(m_GOFinishedAreaPrefab, new Vector3(3.5f, 0.5f, 3.5f)));
        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, new Vector3(-1.5f, 0.5f, -3.5f)));
        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, new Vector3(-2.5f, 0.5f, 3.5f)));
        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, new Vector3(0.5f, 0.5f, 1.5f)));
        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, new Vector3(3.5f, 0.5f, 2.5f)));
        listOfGameObejcts.Add(SpawnObject(m_GOStarPickupPrefab, new Vector3(-2.5f, 0.5f, -3.5f)));
        for (float x = -4.5f; x <= 4.5f; x++)
        {
            SpawnObject(m_GOWallPrefab, new Vector3(x, 0.5f, -4.5f));
        }
        for (float x = -4.5f; x <= 4.5f; x++)
        {
            SpawnObject(m_GOWallPrefab, new Vector3(x, 0.5f, 4.5f));
        }
        for (float y = -4.5f; y <= 4.5f; y++)
        {
            SpawnObject(m_GOWallPrefab, new Vector3(-4.5f, 0.5f, y));
        }
        for (float y = -4.5f; y <= 4.5f; y++)
        {
            SpawnObject(m_GOWallPrefab, new Vector3(4.5f, 0.5f, y));
        }
        return listOfGameObejcts;
    }

    /// <summary>
    /// Spawns the passed prefab.
    /// Sets that objects transforms position to the vector3 value passed.
    /// </summary>
    /// <returns>The object.</returns>
    private GameObject SpawnObject(GameObject prefab, Vector3 pos)
    {
        GameObject go = Instantiate(prefab);
        go.transform.position = pos;
        return go;
    }

    /// <summary>
    /// Calls SpawnObject function passing the Player Prefab and Player Spawn Location.
    /// </summary>
	/// <returns>The Player object.</returns>
    public GameObject SpawnPlayer()
    {
        return SpawnObject(m_GOPlayerPrefab, m_vPlayerSpawn);
    }
}