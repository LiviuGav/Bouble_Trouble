using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    [Header("Settings")]
    public float delayBeforeReturn = 3f; // Timp în secunde până la revenire
    public float fadeDuration = 1f; // Durata efectului de fade

    [Header("References")]
    public Image fadePanel;
    public Text countdownText; // Opțional

    void Start()
    {
        gameObject.SetActive(false);
        if (fadePanel != null) fadePanel.color = new Color(0, 0, 0, 0);
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; // Pauză joc
        StartCoroutine(ReturnToMenuRoutine());
    }

    IEnumerator ReturnToMenuRoutine()
    {
        // Contor invers (opțional)
        float remainingTime = delayBeforeReturn;
        while (remainingTime > 0)
        {
            if (countdownText != null)
                countdownText.text = $"Revenire în meniu în {Mathf.Ceil(remainingTime)}...";

            remainingTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        // Fade-out
        if (fadePanel != null)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                fadePanel.color = new Color(0, 0, 0, timer / fadeDuration);
                yield return null;
            }
        }

        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Repornire timp
        SceneManager.LoadScene("MainMenu");
    }
}