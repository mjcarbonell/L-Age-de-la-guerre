using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int Health = 100; 
    public void TakeDamage(){
        Health -= 10; 
        if (Health == 0 ){
            Destroy(this, 2f); 
            gameObject.SetActive(false); // Deactivates the GameObject immediately
            gameObject.GetComponent<Collider>().enabled = false; // Disables collider (collision)
            gameObject.GetComponent<Renderer>().enabled = false; // Hides the mesh/appearance
        }
    }
}
