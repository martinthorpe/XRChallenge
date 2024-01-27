using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript
{
    /// <summary>
    /// Adds a 1 to the start and end of each row and column of the map list.
    /// </summary>
    /// <returns>The map list.</returns>
    public static List<string> AddOutsideWalls(List<string> map)
    {
        for (int i = 0; i < map.Count; i++)
        {
            map[i] = "1" + map[i] + "1";
        }
        string newY = "";
        for (int x = 0; x < map[0].Length; x++)
        {
            newY = newY + '1';
        }
        map.Insert(0, newY);
        map.Add(newY);
        return map;
    }

    /// <summary>
    /// Removes 1s and addes empty spaces to any wall whihc is unneeded.
    /// This is to remove blocks of walls which may look odd.
    /// </summary>
    /// <returns>The map list.</returns>
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

    /// <summary>
    /// Makes a new string list.
    /// Adds five string values which represent a map.
    /// </summary>
    /// <returns>The string list.</returns>
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

    /// <summary>
    /// Loops through string.
    /// If char isn't in char array valid, turns that char into the first item from the array valid.
    /// </summary>
    /// <returns>The new string value.</returns>
    public static string ChangeUnvalidToFloor(string line, char[] valid)
    {
        string newLine = "";
        for (int i = 0; i < line.Length; i++)
        {
            bool isValid = false;
            foreach (char c in valid)
            {
                if (line[i] == c)
                {
                    isValid = true;
                }
            }
            if (isValid)
            {
                newLine = newLine + line[i];
            }
            else
            {
                newLine = newLine + valid[0];
            }
        }
        return newLine;
    }

    /// <summary>
    /// Checks what is the longest string and holds that value in largestX.
    /// Then makes sure each string holds only valid chars.
    /// Finally extends any string value which isn't the same length as largestX.
    /// </summary>
    /// <returns>The string list.</returns>
    public static List<string> CheckMapIsFull(List<string> map, char[] valid)
    {
        int largestX = 0;
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i].Length > largestX)
            {
                largestX = map[i].Length;
            }
        }
        for (int i = 0; i < map.Count; i++)
        {
            string newX = ChangeUnvalidToFloor(map[i], valid);
            for (int x = 0; x < largestX - map[i].Length; x++)
            {
                newX = newX + valid[1];
            }
            map[i] = newX;
        }
        return map;
    }
}