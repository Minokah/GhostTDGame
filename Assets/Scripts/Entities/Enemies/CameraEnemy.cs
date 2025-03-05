using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemy : Enemy
{
    private GameObject spawner;
    void Start()
    {
        StartCoroutine(CameraLoop());
    }
    
    private IEnumerator CameraLoop()
    {
        Camera();
        Debug.Log("Camera running");
        yield return new WaitForSeconds(30);
    }

    private void Camera()
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().cameraManEffect();
    }
}
