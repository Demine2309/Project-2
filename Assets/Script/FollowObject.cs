using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    //public Transform target;
    //[SerializeField] private Vector3 offSet = new Vector3(0, 2, -10);
    //public float smoothTime = 0.25f;
    //Vector3 currentVelocity;

    //private void LateUpdate()
    //{
    //    transform.position = Vector3.SmoothDamp(transform.position, target.position + offSet, ref currentVelocity, smoothTime);
    //}
    public Transform dummy;
    public Camera cam;

    [SerializeField] private float horizontalMargin = 0.3f;
    [SerializeField] private float verticalMargin = 0.4f;
    [SerializeField] private float depth = -10;
    [SerializeField] private float smoothTime = 0.25f;

    Vector3 target;
    Vector3 lastPosition;
    Vector3 currentVelocity;

    private void LateUpdate()
    {
        SetTarget();
        MoveCamera();
    }

    void SetTarget()
    {
        Vector3 movementDelta = dummy.position - lastPosition;
        Vector3 screenPos = cam.WorldToScreenPoint(dummy.position);
        Vector3 bottomLeft = cam.ViewportToScreenPoint(new Vector3(horizontalMargin, verticalMargin, 0));
        Vector3 topRight = cam.ViewportToScreenPoint(new Vector3(1 - horizontalMargin, 1 - verticalMargin, 0));

        if (screenPos.x < bottomLeft.x || screenPos.x > topRight.x)
        {
            target.x += movementDelta.x;
        }

        if (screenPos.y < bottomLeft.y || screenPos.y > topRight.y)
        {
            target.y += movementDelta.y;
        }

        target.z = depth;
        lastPosition = dummy.position;
    }

    void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
    }
}
