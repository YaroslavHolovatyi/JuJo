using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameStarted => gameStarted;
    public GameObject gameOverUI;
    public Button restartButton;
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text tapToStartText; // 🔹 Додано для "Tap to Start"
    public GameObject player;

    private bool isGameOver = false;
    private bool gameStarted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Time.timeScale = 0f; // 🔹 Ставимо гру на паузу
        if (tapToStartText != null)
            tapToStartText.gameObject.SetActive(true);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (restartButton != null)
            restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        if (tapToStartText != null)
            tapToStartText.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (restartButton != null)
            restartButton.gameObject.SetActive(true);
        else
            Debug.LogWarning("Restart Button is missing!");

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
