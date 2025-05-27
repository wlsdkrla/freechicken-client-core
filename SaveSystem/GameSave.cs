// SaveSystem/GameSave.cs
using System.IO;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public static int EasyLevel = 0;
    public static int HardLevel = 0;

    public static void SaveProgress(bool isEasy)
    {
        PlayerData data = new PlayerData
        {
            LevelChk = isEasy ? EasyLevel : HardLevel,
            isEng = PlayerDataManager.IsEnglish,
            isStartEnd = PlayerDataManager.Data.isStartEnd,
            isVibrationOn = PlayerDataManager.Data.isVibrationOn,
            language = PlayerDataManager.Data.language
        };

        string fileName = isEasy ? "PlayerData_Easy.json" : "PlayerData_Hard.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
    }

    public static void LoadProgress(bool isEasy)
    {
        string fileName = isEasy ? "PlayerData_Easy.json" : "PlayerData_Hard.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
            if (data.LevelChk >= (isEasy ? EasyLevel : HardLevel))
            {
                if (isEasy) EasyLevel = data.LevelChk;
                else HardLevel = data.LevelChk;
            }
            PlayerDataManager.ApplyData(data);
        }
    }
}
