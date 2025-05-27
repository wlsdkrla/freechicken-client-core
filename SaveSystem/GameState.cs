using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject menuCanvas;
    public AudioSource BGM;
    public GameObject LoadingUI;
    public AudioSource ClickSound;
    public GameStateRouter router;

    private Dictionary<string, (string prefix, int level, bool isEasy)> sceneMap = new Dictionary<string, (string, int, bool)>
    {
        {"Factory_1", ("FactoryScene_", 1, true)},
        {"Factory_2", ("FactoryScene_", 2, true)},
        {"Factory_3", ("FactoryScene_", 3, true)},
        {"Factory_4", ("FactoryScene_", 4, true)},
        {"House_1",   ("HouseScene_", 1, true)},
        {"House_2",   ("HouseScene_", 2, true)},
        {"House_3",   ("HouseScene_", 3, true)},
        {"House_4",   ("HouseScene_", 4, true)},
        {"House_5",   ("HouseScene_", 5, true)},
        {"City",       ("CityScene", 0, true)},
        {"Cave_1",     ("CaveScene_", 1, true)},
        {"Cave_2",     ("CaveScene_", 2, true)},
        {"Cave_3",     ("CaveScene_", 3, true)},
        {"Cave_4",     ("CaveScene_", 4, true)},
        {"Cave_5",     ("CaveScene_", 5, true)}
    };

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !menuCanvas.activeSelf)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Collider2D collider = Physics2D.OverlapPoint(touchPosition);
            if (collider != null && sceneMap.ContainsKey(collider.name))
            {
                ClickSound.Play();
                var (prefix, level, isEasy) = sceneMap[collider.name];
                router.LoadSceneByLevel(prefix, level, isEasy);
            }
        }
    }
}