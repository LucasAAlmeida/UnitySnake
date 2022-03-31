using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int Score { get; private set; }
    
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text ScoreText;

    bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    void Start()
    {
        Instantiate(applePrefab);
        applePrefab.GetComponent<AppleController>().MoveApple();

        Score = 0;

        StartCoroutine(AddScoreForTime());

        AddScore(0);
    }

    public void GameOver()
    {
        isGameOver = true;

        gameOverScreen.SetActive(true);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    private IEnumerator AddScoreForTime()
    {
        yield return new WaitForSeconds(2);
        while (!isGameOver) {
            AddScore(5);
            yield return new WaitForSeconds(2);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        ScoreText.text = "Score: " + Score;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Boot");
    }
}
