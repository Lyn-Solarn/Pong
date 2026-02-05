using System.Collections;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    // Resource(s) Used: https://www.youtube.com/watch?v=AcpaYq0ihaM
    
    public float bounceStrength;
    private float baseBounceStrength;

    void Start()
    {
        baseBounceStrength = bounceStrength;
        StartCoroutine(IncreaseSpeed());
    }

    void OnEnable()
    {
        BallLogic.OnScore += resetBounceStrength;
    }

    void OnDisable()
    {
        BallLogic.OnScore -= resetBounceStrength;
    }

    // Waits 5 seconds before increasing bounceStrength
    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            bounceStrength = bounceStrength * 1.15f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        BallLogic ball = collision.gameObject.GetComponent<BallLogic>();
        
        // Checks if it was the ball colliding and not the paddles
        if (ball != null)
        {
            Vector3 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * bounceStrength);
        }
    }

    public void resetBounceStrength()
    {
        bounceStrength = baseBounceStrength;
    }
}
