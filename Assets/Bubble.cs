using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public GameObject smallerBubblePrefab;
    public int maxSplits = 2;
    public float bottomLimit = -5f;
    public float screenEdgeBuffer = 0.2f;
    public int bigBubblePoints = 5; // Puncte pentru bule mari
    public int smallBubblePoints = 10; // Puncte pentru bule mici

    private Rigidbody2D rb;
    private int splitLevel = 0;
    private int direction = 1;
    private Camera mainCamera;
    private float screenLeftEdge;
    private float screenRightEdge;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        CalculateScreenEdges();
        rb.linearVelocity = new Vector2(direction * moveSpeed, jumpForce);
    }

    void CalculateScreenEdges()
    {
        float objectWidth = GetComponent<CircleCollider2D>().bounds.extents.x;
        screenLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + objectWidth + screenEdgeBuffer;
        screenRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - objectWidth - screenEdgeBuffer;
    }

    void Update()
    {
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 currentPos = transform.position;
        if (currentPos.x <= screenLeftEdge || currentPos.x >= screenRightEdge)
        {
            direction *= -1;
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
            float clampedX = Mathf.Clamp(currentPos.x, screenLeftEdge, screenRightEdge);
            transform.position = new Vector2(clampedX, currentPos.y);
        }
    }

    public void Pop()
    {
        // Adaugă puncte în funcție de dimensiunea bulei
        int points = splitLevel == 0 ? smallBubblePoints : bigBubblePoints;
        ScoreManager.Instance.AddScore(points);

        if (splitLevel < maxSplits)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                GameObject newBubble = Instantiate(smallerBubblePrefab, transform.position, Quaternion.identity);
                Bubble b = newBubble.GetComponent<Bubble>();
                b.splitLevel = splitLevel + 1;
                b.moveSpeed = moveSpeed + 1;
                b.jumpForce = jumpForce * 0.8f;
                b.direction = i;
                b.screenEdgeBuffer = screenEdgeBuffer;
                b.bigBubblePoints = bigBubblePoints;
                b.smallBubblePoints = smallBubblePoints;
            }
        }
        Destroy(gameObject, 0.1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Pop();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }

            
            rb.AddForce(new Vector2(direction * moveSpeed * 1.5f, jumpForce * 2.5f), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(new Vector2(direction * moveSpeed * 1.5f, jumpForce * 2.5f), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Bubble"))
        {
            Bubble otherBubble = collision.gameObject.GetComponent<Bubble>();
            if (otherBubble != null)
            {
                Vector2 repelDirection = (transform.position - collision.transform.position).normalized;
                float repelForce = 2f;
                rb.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
                otherBubble.GetComponent<Rigidbody2D>().AddForce(-repelDirection * repelForce, ForceMode2D.Impulse);
            }
        }
       
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(screenLeftEdge, -10, 0), new Vector3(screenLeftEdge, 10, 0));
            Gizmos.DrawLine(new Vector3(screenRightEdge, -10, 0), new Vector3(screenRightEdge, 10, 0));
        }
    }
}