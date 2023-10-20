using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWave : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 direction;
    [SerializeField] float timeAlive;
     float reductionMultiplier;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ParticleSystem particleSystem;

    public void Start()
    {
        Destroy(gameObject,timeAlive);
        reductionMultiplier = 1 / timeAlive;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Reduce();

    }

    private void Reduce()
    {
        transform.localScale -= Vector3.up * (reductionMultiplier * Time.deltaTime);
        if(transform.localScale.x<0)Destroy(gameObject);
        audioSource.volume = audioSource.volume - (reductionMultiplier * Time.deltaTime);
        particleSystem.startLifetime = transform.localScale.y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (!collision.CompareTag("Scenary") && collision.attachedRigidbody)
        {
            collision.attachedRigidbody.velocity = transform.up * speed;
        }
        if (collision.CompareTag("Scenary"))
        {
            transform.localScale -= Vector3.up * (reductionMultiplier);
            audioSource.volume = audioSource.volume - (reductionMultiplier);
        }  

    }
    
}
