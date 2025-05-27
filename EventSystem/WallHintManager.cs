using System.Collections;
using UnityEngine;

public class WallHintManager : MonoBehaviour
{
    [Header("Hint Settings")]
    public Material[] hintMaterials;
    public Renderer wallRenderer;
    public GameObject[] eggBoxes;
    private int selectedIndex = -1;

    void Awake()
    {
        SetWallHintOnce();
    }

    void SetWallHintOnce()
    {
        selectedIndex = Random.Range(0, hintMaterials.Length);
        wallRenderer.material = hintMaterials[selectedIndex];

        FactoryManagerController manager = FindObjectOfType<FactoryManagerController>();
        if (manager != null)
        {
            manager.attackBox = eggBoxes[selectedIndex];
        }
    }
}