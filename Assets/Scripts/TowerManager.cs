using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private int towerHealth = 5; 
    public List<GameObject> healthBars = new List<GameObject>();



    public void takeDamage(){
        towerHealth -= 1; 
        if (healthBars.Count > 0)
        {
            GameObject topBar = healthBars[healthBars.Count - 1];
            healthBars.RemoveAt(healthBars.Count - 1);
            Destroy(topBar);
        }
    }
}
