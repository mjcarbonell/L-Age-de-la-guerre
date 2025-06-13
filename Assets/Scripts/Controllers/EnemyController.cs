using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public GameObject Head; 
    public NavMeshAgent agent;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator; 
    [SerializeField]
    private AudioSource audioSource;
    void Start(){
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
        StartCoroutine(determineTarget()); 
    }
    void Update(){
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // Debug.Log($"Distance {distance} attackRange {attackRange}"); 
        if (distance <= attackRange)    
        {
            agent.isStopped = true; 
            animator.SetFloat("speed", 0);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(attackProcess()); 
                lastAttackTime = Time.time;
                
            }
        }
        else if (distance <= detectionRange)
        {
            agent.isStopped = false;
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
            towerManager.UpdateHealth();
        }
        else if (target.tag == "EnemyCaveMan" || target.tag == "CaveManPlayer")
        {
            HealthScript healthScript = target.transform.parent.GetComponent<HealthScript>(); 
            if (healthScript != null) healthScript.TakeDamage(); 
        }
    }
    IEnumerator attackProcess(){
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        ApplyDamage(); 
        audioSource.Play(); 

    }
    IEnumerator determineTarget(){
        while (true){
            yield return new WaitForSeconds(1f);
            if ( Head.tag == "CaveManPlayer"){
                target = GetClosestTarget(new string[] { "TargetEnemy", "EnemyCaveMan" });
            }
            if ( Head.tag == "EnemyCaveMan"){
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
