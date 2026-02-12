using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PaddleController : MonoBehaviour
{
    public float baseFloat = 15f;
    private float speed;
    private float baseMaxDistance = 3.05f;
    private float maxDistance;
    private Vector3 baseScale = new Vector3(0.5f, 1, 3);
    
    private Key upKey;
    private Key downKey;
    
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    
    // public float forceStrength = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = baseFloat;
        maxDistance  = baseMaxDistance;
        transform.localScale = baseScale;
        
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
    
    void OnEnable()
    {
        BallLogic.OnScore += CompareScores;
    }

    void OnDisable()
    {
        BallLogic.OnScore -= ResetVariables;
    }

    void ResetVariables()
    {
        speed = baseFloat;
        maxDistance  = baseMaxDistance;
        transform.localScale = baseScale;
    }

    void CompareScores()
    {
        int leftScore = int.Parse(leftScoreText.text);
        int rightScore = int.Parse(rightScoreText.text);

        if ((leftScore + 3) == rightScore)
        {
            bool powerUp = Random.value < 0.5f;

            if (powerUp)
            {
                if (CompareTag("LeftPaddle"))
                {
                    LengthPowerUp();
                    Debug.Log("Left Gained Some Length!");
                }
            }
            else
            {
                if (CompareTag("LeftPaddle"))
                {
                    SpeedPowerUp();
                    Debug.Log("Left Speed Up!");
                }
            }
        }
        else if ((rightScore + 3) == leftScore)
        {
            bool powerUp = Random.value < 0.5f;
            
            if (powerUp)
            {
                if (CompareTag("RightPaddle"))
                {
                    LengthPowerUp();
                    Debug.Log("Right Gained Some Length!");
                }
            }
            else
            {
                if (CompareTag("RightPaddle"))
                {
                    SpeedPowerUp();
                    Debug.Log("Right Speed Up!");
                }
            }
        }
    }

    void LengthPowerUp()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 4f);
        maxDistance = 2.55f;
    }

    void SpeedPowerUp()
    {
        speed = 15f;
    }
}
