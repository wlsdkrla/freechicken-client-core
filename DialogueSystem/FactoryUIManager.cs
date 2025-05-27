using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Text;

public class FactoryUIManager : MonoBehaviour
{
    public static FactoryUIManager instance;

    [Header("UI")]
    public TextMeshProUGUI text;
    public GameObject nextText;
    public GameObject Korean;
    public GameObject English;

    [Header("Audio")]
    public AudioSource TalkSound;
    public AudioSource ButtonClickSound;

    [Header("Dialogue")]
    public Queue<string> sentences = new Queue<string>();
    public string currentSentence;
    public bool isTyping;
    public bool isTalkEnd;

    [Header("Game")]
    public FactoryPlayer player;
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;
    public GameObject npc;
    public bool isTalkPoint2;
    private float typingSpeed = 0.04f;


    private void Awake() => instance = this;

    private void Start()
    {
        isTalkEnd = false;
        Cursor.visible = true;
        player.isTalk = true;

        SetLanguageUI();
        StartCoroutine(WaitForTypingEnd());
    }
    private void Update()
    {
        // 현재 문장이 모두 출력되면 next 버튼 활성화
        if (text.text == currentSentence)
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        // 대사 출력이 끝난 상태에서만 스페이스바 또는 마우스 클릭으로 다음 문장 출력
        if (!isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ButtonClickSound.Play();
            NextSentence();
        }
    }

    private void SetLanguageUI()
    {
        Korean.SetActive(!PlayerData.isEnglish);
        English.SetActive(PlayerData.isEnglish);
    }

    IEnumerator WaitForTypingEnd()
    {
        while (true)
        {
            if (text.text == currentSentence)
            {
                nextText.SetActive(true);
                isTyping = false;
            }
            yield return null;
        }
    }

    public void ClickButton()
    {
        if (!isTyping)
        {
            ButtonClickSound.Play();
            NextSentence();
        }
    }

    public void OndiaLog(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
            sentences.Enqueue(line);

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        TalkSound.Play();
        nextText.SetActive(false);

        var builder = new StringBuilder();
        text.text = "";

        foreach (char ch in sentence)
        {
            builder.Append(ch);
            text.text = builder.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        instance.gameObject.SetActive(false);
        isTalkEnd = true;
        player.isTalk = false;
        player.isStopSlide = false;
        Cursor.visible = false;
        maincam.Priority = 2;
    }
}