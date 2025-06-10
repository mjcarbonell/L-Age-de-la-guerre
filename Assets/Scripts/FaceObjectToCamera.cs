using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class FaceObjectToCamera : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthText; 
    public HealthScript healthScript; 
    void Awake(){
        healthScript = transform.parent.GetComponent<HealthScript>(); 
    }
    void Update(){
            healthText.text = $"{healthScript.Health}"; 
        Camera[] allCameras = Camera.allCameras;
        Camera closestCam = null;
        float closestDist = float.MaxValue; 
        foreach (Camera cam in allCameras)
        {
            if (cam.enabled){
                float dist = Vector3.Distance(transform.position, cam.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestCam = cam;
                }
            }
        }
        if (closestCam != null){
            transform.LookAt(closestCam.transform);
            transform.Rotate(0, 0, 0);
        }
    }
}
