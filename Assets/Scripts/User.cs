using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class User
{
    public static string Nickname;
    public static int Money;
    public static List<Level> Levels = new List<Level>();
    public static int SelectedLvl;
    public static string savePath;

    public static void SaveToFile()
    {
        UserStruct user = new UserStruct
        {
            nickname = Nickname,
            money = Money,
            levels = Levels
        };
        string json = JsonUtility.ToJson(user, true);
        try
        {
            File.WriteAllText(savePath, json);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }
    public static bool LoadFromFile()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("File not found");
            return false;
        }
        try
        {
            string json = File.ReadAllText(savePath);
            UserStruct userFromJson = JsonUtility.FromJson<UserStruct>(json);
            Nickname = userFromJson.nickname;
            Money = userFromJson.money;
            Levels = userFromJson.levels;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        return true;
    }
}
[System.Serializable]
public struct UserStruct
{
    public string nickname;
    public int money;
    public List<Level> levels;
};
