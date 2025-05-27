using UnityEngine;
using System.Collections;

public class DropTrigger : MonoBehaviour
{
    public BoxCollider dropArea;
    public ObjectPool objectPool;
    public int spawnCount = 5;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            hasSpawned = true;
            StartCoroutine(SpawnAll());
        }
    }

    IEnumerator SpawnAll()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = GetRandomPos();
            GameObject obj = objectPool.GetObjectFromPool(pos, Quaternion.identity);
            StartCoroutine(ReturnToPoolAfterDelay(obj, 3f));
            yield return null;
        }
    }

    Vector3 GetRandomPos()
    {
        Vector3 center = dropArea.center + transform.position;
        Vector3 halfSize = dropArea.size * 0.5f;
        float x = Random.Range(-halfSize.x, halfSize.x);
        float y = Random.Range(-halfSize.y, halfSize.y);
        float z = Random.Range(-halfSize.z, halfSize.z);
        return center + new Vector3(x, y, z);
    }

    IEnumerator ReturnToPoolAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnObjectToPool(obj);
    }
}
