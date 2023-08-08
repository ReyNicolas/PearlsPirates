using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDuck : MonoBehaviour, IDestroy
{
    public event Action<GameObject> onDestroy;

    public void Destroy()
    {
        Destroy(gameObject);
        onDestroy?.Invoke(gameObject);
    }
}
