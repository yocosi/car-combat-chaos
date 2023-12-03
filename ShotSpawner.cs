using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject shot;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public float fireRate;
    private float lastShootTime;

    void Update()
    {
        transform.rotation = player.transform.rotation * Quaternion.Euler(0, 0, 90);
        if (Input.GetMouseButtonDown(0) && !gameOverScreen.activeSelf && !winScreen.activeSelf)
        {
            float timeSinceLastShoot = Time.time - lastShootTime;

            if (timeSinceLastShoot >= fireRate)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.shotSound, transform.position);
                Instantiate(shot, player.transform.position, transform.rotation);
                lastShootTime = Time.time;
            }
        }
    }
}
