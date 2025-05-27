using UnityEngine;
using System.IO;

public static class PlayerDataManager
{
    private static readonly string savePath = Application.persistentDataPath + "/playerData.json";
    public static PlayerData Data { get; private set; }

    public static void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Data = new PlayerData();
            Save(); // 기본값으로 저장
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(savePath, json);
    }
    public static void ApplyData(PlayerData data)
    {
        Data = data;
        Save();
    }

    public static bool IsEnglish => Data.language == "en";
}
