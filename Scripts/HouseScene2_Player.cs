using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Cinemachine;
//using UnityEngine.UIElements;

public class HouseScene2_Player : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;

    public GameObject player;
    public bool isfallingObstacle;

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

    [Header("Camera")]
    public CinemachineVirtualCamera npc_cam;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera unicycleCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource trumpetAudio;

    [Header("Dialogue")]
    public GameObject NPCDialogue1;
    public GameObject NPCDialogue2;
    public GameObject NPC;
    public GameObject UnicycleDialogue1;
    public GameObject UnicycleDialogue2;
    public bool isTalk1;
    public bool TalkEnd1;
    public bool isTalk2;
    public bool TalkEnd2;

    public GameObject Pos;
    public GameObject Pos2;

    public GameObject EvolutionPlayer;
    public GameObject EvolutionSense;

    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 3.0f;
    public GameObject EvoluPs;

    void Awake()
    {
        mainAudio.Play();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        Cursor.visible = false;
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if(this.gameObject.transform.position.y <= -100f && !Dead)
        {
            Dead = true;
            DieMotion();
            Invoke("ReLoadScene", 2f);
        }

        if (!Dead)
        {
            if (!isTalk1 || !isTalk2)
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
        if (!isTalk1 && !isTalk2)
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
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead && !isTalk1 && !isTalk2)
        {
            jumpAudio.Play();
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            JumpPs.Play();
        }
    }

    void DieMotion()
    {
        DiePs.gameObject.SetActive(true);
        anim.SetBool("isDead", true);
        dieAudio.Play();
    }

    void ReLoadScene()
    {
        Dead = false;
        anim.SetBool("isDead", false);
        DeadCount.count++;

        if (TalkEnd1)
        {
            this.gameObject.transform.position = Pos2.gameObject.transform.position;
        }
        else if (!TalkEnd1)
        {
            this.gameObject.transform.position = Pos.gameObject.transform.position;
        }
       
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropBox"))
        {
            isfallingObstacle = true;
        }

        if (other.gameObject.CompareTag("NPC") && !isTalk1 && !TalkEnd1 && !PlayerData.isEnglish)
        {
            isTalk1 = true;
            NPCDialogue1.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);

            npc_cam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.CompareTag("NPC") && !isTalk1 && !TalkEnd1 && PlayerData.isEnglish)
        {
            isTalk1 = true;
            NPCDialogue2.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);

            npc_cam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "Unicycle_Sense" && !isTalk2 && !TalkEnd2 && !PlayerData.isEnglish)
        {
            isTalk2 = true;
            UnicycleDialogue1.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            unicycleCam.Priority = 10;

            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "Unicycle_Sense" && !isTalk2 && !TalkEnd2 && PlayerData.isEnglish)
        {
            isTalk2 = true;
            UnicycleDialogue2.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            unicycleCam.Priority = 10;

            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "EvolutionSense1")
        {
            trumpetAudio.Play();
            StartRotation();
            Invoke("Destroy_", 2f);
        }
    }

    void Destroy_()
    {
        Destroy(this.gameObject);
        EvolutionPlayer.SetActive(true);
        EvolutionSense.SetActive(false);
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); 

        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);
        EvoluPs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

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
        if (other.gameObject.CompareTag("NPC"))
        {
            npc_cam.Priority = 1;
            mainCam.Priority = 10;

            TalkEnd1 = true;
        }

        if (other.gameObject.name == "Unicycle_Sense")
        {
            unicycleCam.Priority = 1;
            mainCam.Priority = 10;

            TalkEnd2 = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House2_Obstacle") && !Dead)
        {
            Dead = true;
            DieMotion();
            DieCanvas.gameObject.SetActive(true);
            Invoke("ReLoadScene", 2f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    public void LookAround() 
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