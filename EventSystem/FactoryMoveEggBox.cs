using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FactoryMoveEggBox : MonoBehaviour
{
    public float Speed = 0.25f;
    public FactoryPlayer player;
    public bool isChk;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera moveCam;

    private Dictionary<string, Vector3> moveDirections;

    void Awake()
    {
        Application.targetFrameRate = 30;
        moveDirections = new Dictionary<string, Vector3>
        {
            { "Slide", Vector3.forward },
            { "TurnPointR", Vector3.right },
            { "TurnPointL", Vector3.left },
            { "TurnPointD", Vector3.back }
        };
    }

    void Start()
    {
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        Speed = 0;
        StartCoroutine(CheckStart());
    }

    IEnumerator CheckStart()
    {
        yield return new WaitUntil(() => player.isSetEggFinish && !isChk);

        // 카메라 전환
        if (mainCam && moveCam)
        {
            mainCam.Priority = 1;
            moveCam.Priority = 2;
        }

        Speed = 0.25f;
        isChk = true;
    }

    void Update()
    {
        if (player.isSetEggFinish && player.isEgg)
        {
            var pos = player.tmpBox.transform.position;
            player.transform.position = pos;
            player.EggPrefab.transform.position = pos;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!moveDirections.TryGetValue(other.tag, out Vector3 direction))
            return;

        transform.Translate(direction * Speed * Time.deltaTime, Space.World);
    }
}
