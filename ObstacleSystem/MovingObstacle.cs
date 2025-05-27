using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public MoveObstacleType Type;
    public float moveSpeed;
    public float distance;
    public GameObject player;
    public GameObject moveObj;
    public Animator objAnimator;

    [HideInInspector] public bool isMove = false;
    [HideInInspector] public bool isPlayerFollow = false;
    [HideInInspector] public bool isSense = false;
    [HideInInspector] public bool isBigJump = false;
    [HideInInspector] public bool isDropObj = false;
    [HideInInspector] public bool removeObj = false;

    public bool isCube, isBarrel;

    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public float initPositionX, initPositionY, turningPoint;
    [HideInInspector] public bool turnSwitch = false;

    private IObstacleBehavior behavior;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        behavior = ObstacleBehaviorFactory.Create(Type);
        behavior?.Initialize(this);
    }

    void Update()
    {
        behavior?.UpdateBehavior();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerFollow = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerFollow = false;
        }
    }

}
