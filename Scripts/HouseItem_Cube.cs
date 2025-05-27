using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseItem_Cube : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    HouseScenePlayer player;

    void Start()
    {
        explosionParticle.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ť��� �÷��̾� �浹");
            explosionParticle.gameObject.SetActive(true);
            Invoke("RemoveItem", 1f);
            
        }
    }

    void RemoveItem()
    {
        this.gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collision)
    {
        explosionParticle.gameObject.SetActive(false);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            Debug.Log("�÷��̾� ����");
        }
    }
}
