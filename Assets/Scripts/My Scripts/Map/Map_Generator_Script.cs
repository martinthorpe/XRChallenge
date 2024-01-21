using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Map_Generator_Script : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject m_GOPlayerPrefab;
    [SerializeField] private GameObject m_GOStarPickupPrefab;
    [SerializeField] private GameObject m_GOFinishedAreaPrefab;
    [SerializeField] private GameObject m_GOWallPrefab;
    [SerializeField] private GameObject m_GOFloorPrefab;
    [SerializeField] private GameObject m_GOHealthPotionPrefab;
    [SerializeField] private GameObject m_GOHazardPrefab;
    [SerializeField] private Vector3 m_vPlayerSpawn;

    /// <summary>
    /// Checks if the map is valid. If it isn't valid then it returns the default map.
    /// Funs AddBorders and RemoveUnneededWalls functions to make sure the map is safe to player and cleans it up.
    /// For each element it checks what the number is and spawns a certain prefab.
    /// Adding them to a list if they're either; star pickup, finished area, or player spawn.
    /// On Each row or column counting and stopping if incase it passes the maps physical limits.
    /// </summary>
    /// <returns>The list of game objects in the world.</returns>
    public List<GameObject> BuildMap(List<string> map)
    {
        List<GameObject> listOfGameObejcts = new List<GameObject>();
        if (!CheckMapIsValid(map))
        {
            return SpawnDefaultMap();
        }
        map = AddBorders(map);
        map = RemoveUnneededWalls(map);
        for (int i = 0; i < map.Count; i++)
        {
            for (int x = 0; x < map[i].Length; x++)
            {
                Vector3 pos = new Vector3(i, 0.5f, x);
                switch (map[i][x])
                {
                    case '0':
                        continue;
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
                SpawnObject(m_GOFloorPrefab, new Vector3(pos[0], 0.0f, pos[2]));
            }
        }
        return listOfGameObejcts;
    }

    private List<string> AddBorders(List<string> map)
    {
        //get largest x
        int largestX = 0;
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i].Length > largestX)
            {
                largestX = map[i].Length;
            }
        }
        //extend x
        for (int i = 0; i < map.Count; i++)
        {
            string newX = "1";
            foreach (char c in map[i])
            {
                newX = newX + c;
            }
            for (int x = 0; x < largestX - map[i].Length; x++)
            {
                newX = newX + '1';
            }
            newX = newX + '1';
            map[i] = newX;
        }
        //extend y
        string newY = "";
        for (int x = 0; x < largestX + 2; x++)
        {
            newY = newY + '1';
        }
        map.Insert(0, newY);
        map.Add(newY);
        return map;
    }

    private List<string> RemoveUnneededWalls(List<string> map)
    {
        for (int i = 0; i < map.Count; i++)
        {
            for (int x = 0; x < map[i].Length; x++)
            {
                if (map[i][x] == '1' &&
                    ((i + 1 == map.Count) || (map[i + 1][x] == '1' || map[i + 1][x] == ' ')) &&
                    ((i - 1 == -1) || (map[i - 1][x] == '1' || map[i - 1][x] == ' ')) &&
                    ((x + 1 == map[i].Length) || (map[i][x + 1] == '1' || map[i][x + 1] == ' ')) &&
                    ((x - 1 == -1) || (map[i][x - 1] == '1' || map[i][x - 1] == ' ')))
                {
                    string newString = "";
                    for (int t = 0; t < map[i].Length; t++)
                    {
                        if (t == x)
                        {
                            newString = newString + ' ';
                        }
                        else
                        {
                            newString = newString + map[i][t];
                        }
                    }
                    map[i] = newString;
                }
            }
        }
        return map;
    }

    /// <summary>
    /// Checks if the list it was passed contains at least 5 stars and only one player spawn and one finished area.
    /// </summary>
    /// <returns>True if the map is valid, and false if it isn't.</returns>
    private bool CheckMapIsValid(List<string> map)
    {
        int starCount = 0;
        bool hasPlayerSpawn = false;
        bool hasFinishArea = false;
        for (int i = 0; i < map.Count; i++)
        {
            for (int x = 0; x < map[i].Length; x++)
            {
                switch (map[i][x])
                {
                    case '2':
                        starCount++;
                        break;
                    case '5':
                        if (hasFinishArea)
                        {
                            return false;
                        }
                        hasFinishArea = true;
                        break;
                    case '6':
                        if (hasPlayerSpawn)
                        {
                            return false;
                        }
                        hasPlayerSpawn = true;
                        break;
                }
            }
        }
        if (starCount < 5 || !hasPlayerSpawn || !hasFinishArea)
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