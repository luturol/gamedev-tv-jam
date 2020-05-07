using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public float offset;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    
    void LateUpdate()
    {    
        Vector3 cameraTemp = transform.position;
        cameraTemp.x = playerTransform.position.x;
        
        if(playerTransform.position.y > 0 || playerTransform.position.y < 0)
        {            
            cameraTemp.y = playerTransform.position.y;
        }
            

        transform.position = cameraTemp;
    }
}
