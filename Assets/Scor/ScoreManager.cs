using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TMP_Text scoreText; // Referință directă
    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Găsește TextMeshPro automat dacă nu e setat în Inspector
            if (scoreText == null)
            {
                scoreText = GameObject.Find("ScoreText_TMP")?.GetComponent<TMP_Text>();
                if (scoreText == null) Debug.LogError("Nu am găsit ScoreText_TMP în scenă!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateScore();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
        Debug.Log($"Scor actualizat: {score}"); // Pentru debug
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = $"SCOR: {score}";
    }
}