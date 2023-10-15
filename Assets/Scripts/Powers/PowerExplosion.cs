using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerExplosion : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float force;
    [SerializeField] LayerMask layer;
    [SerializeField] int timeAlive;


    public void Start()
    {
        GetCloseColliders()
                   .ForEach(collider =>
                   {
                       if (collider.TryGetComponent<Rigidbody2D>(out var rb))
                       {
                           rb.velocity = (collider.transform.position - transform.position).normalized * force;
                       }
                   });

        Destroy(gameObject,timeAlive);
    }

    List<Collider2D> GetCloseColliders() 
        => Physics2D.OverlapCircleAll(transform.position, radius, layer).ToList();

}