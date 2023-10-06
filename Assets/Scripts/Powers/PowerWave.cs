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
    Vector3 vectorAux = new Vector3 (1, 1, 0);

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
        transform.localScale -= vectorAux * (reductionMultiplier * Time.deltaTime);
        audioSource.volume = audioSource.volume - (reductionMultiplier * Time.deltaTime);
        if(transform.localScale.x<0)Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (!collision.CompareTag("Scenary") && collision.attachedRigidbody)
        {
            collision.attachedRigidbody.velocity = direction * speed;
        }
        if (collision.CompareTag("Scenary"))
        {
            transform.localScale -= vectorAux * (reductionMultiplier);
            audioSource.volume = audioSource.volume - (reductionMultiplier);
        }  

    }
    
}
