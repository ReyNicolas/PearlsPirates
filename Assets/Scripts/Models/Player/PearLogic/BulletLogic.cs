using System;
using System.Collections;
using UnityEngine;

public class BulletLogic: MonoBehaviour
{
    [SerializeField] float actualSpeed;
    [SerializeField] Vector3 finalPosition = Vector3.zero;
    [SerializeField] bool isMoving;
    [SerializeField] Rigidbody2D rigidbody2D;
    PowerSO powerData;

    public void Initialize(PowerSO powerData)
    {
        this.powerData = powerData;
        GetComponent<SpriteRenderer>().color = powerData.PowerColor;
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


    public void Launch(Transform shootSpawnTransform, Vector3 finalPosition, float speed)
    {
        StartCoroutine(DestroyAndStartPower(shootSpawnTransform));
        SetStartValues(shootSpawnTransform, finalPosition, speed);   
    }

     IEnumerator DestroyAndStartPower(Transform shootTransform)
    {
        yield return new WaitForSeconds(powerData.TimeToStart);
        Instantiate(powerData.PowerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void SetStartValues(Transform shootSpawnTransform, Vector3 finalPosition, float speed)
    {
        transform.position = shootSpawnTransform.position;
        transform.rotation = shootSpawnTransform.rotation;
        actualSpeed = speed;
        this.finalPosition = finalPosition;
        isMoving = true;
    }

    void Move() =>
    transform.position = Vector2.MoveTowards(transform.position, finalPosition, actualSpeed * Time.deltaTime);
    void ReduceSpeed() =>
         actualSpeed -= 0.9f * actualSpeed * Time.deltaTime;
    bool ArriveToDestination()
       => transform.position == finalPosition;

    void StopAndStartPhysics()
    {
        if (isMoving)
        {
            isMoving=false;
            rigidbody2D.AddForce(transform.up * actualSpeed / 10, ForceMode2D.Impulse);
            this.enabled = false;
        }
    }
    
}