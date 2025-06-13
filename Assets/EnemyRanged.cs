using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float fireCooldown = 2f;
    public float projectileSpeed = 10f;

    private float fireTimer = 0f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Move toward the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Shoot projectile
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootDirection = (player.position - firePoint.position).normalized;
            rb.velocity = shootDirection * projectileSpeed;
        }
    }
}
