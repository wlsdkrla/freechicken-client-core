using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;
using System.IO;
public class EvloutionPlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;
    public GameObject player;

    public GameObject DieCanvas;

    Vector3 moveVec;
    Vector2 moveInput;

    bool wDown;
    bool isJump;
    Rigidbody rigid;

    public float speed;
    public float JumpPower;

    bool Dead;

    public float hAxis;
    public float vAxis;

    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;

    Animator anim;

    public GameObject Pos;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera unicycleCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;

    [Header("Dialogue")]
    public GameObject ReadygoCity;
    public bool isTalk2;
    public bool TalkEnd2;

    

    // ��ȭȿ��
    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 2.0f;
    public GameObject EvoluPs;



    void Awake()
    {
        //mainAudio.Play();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            if (!isTalk2)
            {
                if (isRotating)
                {
                    HandleCameraRotation();
                }
                else
                {
                    Move();
                    GetInput();
                    Jump();
                    LookAround();
                }
            }
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
    }

    void Move()
    {
        moveInput = new Vector2(hAxis, vAxis);
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveVec = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveVec;
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
            runAudio.Play();
        }
        anim.SetBool("Run", moveInput != Vector2.zero);
        anim.SetBool("Walk", wDown);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead && !isTalk2)
        {
            jumpAudio.Play();
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            JumpPs.Play();
        }
    }

    void DieMotion()
    {
        Dead = true;
        DiePs.gameObject.SetActive(true);
        anim.SetBool("isDead", true);
        dieAudio.Play();
    }

    void ReLoadScene()
    {
        Dead = false;
        anim.SetBool("isDead",false);
        DiePs.gameObject.SetActive(false);
        this.gameObject.transform.position = Pos.gameObject.transform.position;
        //SceneManager.LoadScene("HouseScene2");
        DieCanvas.gameObject.SetActive(false);
    }

    void NextCityScene()
    {
        
    }
  
    void OnTriggerEnter(Collider other)
    {
      

        if (other.CompareTag("evolu")) 
        {
            StartRotation();
            ReadygoCity.SetActive(true);
        }

        if (other.gameObject.name == "GoCitySense")
        {
            isTalk2 = true;
            /*GameSave.Level = 3;
            GameSave.isCity = true;
            PlayerData playerData = new PlayerData();
            playerData.LevelChk = GameSave.Level;
            string json = JsonUtility.ToJson(playerData);

            File.WriteAllText("playerData.json", json);

            PlayerPrefs.SetInt("GoCity", GameSave.isCity ? 1 : 0);

*/
            GameSave.Level = 3;
            if (File.Exists("PlayerData.json"))
            {

                string jsonData = File.ReadAllText("playerData.json");
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (loadedData.LevelChk >= GameSave.Level)
                {
                    GameSave.Level = loadedData.LevelChk;
                }
                else
                {
                    GameSave.Level = 3;
                }
            }
            else
            {
                GameSave.Level = 3;
            }

            LoadSceneInfo.is2DEnterScene = true;
            PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 2;
            SceneManager.LoadScene("LoadingScene");
        }
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        // ȸ�� ���� ��� (0���� 720������)
        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); // 0���� 720������ �� ���� ȸ��

        // ȸ��
        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);

        EvoluPs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            // ȸ���� �Ϸ�� �Ŀ� ���� ���·� ���ư��� ó�� �߰�
            cameraArm.rotation = originalCameraRotation;
            EvoluPs.SetActive(false);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        originalCameraRotation = cameraArm.rotation; 
    }


    void OnTriggerExit(Collider other)
    {
       

        if (other.CompareTag("evolu"))
        {
            other.gameObject.SetActive(false);
            ReadygoCity.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House2_Obstacle") && !Dead)
        {
            Dead = true;
            DieMotion();
            DieCanvas.gameObject.SetActive(true);
            Invoke("ReLoadScene", 3.5f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    public void LookAround() // ī�޶�
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 100f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}

