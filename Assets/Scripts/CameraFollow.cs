using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    public float screenBoxPercentage;

    private bool isFollowing = false;

    private void FixedUpdate()
    {
        Follow();
    }

    private bool CheckInBoundingBox()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        float screenWidthValue = Screen.width * screenBoxPercentage / 100;
        float screenHeightValue = Screen.height * screenBoxPercentage / 100;
        return (screenPos.x > screenWidthValue && screenPos.x < Screen.width - screenWidthValue)
                && (screenPos.y > screenHeightValue && screenPos.y < Screen.height - screenHeightValue);
    }

    private void Follow()
    {
        Vector3 desiredPosition = target.position + offset;
        float distanceToTarget = (desiredPosition - transform.position).magnitude;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, distanceToTarget * smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

}
