using System;
using System.Collections;
using UnityEngine;

public class BulletLogic: MonoBehaviour, IDestroy
{
    [SerializeField] float actualSpeed;
    [SerializeField] Vector3 finalPosition = Vector3.zero;
    [SerializeField] bool isMoving;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject CreationEffectPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;

    PowerSO powerData;

    public event Action<GameObject> onDestroy;

    public void Initialize(PowerSO powerData)
    {
        this.powerData = powerData;
        spriteRenderer.color = powerData.PowerColor;
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
        Destroy(Instantiate(CreationEffectPrefab, transform.position, Quaternion.identity),1.5f);

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
            particleSystem.Play();
            audioSource.Play();
            rigidbody2D.AddForce(transform.up * actualSpeed / 10, ForceMode2D.Impulse);            
            this.enabled = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}