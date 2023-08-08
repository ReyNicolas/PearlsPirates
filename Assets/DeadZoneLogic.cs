using System;
using UnityEngine;

public class DeadZoneLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDestroy>()!=null) collision.GetComponent<IDestroy>().Destroy();
    }
}
