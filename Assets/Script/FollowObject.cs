using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offSet = new Vector3(0, 2, -10);

    private void LateUpdate()
    {
        transform.position = target.position + offSet;
    }
}
