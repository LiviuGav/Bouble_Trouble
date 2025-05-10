using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject heartPrefab; // Un UI Image cu o inimă
    public Transform heartContainer;

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        UpdateHearts();
    }

    void Update()
    {
        if (hearts.Count != playerHealth.lives)
        {
            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        // Șterge inimile vechi
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // Creează inimile noi
        for (int i = 0; i < playerHealth.lives; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(newHeart);
        }
    }
}
