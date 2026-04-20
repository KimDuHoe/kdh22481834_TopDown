using UnityEngine;

public class kdh22481834_MonsterBullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 15f; // 총알 속도
    public int damage = 1;    // 데미지

    void Start()
    {
        // 1. 태어나자마자 플레이어를 찾아서 조준
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // 플레이어의 발 밑이 아니라 '가슴 높이(Y: 1.0)'를 향해 쏘도록 보정
            Vector3 targetPos = player.transform.position;
            targetPos.y = 1.0f;

            // 총알의 앞머리를 플레이어 쪽으로 돌림
            transform.LookAt(targetPos);
        }

        // 2. 3초 뒤 자동 삭제 (메모리 관리)
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // 3. 바라보는 방향(플레이어 방향)으로 계속 직진
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // [핵심 기능] 
        // 나를 쏜 포탑(Enemy)이나, 옆에 있는 다른 총알(Enemy)과는 부딪혀도 무시!
        // (이게 없으면 총알이 나오자마자 터지거나 멈춥니다)
        if (other.CompareTag("Enemy"))
        {
            return; // 함수 종료 (통과)
        }

        // 플레이어 맞춤
        if (other.CompareTag("Player"))
        {
            kdh22481834_PlayerController playerScript = other.GetComponent<kdh22481834_PlayerController>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage); // 플레이어 아야!
            }
            Destroy(gameObject); // 총알 삭제
        }
        // 벽에 맞춤
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // 총알 삭제
        }
    }
}