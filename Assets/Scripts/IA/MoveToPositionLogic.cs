using System;
using UnityEngine;

public class MoveToPositionLogic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acSpeed; // prueba
    [SerializeField] LayerMask layerMask;
    [SerializeField] int degreesPerSecond;
    public Action OnArriveToPosition;
    Rigidbody2D rb;
    Vector3 position;
    float rotateTimer;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPositionToMove(Vector2 position)
    {
        this.position = position;
        Debug.Log(gameObject);
        Debug.Log(position);
       // transform.up = this.position - transform.position;
    }

    private void Update()
    {
        rotateTimer -= Time.deltaTime;
        if (Vector3.Distance(position, transform.position) < 0.2f) OnArriveToPosition?.Invoke();
        //if (rb.velocity.magnitude > speed) return;
        //rb.velocity = transform.up * speed;
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > speed) return;
        if (rotateTimer > 0)
        {
          rb.AddForce(Time.fixedDeltaTime * 0.8f * acSpeed * transform.right.normalized, ForceMode2D.Impulse);
          rb.AddForce(Time.fixedDeltaTime * 0.2f  * acSpeed * transform.up.normalized, ForceMode2D.Impulse);
            return;
        }
        rb.AddForce(Time.fixedDeltaTime * 0.8f * acSpeed * (position - transform.position).normalized, ForceMode2D.Impulse);
        rb.AddForce(Time.fixedDeltaTime * 0.2f * acSpeed * transform.up.normalized, ForceMode2D.Impulse);

    }


    private void LateUpdate()
    {
        transform.up = rb.velocity;

        if (Physics2D.Raycast(transform.position, transform.up.normalized, 2, layerMask))
        {
             rotateTimer = .5f;
        }


        //if (Physics2D.Raycast(transform.position, transform.up.normalized, 2, layerMask))
        //{
        //    transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime); 
        //    return;
        //}
        //transform.rotation = Quaternion.RotateTowards(
        //    transform.rotation
        //    , Quaternion.FromToRotation(Vector3.up, (position - transform.position)).normalized
        //    , degreesPerSecond * Time.deltaTime);
    }

}
