using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public LogicManager logic;
    public float moveSpeed;
    private bool isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && !logic.GetWinCheck())
        {
            CarMovement(); // Keyboard movement
            CarRotation(); // Rotating by following the mouse   
        }
    }

    void CarMovement()
    {
        //Vector2 used here because we just want to move horizontally and vertically
        Vector2 input = new Vector2(
            (Input.GetAxisRaw("Horizontal") * moveSpeed), 
            (Input.GetAxisRaw("Vertical") * moveSpeed)).normalized; 

        //Vector3 to get the new position because even a 2D object has 3 axis in Unity
        Vector3 movement = new Vector3(
            input.x * moveSpeed, 
            input.y * moveSpeed, 0) * Time.deltaTime;

        transform.position += movement;
    }

    void CarRotation()
    {
        // Get the current position of the mouse on the screen in pixel coordinates.
        Vector3 mousePos = Input.mousePosition;
        
        if (Camera.main != null)
        {
            // Convert the object's world position to screen position. This allows me to determine where
            // the object is in terms of pixel coordinates on the screen.
            Vector3 objectPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        
            // Vector pointing from the object towards the mouse.
            Vector3 directionToMouse = mousePos - objectPosOnScreen;

            // Calculate the angle between the positive X-axis and the direction to the mouse.
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            // Set the object's rotation so that its forward direction points towards the mouse.
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            logic.GameOver();
            isGameOver = true;   
        }
    }
}
