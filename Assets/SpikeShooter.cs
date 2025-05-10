using UnityEngine;

public class SpikeShooter : MonoBehaviour
{
    public GameObject spikePrefab;
    public Transform firePoint;
    public KeyCode shootKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(spikePrefab, firePoint.position, Quaternion.identity);
    }
}
