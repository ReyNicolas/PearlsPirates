using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDuck : MonoBehaviour, IDestroy
{
    public event Action<GameObject> OnDestroyGO;

    public void Destroy()
    {
        Destroy(gameObject);
        OnDestroyGO?.Invoke(gameObject);
    }
}
