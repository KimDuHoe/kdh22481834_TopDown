using UnityEngine;

public class kdh22481834_Bullet : MonoBehaviour
{
    public float speed = 20f; // Bullet Speed
    public int damage = 1;    // Bullet Damage

    void Update()
    {
        // head 쪽으로 전진!
        // Weapon에서 이미 방향전환 -> 앞으로 직진
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 3초 뒤 삭제 (Memory Leak 방지)
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Case A: 움직이는 파란 공(Chaser)인지 확인
        kdh22481834_EnemyChaser chaser = other.GetComponent<kdh22481834_EnemyChaser>();
        if (chaser != null)
        {
            chaser.TakeDamage(damage, transform.position);

            // Case B: 고정형 포탑(Turret)인지 확인
            kdh22481834_EnemyTurret turret = other.GetComponent<kdh22481834_EnemyTurret>();
            if (turret != null)
            {
                // 포탑에게 데미지 전달 (무적이어도 빨간색 깜빡임 효과 나옴)
                turret.TakeDamage(damage, transform.position);
            }

            // 누굴 때렸든 적 태그라면 총알 삭제
            Destroy(gameObject);
        }
        // Collision with Wall (벽)
        else if (other.CompareTag("Wall"))
        {
            // 벽에 박혔으니 총알 삭제
            Destroy(gameObject);
        }
    }
}