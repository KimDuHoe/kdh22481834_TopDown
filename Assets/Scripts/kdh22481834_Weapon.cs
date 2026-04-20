using UnityEngine;

public class kdh22481834_Weapon : MonoBehaviour
{
    [Header("Settings")]
    public GameObject bulletPrefab; // 총알 프리팹 연결
    public float attackRate = 0.2f; // 연사 속도 (0.2초)

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 쿨타임이 차면 발사
        if (timer >= attackRate)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        // 1. 마우스가 찍은 바닥 위치 찾기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            // 2. 방향 계산 (플레이어 -> 마우스)
            Vector3 fireDirection = (hitPoint - transform.position).normalized;
            fireDirection.y = 0; // 높이 차이 무시

            // 3. [핵심] 회전값 계산
            // "캡슐의 머리(Up)가 발사 방향(fireDirection)을 보게함"
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, fireDirection);

            // 4. 생성 (위치는 플레이어 자리, 회전은 계산한 대로!)
            Instantiate(bulletPrefab, transform.position, rotation);
        }
    }
}