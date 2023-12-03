using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotScript : MonoBehaviour
{
    public LogicManager logic;
    public Rigidbody2D rigidBody;
    public float bulletSpeed;

    public UnityEvent OnHit = new UnityEvent(); 

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = bulletSpeed * (-transform.up);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            OnHit?.Invoke();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.carExplosionSound, transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            logic.AddScore(1);
        }
    }
}
