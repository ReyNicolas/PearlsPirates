using Unity.Mathematics;
using UnityEngine;

public class PowerWhirlpool : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float timeAlive;

    public void Start()
    {
        Destroy(gameObject, timeAlive);
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
