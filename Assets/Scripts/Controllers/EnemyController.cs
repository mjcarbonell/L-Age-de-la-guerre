using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public GameObject target;
    // public TowerManager targetManager; 
    public NavMeshAgent agent;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator; 
    public int Health = 100; 
    
    void Start(){
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
        StartCoroutine(determineTarget()); 
    }

    void Update(){
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= attackRange)    
        {
            agent.SetDestination(transform.position); 
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("attack");
                // towerManager.takeDamage();
                lastAttackTime = Time.time;
                ApplyDamage(); 
            }
        }
        else if (distance <= detectionRange)
        {
            agent.SetDestination(target.transform.position);
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }
    public void ApplyDamage(){
        if (target == null) return;
        if (target.tag == "TargetEnemy" || target.tag == "TargetPlayer")
        {
            
            TowerManager towerManager = target.transform.parent.GetComponent<TowerManager>(); 
            towerManager.TakeDamage();
            towerManager.UpdateHealth();
        }
        else if (target.tag == "EnemyCaveMan" || target.tag == "CaveManPlayer")
        {
            EnemyController enemyController = target.GetComponent<EnemyController>();
            if (enemyController != null)
                enemyController.TakeDamage();
        }
    }
    public void TakeDamage(){
        Health -= 10; 
        if (Health == 0 ){
            Destroy(this, 2f); 
            gameObject.SetActive(false); // Deactivates the GameObject immediately
            gameObject.GetComponent<Collider>().enabled = false; // Disables collider (collision)
            gameObject.GetComponent<Renderer>().enabled = false; // Hides the mesh/appearance
        }
    }
    IEnumerator determineTarget(){
        while (true){
            yield return new WaitForSeconds(1f);
            // Debug.Log("Target "+ target); 
            if (CompareTag("CaveManPlayer")){
                target = GetClosestTarget(new string[] { "TargetEnemy", "EnemyCaveMan" });
            }
            if (CompareTag("EnemyCaveMan")){
                target = GetClosestTarget(new string[] { "TargetPlayer", "CaveManPlayer" });
            }
        }
    }
    GameObject GetClosestTarget(string[] tags){
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        foreach (string tag in tags){
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objs){
                float dist = Vector3.Distance(transform.position, obj.transform.position);
                if (dist < minDist){
                    minDist = dist;
                    closest = obj;
                }
            }
        }
        return closest;
    }
}
