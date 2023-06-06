using System;
using System.Collections;
using UnityEngine;

public class BulletLogic: MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float actualSpeed;
    [SerializeField] Vector3 finalPosition = Vector3.zero;
    [SerializeField] bool isMoving;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Power power;
    [SerializeField] SpriteRenderer spriteRenderer;

    public void Initialize(Color color, Power power)
    {
        spriteRenderer.color = color;
        this.power = power;
     }

    private void Update()
    {
        if (!isMoving) return;
        Move();
        ReduceSpeed();
        if (ArriveToDestination()) StopAndStartPhysics();
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAndStartPhysics();
    }

    void Move()=>
        transform.position = Vector2.MoveTowards(transform.position, finalPosition, actualSpeed * Time.deltaTime);
       
    void ReduceSpeed() =>
         actualSpeed -= 0.9f * actualSpeed * Time.deltaTime;
    private bool ArriveToDestination()
       => transform.position == finalPosition;


    public void Launch(Transform shootSpawnTransform, Vector3 finalPosition)
    {
        StartCoroutine(DestroyAndStartPower(shootSpawnTransform));
        SetStartValues(shootSpawnTransform, finalPosition);   
    }

    void SetStartValues(Transform shootSpawnTransform, Vector3 finalPosition)
    {
        transform.position = shootSpawnTransform.position;
        transform.rotation = shootSpawnTransform.rotation;
        actualSpeed = speed;
        this.finalPosition = finalPosition;
        isMoving = true;
    }

    void StopAndStartPhysics()
    {
        if (isMoving)
        {
            isMoving=false;
            rigidbody2D.AddForce(transform.up * speed / 10, ForceMode2D.Impulse);
            this.enabled = false;
        }
    }
    
    IEnumerator DestroyAndStartPower(Transform shootTransform)
    {
       yield return new WaitForSeconds(power.TimeToStart);
        power.gameObject.SetActive(true);
        power.transform.position = transform.position;
        power.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }
   
}