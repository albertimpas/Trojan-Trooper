using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public enum GameMode
    {
        Easy,
        Medium,
        Hard
    }

    public GameMode gameMode; // Set this in the Inspector for each mode
    private int score = 0;
    private int highestScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highestScoreText;

    void Start()
    {
        // Use the gameMode as part of the PlayerPrefs key to store and retrieve scores for each mode
        string modeKey = "HighestScore_" + gameMode.ToString();
        highestScore = PlayerPrefs.GetInt(modeKey, 0);
        UpdateHighestScoreUI();
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateHighestScore(); // Call to update the highest score
    }

    public void UpdateHighestScore()
    {
        if (score > highestScore)
        {
            highestScore = score;
            string modeKey = "HighestScore_" + gameMode.ToString();
            PlayerPrefs.SetInt(modeKey, highestScore);
            PlayerPrefs.Save();
            UpdateHighestScoreUI();
        }
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highestScore;
    }
}
