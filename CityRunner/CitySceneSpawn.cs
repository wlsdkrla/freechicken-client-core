using UnityEngine;

public class CitySceneSpawn : MonoBehaviour
{
    public GameObject[] areaPrefabs;
    public Material[] skyboxes;
    public float zDistance;
    public GameObject lastMap;
    public ObjectPool areaPool;

    private int areaIndex = 0;
    private int lastSkyboxIndex = -1;
    private bool isStop = false;
    private bool isFinish = false;
    private CityScenePlayer player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CityScenePlayer>();

        for (int i = 0; i < 2; i++)
        {
            SpawnArea(i != 0);
        }
    }

    private void Update()
    {
        if (areaIndex == 6 && !isFinish)
        {
            isStop = true;
            SpawnLastMap();
        }
    }

    public void SpawnArea(bool isRandom = true)
    {
        GameObject clone = areaPool.GetObjectFromPool(Vector3.zero, Quaternion.identity);
        if (clone == null) return;

        if (isRandom)
        {
            int newIndex;
            do { newIndex = Random.Range(0, skyboxes.Length); }
            while (newIndex == lastSkyboxIndex);

            RenderSettings.skybox = skyboxes[newIndex];
            lastSkyboxIndex = newIndex;
        }

        clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
        clone.GetComponent<CityArea>().Setup(this, player);
        areaIndex++;
    }

    void SpawnLastMap()
    {
        isFinish = true;
        Instantiate(lastMap, new Vector3(0, 0, areaIndex * zDistance), Quaternion.identity);
    }

    public bool IsStop() => isStop;
}
