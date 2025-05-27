using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBelt : MonoBehaviour
{
    FactoryPlayer player;
    
    public float Speed;
    void Start()
    {
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        Speed = 0.5f;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (!player.isTalk)
        {
            Move();
        }
    }
    void Move()
    {
        if (player.isSlide)
        {
            Speed = 1.2f;
            player.transform.Translate(Vector3.forward * Time.deltaTime * Speed,Space.World);
            
        }
        else if (!player.isSlide)
        {
            Speed = 0;
        }

    }
   
}
