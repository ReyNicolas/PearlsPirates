using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToPositionLogic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask layerMask;
    Rigidbody2D rb;
    Vector3 position;
    int degreesPerSecond = 90;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPositionToMove(Vector2 position)
    {
        this.position = position;
        transform.up = this.position - transform.position;
    }

    private void Update()
    {
        rb.velocity = transform.up * speed;

        if (Vector3.Distance(position, transform.position) < 0.1f) Destroy(gameObject);
    }
    private void LateUpdate()
    {
        if (Physics2D.Raycast(transform.position, transform.up.normalized, 2, layerMask))
        {
            transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime); 
            return;
        }
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.FromToRotation(Vector3.up, (position-transform.position)).normalized, degreesPerSecond * Time.deltaTime);
           
        
            

    }
}
