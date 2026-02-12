using System.Collections;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    // Resource(s) Used: https://www.youtube.com/watch?v=AcpaYq0ihaM
    
    public float bounceStrength;
    private float baseBounceStrength;
    public AudioClip collisionSound;
    public float basePitch;
    private float pitch;
    
    AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        
        baseBounceStrength = bounceStrength;
        pitch = basePitch;
        
        StartCoroutine(IncreaseSpeed());
        StartCoroutine(RaisePitch());
    }

    void OnEnable()
    {
        BallLogic.OnScore += resetBounceStrength;
        BallLogic.OnScore += resetPitch;
    }

    void OnDisable()
    {
        BallLogic.OnScore -= resetBounceStrength;
        BallLogic.OnScore -= resetPitch;
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

    IEnumerator RaisePitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            
            pitch = pitch * 1.05f;
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
            
            AudioSource.pitch = pitch;
            AudioSource.PlayOneShot(collisionSound);
        }
    }

    public void resetBounceStrength()
    {
        bounceStrength = baseBounceStrength;
        
    }

    public void resetPitch()
    {
        pitch = basePitch;
    }
}
