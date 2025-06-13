using UnityEngine;

public class HoningSpear : MonoBehaviour
{
    public GameObject target;
    public float speed = 20f;
    public float rotateSpeed = 5f;
    public float lifeTime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime); 
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Direction to target
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Calculate rotation step
        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotateAmount * rotateSpeed;

        // Move forward constantly
        rb.velocity = transform.forward * speed;
    }
    void OnTriggerEnter(Collider other){
        
        if(target.name == other.transform.name && target.tag == other.transform.tag){
            // Debug.Log($"target tag: {target.tag} trigger tag {other.transform.tag}"); 
            // Debug.Log($"target name: {target.name} trigger hit name: {other.transform.name}");
            ApplyDamage(); 
            Destroy(gameObject, 0.1f); 
        }
        else if(other.transform.parent != null && target.name == other.transform.parent.name){
            // Debug.Log($"target name: {target.name} trigger hit tag: {other.transform.name}");
            ApplyDamage();
            Destroy(gameObject, 0.1f);
        }
    }
    public void ApplyDamage(){
        if (target == null) return;
        if (target.tag == "TargetEnemy" || target.tag == "TargetPlayer")
        {
            TowerManager towerManager = target.transform.parent.GetComponent<TowerManager>(); 
            towerManager.UpdateHealth();
        }
        else if (target.tag == "EnemyCaveMan" || target.tag == "CaveManPlayer")
        {
            HealthScript healthScript = target.transform.parent.GetComponent<HealthScript>();
            if (healthScript != null) healthScript.TakeDamage();
        }
    }
}
