using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefabul pentru bula mare
    public float spawnInterval = 5f; // Intervalul de timp între fiecare instanțiere (în secunde)
    public float spawnHeight = 5f; // Înălțimea la care va apărea bula de sus

    void Start()
    {
        StartCoroutine(SpawnBubble()); // Pornește funcția de generare a bulei
    }

    IEnumerator SpawnBubble()
    {
        while (true) // Crează bule în mod continuu
        {
            Instantiate(bubblePrefab, GetRandomSpawnPosition(), Quaternion.identity); // Instanțiază bula mare la poziția random
            yield return new WaitForSeconds(spawnInterval); // Așteaptă 6 secunde înainte de a genera o nouă bula
        }
    }

    // Funcție pentru a obține o poziție aleatorie pe axa X între -8 și 8 și fix la spawnHeight pe axa Y
    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-8f, 8f); // Locație aleatorie pe axa X
        return new Vector3(randomX, spawnHeight, 0f); // Returnează locația la spawnHeight pe Y (fix)
    }
}
