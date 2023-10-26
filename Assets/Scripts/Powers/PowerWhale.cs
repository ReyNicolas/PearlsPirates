using System;
using UnityEngine;

public class PowerWhale : MonoBehaviour, IDestroy
{
    [SerializeField] float speed;
    [SerializeField] Transform targetTransform;
    [SerializeField] float timeAlive;

    public event Action<GameObject> OnDestroyGO;

    public void Start()
    {
        Destroy(gameObject, timeAlive);
    }

    private void Update()
    {
        if(targetTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            transform.up = targetTransform.position- transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && targetTransform == null)
        {
            targetTransform = collision.transform;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
        OnDestroyGO?.Invoke(gameObject);
    }
}
