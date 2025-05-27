using System.Collections;
using UnityEngine;

public class EggModeController : MonoBehaviour
{
    public GameObject eggPrefab;
    public GameObject playerMesh;
    public GameObject turnEggCanvas;
    public GameObject changeEggCanvas;
    public GameObject changeEggDoor;
    public Transform eggTransform;

    public bool isEgg = false;
    public bool isClick = false;
    public bool isSetEggFinish = false;
    private Coroutine checkCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (!isClick && !isSetEggFinish && other.CompareTag("PointZone"))
        {
            turnEggCanvas.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isClick && !isSetEggFinish && other.CompareTag("PointZone"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartEggTransformation(other.transform.position);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isSetEggFinish && other.CompareTag("PointZone"))
        {
            turnEggCanvas.SetActive(false);
        }
    }

    private void StartEggTransformation(Vector3 position)
    {
        isClick = true;
        UpdateGameObjectStates(false, true, false, true);
        eggPrefab.transform.position = position;
        isEgg = true;
        checkCoroutine = StartCoroutine(Egg());
        StartCoroutine(Check());
    }

    IEnumerator Egg()
    {
        yield return new WaitForSeconds(3f);
        DisableGameElements();
        isSetEggFinish = true;
    }

    IEnumerator Check()
    {
        float timer = 3f;
        while (timer > 0f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopCoroutine(checkCoroutine);
                ResetToPlayerState();
                yield break;
            }
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private void ResetToPlayerState()
    {
        UpdateGameObjectStates(true, false, false, false);
        isClick = false;
        isEgg = false;
    }

    private void DisableGameElements()
    {
        changeEggDoor.SetActive(false);
        turnEggCanvas.SetActive(false);
        changeEggCanvas.SetActive(false);
    }

    private void UpdateGameObjectStates(bool isMeshActive, bool isEggPrefabActive, bool isTurnEggCanvasActive, bool isChangeEggCanvasActive)
    {
        playerMesh.SetActive(isMeshActive);
        eggPrefab.SetActive(isEggPrefabActive);
        turnEggCanvas.SetActive(isTurnEggCanvasActive);
        changeEggCanvas.SetActive(isChangeEggCanvasActive);
    }
}