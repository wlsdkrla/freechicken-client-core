using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDrop : MonoBehaviour
{
    
    public GameObject[] prefab;
    public BoxCollider area;
    public int cnt = 10;
    GameObject go;
    
    bool isEggFalltrue;
    
    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();

       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isEggFalltrue)
        {
            for (int i = 0; i < cnt; i++)
            {
                Spawn();
            }
        }
        isEggFalltrue = false;
       
    }
    void Spawn()
    {
        Vector3 pos = GetRandomPos();

        int selection = Random.Range(0, prefab.Length);
        GameObject go = prefab[selection];
        //GameObject go = prefab;
        GameObject instance = Instantiate(go, pos, Quaternion.identity);
        float random = Random.Range(3.5f, 5f);
        Destroy(instance, random);
       
    }

    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePos.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        Vector3 pos = area.center + spawnPos;
        return pos;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("��������");
            isEggFalltrue = true;
            //slideBelt.Speed = 0.3f;

        }      
    }
}
