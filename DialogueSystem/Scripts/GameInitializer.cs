using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        PlayerDataManager.Load();
    }
}
