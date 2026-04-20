using UnityEngine;

public class kdh22481834_EnemyTurret : MonoBehaviour
{
    [Header("Settings")]
    public float fireRate = 2.0f; // 발사 속도
    public float attackRange = 25f; // 사거리

    [Header("Bullet")]
    public GameObject monsterBulletPrefab;
    public Transform firePoint;

    private Transform player;
    private float timer = 0f;

    void Start()
    {
        // 플레이어 찾기
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        // 발사 타이머 랜덤 시작
        timer = Random.Range(0f, 1f);
    }

    void Update()
    {
        if (player == null) return;

        // 1. 거리 체크
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > attackRange) return;

        // 2. 플레이어 바라보기 (회전)
        transform.LookAt(player);

        // 3. 발사 타이머
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        if (monsterBulletPrefab == null) return;

        // FirePoint가 있으면 point, 없으면 몸체에서 발사
        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;

        Instantiate(monsterBulletPrefab, spawnPos, transform.rotation);
    }

    // 추후 포탑도 부술 수 있다면
    public void TakeDamage(int damage, Vector3 hitPos)
    {
        // 내용은 비워둡니다. 
        // 맞긴 맞았지만, 아무런 반응도(데미지, 깜빡임) 하지 않습니다.
    }
}