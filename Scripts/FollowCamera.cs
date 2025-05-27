using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 firstTarget; // ���� �÷��̾� ��ġ
    public Vector3 offset; // ī�޶� ��ġ

    void Update()
    {
        transform.position = target.position + offset - firstTarget;
    }

}
