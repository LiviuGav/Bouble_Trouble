using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    void Start()
    {
        // Verifică dacă Player2 există în scenă
        if (player2 != null)
        {
            bool isMultiplayer = MainMenu.PlayerCount == 2;
            player2.SetActive(isMultiplayer);

            // Debug informații
            Debug.Log($"Game mode: {(isMultiplayer ? "Multiplayer" : "Singleplayer")}");
        }
    }
}