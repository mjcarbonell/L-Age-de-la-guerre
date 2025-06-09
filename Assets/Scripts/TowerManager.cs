using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private int towerHealth = 5; 
    public Slider healthSlider;
    [SerializeField]
    private float current = 1; 
    private float max = 1; 
    public void UpdateHealth(){
        if (current <= 0f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
        current -= 0.02f; 
        healthSlider.value = current / max ;
    }

}
