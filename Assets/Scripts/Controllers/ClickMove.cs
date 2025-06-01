using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    [SerializeField]
    private Animator animator;
    public float speed; 
    void Update()
    {
        speed = agent.velocity.magnitude;
        animator.SetFloat("speed", speed);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(0)){
            Attack();
        }
        
    }
    void Attack(){
        animator.SetTrigger("attack"); // This assumes your Animator has a trigger called "attack"
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f)){
            Debug.Log("Ray hit: " + hit.collider.name); // SAFE — only runs if raycast hits
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy!");
            }
        }
        
    }
}
