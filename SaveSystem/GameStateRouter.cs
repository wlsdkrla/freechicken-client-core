using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateRouter : MonoBehaviour
{
    public AudioSource ClickSound;

    public void LoadSceneByLevel(string scenePrefix, int targetLevel, bool isEasy)
    {
        if (isEasy) GameSave.EasyLevel = targetLevel;
        else GameSave.HardLevel = targetLevel;

        GameSave.SaveProgress(isEasy);
        SceneManager.LoadScene(scenePrefix + targetLevel);
    }
}
