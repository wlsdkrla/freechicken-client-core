using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryFixUI : MonoBehaviour
{   
    public Slider slider;
    public TextMeshProUGUI stopSlideTxt;
    public GameObject nonestopSlideTxt;
    public FactoryPlayer factoryPlayer;
    public TextMeshProUGUI E;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera stopConCam;

    public GameObject fixObj;
    public AudioSource fixAudio;
    public AudioSource GetEggSound;
    float t = 0;
    
    void Start()
    {
        stopSlideTxt.gameObject.SetActive(false);
        factoryPlayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
    }
  
    void Update()
    {
        
        if (Input.GetButton("E"))
        {
            
            E.color = Color.red;
           
            if (slider.value < 100f)
            {
                t += Time.deltaTime;
                slider.value = Mathf.Lerp(0, 100, t);
            }
            else
            {
                nonestopSlideTxt.SetActive(false);
                stopSlideTxt.gameObject.SetActive(true);
               
                factoryPlayer.isSlide = false;
                factoryPlayer.isStopSlide = true;
                StartCoroutine(Stop());
                
            }

        }
        if (Input.GetButtonUp("E"))
        {
            mainCam.Priority = 2;
            stopConCam.Priority = 1;
            E.color = Color.white;
            t = 0;
            slider.value = 0;
        }

    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(.5f);
        mainCam.Priority = 2;
        stopConCam.Priority = 1;
        fixAudio.Stop();
        GetEggSound.Play();
        Destroy(fixObj);
       
        this.gameObject.SetActive(false);
    }
   
}