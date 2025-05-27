using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bullet;
    float bulletSpeed;
    float currTime;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        bulletSpeed = Random.Range(20, 50);
        currTime += Time.deltaTime;
       
        if (currTime > 5)
        {

            bullet = Instantiate(this.gameObject);
            
            bullet.transform.position = this.gameObject.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            
            Destroy(bullet, 4);
            currTime = 0;

        }

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Player ���ݹ޴� ��� & ���� �ϳ� --
            Debug.Log("�÷��̾� ���� �޾Ҽ�");
        }
    }
}
