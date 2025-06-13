using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    protected Transform player;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 2f;
    public float projectileForce = 10f;
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected void MoveTowardsPlayer()
    {
        if (player && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
