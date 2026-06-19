using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score;
    public float Speed = 5f;
    public float BaseSpeed;
    public float SpeedMult = 1f;

    [SerializeField] AudioClip deathSFX;
    private int highscore;
    private DuckController duckPlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        duckPlayer = FindFirstObjectByType<DuckController>();
        Instance = this;
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    private void Start()
    {
        BaseSpeed = Speed;
        StartCoroutine(IncreaseSpped());
    }

    private void Update()
    {
        Speed = BaseSpeed * SpeedMult;
    }

    public void GameOver()
    {
        SpeedMult = 0f;
        duckPlayer.CanMove = false;
        bool isHighscoreNew = Score > PlayerPrefs.GetInt("highscore", 0);
        if (isHighscoreNew)
        {
            PlayerPrefs.SetInt("highscore", Score);
            PlayerPrefs.Save();
            highscore = Score;
        }
        else
        {
            highscore = PlayerPrefs.GetInt("highscore", 0);
        }
        GameplayUI.Instance.GameOverScreen(Score, highscore);
        
        AudioMgr.Instance.StopMusic();
        AudioMgr.Instance.PlaySFX(deathSFX);
    }

    IEnumerator IncreaseSpped()
    {
        yield return new WaitForSeconds(15f);
        BaseSpeed += 2f;
        yield return new WaitForSeconds(20f);
        BaseSpeed += 2f;
        yield return new WaitForSeconds(20f);
        BaseSpeed += 2f;
    }
}
