using System.Collections;
using UnityEngine;
using Cinemachine;

public class FactoryManagerController : MonoBehaviour
{
    [Header("References")]
    public FactoryPlayer player;
    public EggModeController eggModeController;
    public Animator anim;
    public GameObject eggBox;
    public GameObject eggBoxSpawnPos;
    public GameObject attackBox;
    public GameObject wall;

    [Header("UI")]
    public GameObject successCanvas;
    public GameObject failCanvas;

    [Header("Camera")]
    public CameraManager cameraManager;
    [Header("Audio")]
    public AudioSource hitAudio;
    public AudioSource bgmSuccess;
    public AudioSource bgmFail;
    public AudioSource heartAudio;

    [Header("Particle")]
    public GameObject particlePrefab;
    public GameObject attackParticle;

    private bool isChk;
    [SerializeField] private Transform eggBoxResetTransform;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<FactoryPlayer>();
        eggModeController = FindObjectOfType<EggModeController>();
        StartCoroutine(WaitForEggReachAttackPosition());
    }

    IEnumerator WaitForEggReachAttackPosition()
    {
        yield return new WaitUntil(() => eggModeController.isSetEggFinish);
        yield return new WaitUntil(() =>
            Vector3.Distance(player.tmpBox.transform.position, attackBox.transform.position) < 0.1f);

        yield return StartCoroutine(ExecuteAttack());
    }

    IEnumerator ExecuteAttack()
    {
        Vector3 attackPos = attackBox.transform.position;
        GameObject particle = Instantiate(particlePrefab, attackPos, Quaternion.identity);
        bool isSuccess = Vector3.Distance(player.tmpBox.transform.position, attackPos) < 0.1f;

        yield return new WaitForSeconds(isSuccess ? 2f : 1.5f);

        if (isSuccess) HandleSuccess();
        else HandleFail();

        Destroy(particle, 3f);
    }

    void HandleSuccess()
    {
        successCanvas.SetActive(true);
        cameraManager.SetManagerInCamActive();
        Invoke(nameof(ResolveSuccess), 2f);
    }

    void ResolveSuccess()
    {
        successCanvas.SetActive(false);
        wall.SetActive(true);
        SwitchToPlayerMode();
        ResetEggBox();

        cameraManager.SetMainCamActive();

        heartAudio.Stop();
        bgmSuccess.Play();

        DeadCount.count++;
        ResetState();
    }

    void HandleFail()
    {
        failCanvas.SetActive(true);
        Invoke(nameof(ResolveFail), 3f);
    }

    void ResolveFail()
    {
        failCanvas.SetActive(false);
        cameraManager.SetMainCamActive();

        SwitchToPlayerMode();
        eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0.1f;
        anim.SetBool("isAttack", false);
        bgmFail.Play();

        player.isStopSlide = false;
    }

    void SwitchToPlayerMode()
    {
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
    }

    void ResetEggBox()
    {
        eggBox.transform.SetPositionAndRotation(
            eggBoxResetTransform.position,
            eggBoxResetTransform.rotation
        );
        eggBox.GetComponent<FactoryMoveEggBox>().isChk = false;
    }

    void ResetState()
    {
        isChk = false;
        player.isSetEggFinish = false;
        player.isClick = false;
        player.isChk = false;
        anim.SetBool("isAttack", false);
        player.Pos();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EggBox") && !isChk)
        {
            isChk = true;
            eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0f;
            anim.SetBool("isAttack", true);
            Invoke(nameof(PlayHitSound), 0.5f);
        }
    }

    void PlayHitSound()
    {
        hitAudio.Play();
        if (attackParticle != null)
        {
            attackParticle.SetActive(true);
        }
    }
}