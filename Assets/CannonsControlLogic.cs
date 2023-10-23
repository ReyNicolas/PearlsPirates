using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonsControlLogic : MonoBehaviour
{
    [SerializeField] float fireControlRate;
    [SerializeField] LayerMask layerMask;
    [SerializeField] CannonShoot cannonShootRight;
    [SerializeField] CannonShoot cannonShootLeft;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack", fireControlRate, fireControlRate);
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(transform.position + (transform.right * 3), 2 ,layerMask))
        {
            Debug.Log("colision right");
            cannonShootRight.Shoot();
        }
        if (Physics2D.OverlapCircle(transform.position - (transform.right * 3), 2, layerMask))
        {
            Debug.Log("colision left");
            cannonShootLeft.Shoot();
        }
    }

    

}
