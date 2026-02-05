using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float speed = 2f;
    private float maxDistance = 3.05f;
    private Key upKey;
    private Key downKey;
    // public float forceStrength = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CompareTag("LeftPaddle"))
        {
            upKey = Key.W;
            downKey = Key.S;
        }
        else if (CompareTag("RightPaddle"))
        {
            upKey = Key.UpArrow;
            downKey = Key.DownArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[upKey].isPressed)
        {
            // Vector3 force = new Vector3(0f, 0f, forceStrength);
            // Rigidbody rb = GetComponent<Rigidbody>();
            // rb.AddForce(force, ForceMode.Force);
            
            Vector3 newPosition = transform.position += new Vector3(0f, 0f, speed) * Time.deltaTime;
            newPosition.z = Math.Clamp(newPosition.z, -maxDistance, maxDistance);
            
            transform.position = newPosition;
        }

        if (Keyboard.current[downKey].isPressed)
        {
            // Vector3 force = new Vector3(0f, 0f, -forceStrength);
            // Rigidbody rb = GetComponent<Rigidbody>();
            // rb.AddForce(force, ForceMode.Force);

            Vector3 newPosition = transform.position += new Vector3(0f, 0f, -speed) * Time.deltaTime;
            newPosition.z = Math.Clamp(newPosition.z, -maxDistance, maxDistance);
            
            transform.position = newPosition;
        }
    }
}
