using UnityEngine;

public class CityArea : MonoBehaviour
{
    public float destroyDistance = 50f;
    private CitySceneSpawn spawner;
    private CityScenePlayer player;

    public void Setup(CitySceneSpawn spawner, CityScenePlayer player)
    {
        this.spawner = spawner;
        this.player = player;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = player.transform.position.z - transform.position.z;
        if (distance >= destroyDistance && !spawner.IsStop())
        {
            spawner.SpawnArea();
            gameObject.SetActive(false); // Object Pool로 반환
        }
    }
}
