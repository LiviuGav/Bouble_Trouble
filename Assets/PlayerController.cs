using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float screenEdgeOffset = 0.5f;
    public bool isPlayerTwo = false;

    [SerializeField] private GameObject heartUI;

    private string horizontalAxis;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        horizontalAxis = isPlayerTwo ? "Horizontal2" : "Horizontal";

        // Dezactivează Player2 și UI-ul său dacă e singleplayer
        if (isPlayerTwo && MainMenu.PlayerCount == 1)
        {
            gameObject.SetActive(false);

            // Dezactivează și UI-ul de inimi dacă există
            if (heartUI != null)
            {
                heartUI.gameObject.SetActive(false);
            }
            return;
        }
    }

    void Update()
    {
        float move = Input.GetAxis(horizontalAxis);
        transform.Translate(Vector2.right * move * speed * Time.deltaTime);
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float clampedX = Mathf.Clamp(
            transform.position.x,
            minBounds.x + screenEdgeOffset,
            maxBounds.x - screenEdgeOffset
        );

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}