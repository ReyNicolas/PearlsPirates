using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWave : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 direction;
    [SerializeField] float timeAlive;
    Vector3 vectorAux = new Vector3 (1, 1, 0);

    public void Start()
    {
        Destroy(gameObject,timeAlive);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.localScale -= vectorAux * (1 / timeAlive * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (!collision.CompareTag("Scenary") && collision.attachedRigidbody)
        {
            collision.attachedRigidbody.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scenary"))
        {
            Destroy(gameObject);
        }
    }
    
}
