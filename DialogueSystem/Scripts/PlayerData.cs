using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int levelCheckpoint = 0;
    public bool isStartEnd = false;
    public bool isVibrationOn = true;
    public string language = "ko"; // "ko" or "en"
}
