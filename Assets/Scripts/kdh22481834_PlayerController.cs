using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using TMPro;

public class kdh22481834_PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f; // Player movement speed

    public int maxHp = 40; // Maximum HP
    private int currentHp; // Current HP
    private bool isInvincible = false; // 피격시 무적 상태 여부

    [Header("Health UI")]
    public TextMeshProUGUI hpText; // 체력바 UI

    private Rigidbody rb; // Physical information 
    private Renderer rend; // Visual information (Color Change)
    private Color originalColor; // Original Color (원래 색깔)

    void Start()
    {
        // 초기화
        rb = GetComponent<Rigidbody>();

        rend = GetComponent<Renderer>();
        if (rend != null) originalColor = rend.material.color;

        // 시작할 때 체력 초기화
        currentHp = maxHp;

        // 시작하면 HP UI 갱신
        UpdateHPUI();
    }

    void FixedUpdate()
    {
        // 1. Push Keyboard (left:A, right:D, up:W, down:S)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. Move Player
        // new Vector3(h, v, 0) -> Fly to the sky
        // new Vector3(h, 0, v) -> Move on the ground
        Vector3 direction = new Vector3(h, 0, v).normalized;

        // 3. Physical Move
        if (rb != null)         // 에러 방지
        {
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    // Call when attacked by enemy
    public void TakeDamage(int damage)
    {
        // If already invincible, ignore damage
        // Invoke : 즉시 지시, Enumerator : 특정 시간 후에 함수 실행
        if (isInvincible) return;

        currentHp -= damage;
        // 맞을때마다 갱신
        UpdateHPUI();
        Debug.Log($"공격받음! 남은 체력: {currentHp}");

        isInvincible = true; // 무적 상태 시작

        // 죽음 처리
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            // Flicker(깜빡임) 효과 시작
            StartCoroutine(BlinkEffect());
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null)
        {
            // 화면에 "HP: currentHP/maxHP" 처럼 표시
            hpText.text = $"HP: {currentHp} / {maxHp}";
        }
    }

    // Hit effect : Change Color to Red and return to original color
    IEnumerator BlinkEffect()
    {
        // 1초 동안 깜빡거림(무적시간)
        for (int i = 0; i < 5; i++)
        {
            // 1. 빨간색으로 변함 (맞았다는 표시)
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            // 2. 원래 색으로 복구
            rend.material.color = originalColor;
            yield return new WaitForSeconds(0.1f); // 0.1초 대기
        }

        // 반복이 끝나면 무적 해제
        isInvincible = false;
        rend.material.color = originalColor; // 색 복구(확인용)
    }

    void Die()
    {
        Debug.Log("으악! 사망했습니다.");
        Destroy(gameObject); // 플레이어 삭제
        // Call Game Over in Game Manager
        kdh22481834_GameManager.Instance.GameOver();
    }
}
