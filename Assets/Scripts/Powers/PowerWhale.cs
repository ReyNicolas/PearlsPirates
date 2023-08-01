using UnityEngine;

public class PowerWhale : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform targetTransform;
    [SerializeField] float timeAlive;


    public void Start()
    {
        Destroy(gameObject, timeAlive);
    }

    private void Update()
    {
        if(targetTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && targetTransform == null)
        {
            targetTransform = collision.transform;
        }
    }

}
