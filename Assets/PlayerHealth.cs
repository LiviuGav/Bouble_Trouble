using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 1;
    [SerializeField] private GameOverScreen gameOverScreen;
    public void TakeDamage()
    {
        lives--;
        Debug.Log(gameObject.name + " a fost lovit! Vieți rămase: " + lives);

        if (lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        Debug.Log($"{name} a murit!");

        // Verifică dacă mai sunt jucători în viață
        if (AreAllPlayersDead())
        {
            ShowGameOver();
        }
    }

    bool AreAllPlayersDead()
    {
        PlayerHealth[] players = FindObjectsByType<PlayerHealth>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (PlayerHealth player in players)
        {
            if (player.gameObject.activeSelf && player.lives > 0)
            {
                Debug.Log($"Mai există jucători în viață: {player.name}");
                return false;
            }
        }
        return true;
    }

    void ShowGameOver()
    {
        // Încearcă mai întâi referința directă
        if (gameOverScreen != null)
        {
            gameOverScreen.ShowGameOver();
            return;
        }

        // Fallback: caută în scenă dacă referința nu e setată
        gameOverScreen = FindFirstObjectByType<GameOverScreen>(FindObjectsInactive.Include);
        if (gameOverScreen != null)
        {
            gameOverScreen.ShowGameOver();
        }
        else
        {
            Debug.LogError("Nu s-a găsit GameOverScreen în scenă! Verifică:");
            Debug.LogError("- Există un obiect cu scriptul GameOverScreen?");
            Debug.LogError("- Este activat în Hierarchy?");
            Debug.LogError("- Ai setat referința în Inspector?");
        }
    }
}