using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > 10)
        {
            Destroy(gameObject); // distruge țeapa dacă iese din ecran
        }
    }
}
