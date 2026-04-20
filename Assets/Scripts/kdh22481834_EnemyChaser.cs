using UnityEngine;
using System.Collections; // 코루틴 사용 필수

public class kdh22481834_EnemyChaser : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3f;
    public int maxHp = 3;
    private int currentHp;

    [Header("Hit Effect")]
    public float knockbackForce = 5f; // 뒤로 밀리는 힘

    private Transform playerTarget;
    private Rigidbody rb;
    private Renderer rend;
    private Color originalColor;

    // [중요] 넉백 중에는 움직이지 못하게 하는 변수
    private bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        // 원래 색깔 기억해서 재사용
        if (rend != null) originalColor = rend.material.color;

        currentHp = maxHp;
        moveSpeed = moveSpeed * Random.Range(0.9f, 1.1f);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTarget = playerObj.transform;
    }

    void FixedUpdate()
    {
        // 넉백 당하는 중이면 이동 코드 실행 안 함 (밀려나야 하니까)
        if (isKnockedBack) return;

        if (playerTarget == null) return;

        Vector3 direction = (playerTarget.position - transform.position).normalized;
        direction.y = 0;

        if (rb != null)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    // [수정] 데미지 함수가 이제 '누가 때렸는지(위치)'를 알아야 밀려날 방향을 계산함
    public void TakeDamage(int damage, Vector3 hitPos)
    {
        currentHp -= damage;

        // 1. 빨간색 깜빡임 효과 시작
        if (rend != null)
        {
            // 기존에 깜빡이던 게 있으면 끄고 새로 시작 (연타 맞을 때 자연스럽게)
            StopCoroutine("HitFlash");
            StartCoroutine("HitFlash");
        }

        // 2. 넉백 (밀려남) 효과
        if (rb != null)
        {
            // 밀려날 방향 = (내 위치 - 총알 위치).정규화
            Vector3 knockbackDir = (transform.position - hitPos).normalized;
            knockbackDir.y = 0; // 위로는 안 튀게

            // 넉백 코루틴 시작
            StopCoroutine("KnockbackRoutine");
            StartCoroutine(KnockbackRoutine(knockbackDir));
        }

        if (currentHp <= 0) Die();
    }

    // 피격시 작동 코루틴
    IEnumerator HitFlash()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f); // 0.1초 유지
        rend.material.color = originalColor; // 원상복구
    }

    // 넉백 코루틴
    IEnumerator KnockbackRoutine(Vector3 dir)
    {
        isKnockedBack = true; // 이동 코드 정지

        // 순간적인 힘을 줘서 뒤로 밀어버림
        rb.linearVelocity = Vector3.zero; // 기존 속도 제거
        rb.AddForce(dir * knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f); // 0.2초 동안 밀려남

        isKnockedBack = false; // 다시 추격 시작
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            kdh22481834_PlayerController playerScript = collision.gameObject.GetComponent<kdh22481834_PlayerController>();
            if (playerScript != null) playerScript.TakeDamage(1);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}