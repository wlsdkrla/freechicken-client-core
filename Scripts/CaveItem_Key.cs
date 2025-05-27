using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaveItem_Key : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pickUpKeyItemText;
    [SerializeField] TextMeshProUGUI nearKeyItemText;
    bool isPickUp;

    CaveScenePlayer player;
    AI_Cave ai;
    public AudioSource getKeySound;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        pickUpKeyItemText.gameObject.SetActive(false);
        nearKeyItemText.gameObject.SetActive(false);
        ai = GameObject.FindGameObjectWithTag("Slide").GetComponent<AI_Cave>();
        this.gameObject.transform.localScale = new Vector3(4f, 4f, 4f);
    }
    void Update()
    {
        PickUp();
        this.transform.position = ai.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals( "Player"))
        {
            Debug.Log("���迡 ������ ����");
            nearKeyItemText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            nearKeyItemText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }

    void PickUp()
    {
        if (Input.GetButtonDown("Interaction") && isPickUp)
        {
            player.hasKey = true;
            ++player.keyCount;
            gameObject.SetActive(false);
            getKeySound.Play();
            nearKeyItemText.gameObject.SetActive(false);
            pickUpKeyItemText.gameObject.SetActive(true);
            Invoke("notshowtext", 1.5f);
            
        }
        else if(player.keyCount==0)
        {
            player.hasKey=false;
        }
    }

    void notshowtext()
    {
        pickUpKeyItemText.gameObject.SetActive(false);
    }
}
