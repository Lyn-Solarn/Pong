using UnityEngine;
using TMPro;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class BallLogic : MonoBehaviour
{
    // Resource(s) Used: https://www.youtube.com/watch?v=AcpaYq0ihaM
    
    private Rigidbody rb;
    private int leftScore;
    private int rightScore;
    // -1f = LeftSide Loser, 1f = RightSide Loser
    private float currentLoser = 1f;
    public float speed = 200f;
    public TextMeshProUGUI scores;
    public GameObject leftWin;
    public GameObject rightWin;

    public static event Action OnScore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLoser = Random.value < 0.5f ? 1f : -1f;
        
        leftScore = 0;
        rightScore = 0;
        leftWin.SetActive(false);
        rightWin.SetActive(false);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        
        transform.position = new Vector3(0f, 0.5f, 0f);
        rb.linearVelocity = Vector3.zero;
        
        AddStartingForce();
    }

    IEnumerator ResetGame()
    {
        transform.position = new Vector3(0f, 0.5f, 0f);
        rb.linearVelocity = Vector3.zero;
        
        yield return new WaitForSeconds(3f);
        
        leftScore = 0;
        rightScore = 0;
        SetScores();
        leftWin.SetActive(false);
        rightWin.SetActive(false);
        
        StartCoroutine(StartGame());
    }

    void AddStartingForce()
    {
        float x = currentLoser;
        float z = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1.0f, -0.5f) : UnityEngine.Random.Range(0.5f, 1.0f);

        Vector3 direction = new Vector3(x, 0, z);
        rb.AddForce(direction * speed);
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    void SetScores()
    {
        scores.text = leftScore.ToString() + " - " + rightScore.ToString();

        if (leftScore == 11)
        {
            leftWin.SetActive(true);
            Debug.Log("Game Over, Left Side Wins!");
        }

        if (rightScore == 11)
        {
            rightWin.SetActive(true);
            Debug.Log("Game Over, Right Side Wins!");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftGoal"))
        {
            rightScore++;
            SetScores();
            // Ball goes toward left paddle
            currentLoser = -1f;
            
            if (rightScore != 11)
            {
                StartCoroutine(StartGame());
            }
            else
            {
                StartCoroutine(ResetGame());
            }
            
            OnScore?.Invoke();
            
            Debug.Log("Right Side Scored!");
        }

        if (other.gameObject.CompareTag("RightGoal"))
        {
            leftScore++;
            SetScores();
            //Ball goes toward right paddle
            currentLoser = 1f;
            
            if (leftScore != 11)
            {
                StartCoroutine(StartGame());
            }
            else
            {
                StartCoroutine(ResetGame());
            }
            
            OnScore?.Invoke();
            
            Debug.Log("Left Side Scored!");
        }
    }
}
