using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private int towerHealth = 5; 
    public List<GameObject> healthBars = new List<GameObject>();
    public Slider healthSlider;
    [SerializeField]
    private int current = 100; 
    private int max = 100; 
    public void TakeDamage(){
        towerHealth -= 1; 
        if (healthBars.Count > 0)
        {
            GameObject topBar = healthBars[healthBars.Count - 1];
            healthBars.RemoveAt(healthBars.Count - 1);
            Destroy(topBar);
        }
    }
    public void UpdateHealth(){
        current -= 10; 
        healthSlider.value = current / max ;
    }
}
