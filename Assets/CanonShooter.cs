using UnityEngine;

public class CanonShooter : MonoBehaviour
{
    public GameObject cannonballPrefab; // Assign your cannonball prefab in the inspector
    public Transform firePoint;         // Where the cannonball spawns (e.g., the mouth of the cannon)
    public float launchForce = 700f;    // Adjust the force as needed
    public GameObject target;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireCannon();
        }
    }

    void FireCannon()
    {
        if (cannonballPrefab != null && firePoint != null)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            HoningSpear honing = cannonball.GetComponent<HoningSpear>();
            honing.target = target; 
        }
    }
}


