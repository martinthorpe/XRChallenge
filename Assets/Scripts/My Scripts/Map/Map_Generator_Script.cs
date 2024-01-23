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
        if (!MapScript.CheckMapIsValid(map))
        {
            map.Clear();
            map = MapScript.ReturnDefaultMapTemplate();
        }
        map = MapScript.AddBorders(map);
        map = MapScript.RemoveUnneededWalls(map);
        for (int i = 0; i < map.Count; i++)
        {
            for (int x = 0; x < map[i].Length; x++)
            {
                Vector3 pos = new Vector3(i, 0.5f, x);
                switch (map[i][x])
                {
                    case '0':
                        break;
                    case '1':
                        SpawnObject(m_GOWallPrefab, pos);
                        continue;
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
                    default:
                        continue;
                }
                SpawnObject(m_GOFloorPrefab, new Vector3(pos[0], 0.0f, pos[2]));
            }
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