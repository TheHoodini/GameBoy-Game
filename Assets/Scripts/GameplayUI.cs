using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI ResultsScore;
    [SerializeField] private TextMeshProUGUI score;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        score.text = GameManager.Instance.Score.ToString();
    }

    public void GameOverScreen(int score, int highScore)
    {
        ResultsScore.text = $"Aura: {score} H.Score: {highScore}";
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
