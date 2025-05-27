using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private bool isEasy = true;

    void Awake()
    {
        PlayerDataManager.Load();
        GameSave.LoadProgress(isEasy);
    }
}
