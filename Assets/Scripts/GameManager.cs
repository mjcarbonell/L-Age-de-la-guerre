using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public Transform spawner;
    public Transform enemySpawner;
    public GameObject cavemanPrefab; 
    public GameObject spearThrowerPrefab; 
    public TextMeshProUGUI coinValue;
    private int currentCoins = 0; 
    void Start(){
        StartCoroutine(IncreaseCoins());   
        StartCoroutine(spawnEnemies());
    }
    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Replace with your actual scene name
    }
    public void cardSelected(){
        if (currentCoins >= 2){
            currentCoins -=2;
            coinValue.text = currentCoins.ToString();
            GameObject clickedCard = EventSystem.current.currentSelectedGameObject;
            Image imageComponent = clickedCard.GetComponent<Image>();
            if (imageComponent.sprite.name == "CaveManCard"){
                GameObject playerCaveman = Instantiate(cavemanPrefab, spawner.position,Quaternion.LookRotation(Vector3.back));
                EnemyController enemyController = playerCaveman.GetComponent<EnemyController>(); 
                enemyController.Head.tag = "CaveManPlayer";
            }
            if (imageComponent.sprite.name == "SpearThrowerCard"){
                GameObject playerCaveman = Instantiate(spearThrowerPrefab, spawner.position,Quaternion.LookRotation(Vector3.back));
                // playerCaveman.tag = "CaveManPlayer";
                ThrowerController throwerController = playerCaveman.GetComponent<ThrowerController>();
                throwerController.Head.tag = "CaveManPlayer"; 
            }
            clickedCard.SetActive(false);
        }
    }
    IEnumerator IncreaseCoins() {
        while (true) {
            yield return new WaitForSeconds(2f);
            currentCoins++;
            coinValue.text = currentCoins.ToString();
        }
    }
    IEnumerator spawnEnemies(){
        // while (true){
            yield return new WaitForSeconds(2f);
            GameObject enemyCaveman = Instantiate(cavemanPrefab, enemySpawner.position, Quaternion.LookRotation(Vector3.forward));
            EnemyController enemyController = enemyCaveman.GetComponent<EnemyController>();
            enemyController.Head.tag = "EnemyCaveMan"; 
            // enemyCaveman.tag = "EnemyCaveMan"; 
        // }
    }
    
}
