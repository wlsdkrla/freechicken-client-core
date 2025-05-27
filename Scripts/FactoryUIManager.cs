using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;
public class FactoryUIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;
   
    public Queue<string> sentences;
    public string currentSentences;
    public bool isTyping;
   
    public static FactoryUIManager instance;
    public FactoryPlayer player;
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;

    public bool isTalkEnd;
    public GameObject npc;
    public bool isTalkPoint2;

    public AudioSource TalkSound;
    public AudioSource ButtonClickSound;
    public GameObject Korean;
    public GameObject English;
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        
        sentences = new Queue<string>();
        isTalkEnd = false;
        Cursor.visible = true;
        player.isTalk = true;
        if (PlayerData.isEnglish)
        {
            English.SetActive(true);
        }
        else if (!PlayerData.isEnglish)
        {
            Korean.SetActive(true);
        }
        
    }
   
    public void OndiaLog(string[] lines)
    {
        sentences.Clear();
        
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

       

    }
    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentences = sentences.Dequeue();
            isTyping = true;
            TalkSound.Play();
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentences));
        }
        if (sentences.Count == 0)
        {
            
            instance.gameObject.SetActive(false);
                player.isTalk = false;
                isTalkEnd = true;
            Cursor.visible = false;
                player.isStopSlide = false;
                npccam.Priority = 1;
                maincam.Priority = 2;
              
           
        }

    }
    IEnumerator Typing(string line)
    {
        text.text = "";
        foreach (char ch in line.ToCharArray())
        {
            text.text += ch;
            yield return new WaitForSeconds(0.04f);
            
            
        }
       
    }
    void Update()
    {

        if (text.text.Equals(currentSentences))
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButton(0) &&!isTyping )
        {
            if (!isTyping)

                NextSentence();
            ButtonClickSound.Play();
        }
      
       
    }
    
}
