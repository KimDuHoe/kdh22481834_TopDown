using UnityEngine;

public class kdh22481834_CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // target 추적
    public float smoothSpeed = 0.125f; // Following Speed
    public Vector3 offset;   // Player ~ Camera interval

    [Header("Smooth Settings")]
    public float smoothTime = 0.2f; // 목표까지 도달하는 시간 (작을수록 빠름)
    private Vector3 currentVelocity = Vector3.zero; // 현재 속도

    void LateUpdate() // 후처리
    {
        // target X : return
        if (target == null) return;

        // 1. 목표 위치 : 플레이어 위치 + 초기에 설정한 간격(offset)
        Vector3 targetPosition = target.position + offset;

        // 2. 부드럽게 이동 (SmoothDamp, Lerp 보다 정교하다고 함)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}