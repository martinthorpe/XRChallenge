using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript
{
    public static List<string> AddBorders(List<string> map)
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

    public static List<string> RemoveUnneededWalls(List<string> map)
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
    public static bool CheckMapIsValid(List<string> map)
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

    public static List<string> ReturnDefaultMapTemplate()
    {
        List<string> map = new List<string>();
        map.Add("600020");
        map.Add("024030");
        map.Add("030200");
        map.Add("024110");
        map.Add("402150");
        return map;
    }

    public static List<string> CheckMapIsFull(List<string> map)
    {
        return map;
    }
}