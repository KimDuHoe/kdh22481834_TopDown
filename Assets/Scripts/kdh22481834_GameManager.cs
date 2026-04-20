using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class kdh22481834_GameManager : MonoBehaviour
{
    public static kdh22481834_GameManager Instance;

    [Header("Game Settings")]
    public float gameTime = 300f;

    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public float spawnRangeLimit = 28f;

    [Header("Turret Settings")]
    public GameObject turretGroup;
    private bool isTurretActive = false;

    // 난이도 변수
    private float currentSpawnInterval = 2.0f;
    private float timer = 0f;
    private bool isGameOver = false;

    [Header("UI Objects")]
    public GameObject startPanel;    // 시작 화면 패널
    public GameObject gameUIPanel;   // 게임 중 UI 패널 (HP, 시간 등)

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI resultText;
    public GameObject restartButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 1. 포탑 끄기
        if (turretGroup != null) turretGroup.SetActive(false);

        // 2. 시간 멈춤
        Time.timeScale = 0f;

        // 3. 시작 화면은 켜고, 게임 UI는 끄기
        if (startPanel != null) startPanel.SetActive(true);
        if (gameUIPanel != null) gameUIPanel.SetActive(false); // 가리기
    }

    // 시작 버튼 함수
    public void GameStart()
    {
        Time.timeScale = 1f; // 시간 흐름

        // 4. 시작 화면은 끄고, 게임 UI는 켜기
        if (startPanel != null) startPanel.SetActive(false);
        if (gameUIPanel != null) gameUIPanel.SetActive(true); // 보여주기
    }

    void Update()
    {
        if (isGameOver) return;

        gameTime -= Time.deltaTime;
        if (timeText != null)
        {
            timeText.text = $"Time: {Mathf.Ceil(gameTime)}";
        }

        CheckPhase();

        if (gameTime <= 0)
        {
            GameClear();
            return;
        }

        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void CheckPhase()
    {
        if (gameTime > 200f)
        {
            currentSpawnInterval = 2.0f;
        }
        else if (gameTime > 100f)
        {
            currentSpawnInterval = 1.0f;
            if (isTurretActive == false) ActivateTurrets();
        }
        else
        {
            currentSpawnInterval = 0.5f;
        }
    }

    void ActivateTurrets()
    {
        if (turretGroup != null) turretGroup.SetActive(true);
        isTurretActive = true;
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-spawnRangeLimit, spawnRangeLimit);
        float randomZ = Random.Range(-spawnRangeLimit, spawnRangeLimit);
        Vector3 spawnPos = new Vector3(randomX, 0.5f, randomZ);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // 게임오버 때는 게임 UI를 가릴지 말지
        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "GAME OVER";
            resultText.color = Color.red;
        }
        if (restartButton != null) restartButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameClear()
    {
        if (isGameOver) return;
        isGameOver = true;
        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "SURVIVED!!";
            resultText.color = Color.blue;
        }
        if (restartButton != null) restartButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}