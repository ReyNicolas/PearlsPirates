using System;
using UnityEngine;

public interface IDestroy
{
    void Destroy();
    event Action<GameObject> OnDestroyGO;
}