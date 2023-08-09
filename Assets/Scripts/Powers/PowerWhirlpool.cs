using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PowerWhirlpool : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float timeAlive;

    public void Start()
    {
        Destroy(gameObject, timeAlive);
        StartCoroutine(startsToDisappear());
    }

    IEnumerator startsToDisappear()
    {
        yield return new WaitForSeconds(timeAlive * 0.8f);
        while (true)
        {
            force *= 0.9f;
            transform.localScale *= 0.9f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody)
        {
             
            collision.attachedRigidbody.AddForce( CalculateForce(transform.position - collision.transform.position) ,ForceMode2D.Impulse);
        }   
    }

    Vector2 CalculateForce(Vector2 vectorDistance) =>
        vectorDistance.normalized * Mathf.Min(force * Time.deltaTime, vectorDistance.magnitude);


}
